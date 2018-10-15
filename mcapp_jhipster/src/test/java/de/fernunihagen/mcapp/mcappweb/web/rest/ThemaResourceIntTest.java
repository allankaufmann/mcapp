package de.fernunihagen.mcapp.mcappweb.web.rest;

import de.fernunihagen.mcapp.mcappweb.McappWebApp;

import de.fernunihagen.mcapp.mcappweb.domain.Thema;
import de.fernunihagen.mcapp.mcappweb.repository.ThemaRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.ThemaSearchRepository;
import de.fernunihagen.mcapp.mcappweb.web.rest.errors.ExceptionTranslator;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.MockitoAnnotations;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.data.web.PageableHandlerMethodArgumentResolver;
import org.springframework.http.MediaType;
import org.springframework.http.converter.json.MappingJackson2HttpMessageConverter;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.setup.MockMvcBuilders;
import org.springframework.transaction.annotation.Transactional;

import javax.persistence.EntityManager;
import java.util.Collections;
import java.util.List;


import static de.fernunihagen.mcapp.mcappweb.web.rest.TestUtil.createFormattingConversionService;
import static org.assertj.core.api.Assertions.assertThat;
import static org.elasticsearch.index.query.QueryBuilders.queryStringQuery;
import static org.hamcrest.Matchers.hasItem;
import static org.mockito.Mockito.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

/**
 * Test class for the ThemaResource REST controller.
 *
 * @see ThemaResource
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes = McappWebApp.class)
public class ThemaResourceIntTest {

    private static final Long DEFAULT_THEMA_ID = 1L;
    private static final Long UPDATED_THEMA_ID = 2L;

    private static final String DEFAULT_THEMA_TEXT = "AAAAAAAAAA";
    private static final String UPDATED_THEMA_TEXT = "BBBBBBBBBB";

    @Autowired
    private ThemaRepository themaRepository;

    /**
     * This repository is mocked in the de.fernunihagen.mcapp.mcappweb.repository.search test package.
     *
     * @see de.fernunihagen.mcapp.mcappweb.repository.search.ThemaSearchRepositoryMockConfiguration
     */
    @Autowired
    private ThemaSearchRepository mockThemaSearchRepository;

    @Autowired
    private MappingJackson2HttpMessageConverter jacksonMessageConverter;

    @Autowired
    private PageableHandlerMethodArgumentResolver pageableArgumentResolver;

    @Autowired
    private ExceptionTranslator exceptionTranslator;

    @Autowired
    private EntityManager em;

    private MockMvc restThemaMockMvc;

    private Thema thema;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        final ThemaResource themaResource = new ThemaResource(themaRepository, mockThemaSearchRepository);
        this.restThemaMockMvc = MockMvcBuilders.standaloneSetup(themaResource)
            .setCustomArgumentResolvers(pageableArgumentResolver)
            .setControllerAdvice(exceptionTranslator)
            .setConversionService(createFormattingConversionService())
            .setMessageConverters(jacksonMessageConverter).build();
    }

    /**
     * Create an entity for this test.
     *
     * This is a static method, as tests for other entities might also need it,
     * if they test an entity which requires the current entity.
     */
    public static Thema createEntity(EntityManager em) {
        Thema thema = new Thema()
            .themaID(DEFAULT_THEMA_ID)
            .themaText(DEFAULT_THEMA_TEXT);
        return thema;
    }

    @Before
    public void initTest() {
        thema = createEntity(em);
    }

    @Test
    @Transactional
    public void createThema() throws Exception {
        int databaseSizeBeforeCreate = themaRepository.findAll().size();

        // Create the Thema
        restThemaMockMvc.perform(post("/api/themas")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(thema)))
            .andExpect(status().isCreated());

        // Validate the Thema in the database
        List<Thema> themaList = themaRepository.findAll();
        assertThat(themaList).hasSize(databaseSizeBeforeCreate + 1);
        Thema testThema = themaList.get(themaList.size() - 1);
        assertThat(testThema.getThemaID()).isEqualTo(DEFAULT_THEMA_ID);
        assertThat(testThema.getThemaText()).isEqualTo(DEFAULT_THEMA_TEXT);

        // Validate the Thema in Elasticsearch
        verify(mockThemaSearchRepository, times(1)).save(testThema);
    }

    @Test
    @Transactional
    public void createThemaWithExistingId() throws Exception {
        int databaseSizeBeforeCreate = themaRepository.findAll().size();

        // Create the Thema with an existing ID
        thema.setId(1L);

        // An entity with an existing ID cannot be created, so this API call must fail
        restThemaMockMvc.perform(post("/api/themas")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(thema)))
            .andExpect(status().isBadRequest());

        // Validate the Thema in the database
        List<Thema> themaList = themaRepository.findAll();
        assertThat(themaList).hasSize(databaseSizeBeforeCreate);

        // Validate the Thema in Elasticsearch
        verify(mockThemaSearchRepository, times(0)).save(thema);
    }

    @Test
    @Transactional
    public void getAllThemas() throws Exception {
        // Initialize the database
        themaRepository.saveAndFlush(thema);

        // Get all the themaList
        restThemaMockMvc.perform(get("/api/themas?sort=id,desc"))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(thema.getId().intValue())))
            .andExpect(jsonPath("$.[*].themaID").value(hasItem(DEFAULT_THEMA_ID.intValue())))
            .andExpect(jsonPath("$.[*].themaText").value(hasItem(DEFAULT_THEMA_TEXT.toString())));
    }
    
    @Test
    @Transactional
    public void getThema() throws Exception {
        // Initialize the database
        themaRepository.saveAndFlush(thema);

        // Get the thema
        restThemaMockMvc.perform(get("/api/themas/{id}", thema.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.id").value(thema.getId().intValue()))
            .andExpect(jsonPath("$.themaID").value(DEFAULT_THEMA_ID.intValue()))
            .andExpect(jsonPath("$.themaText").value(DEFAULT_THEMA_TEXT.toString()));
    }

    @Test
    @Transactional
    public void getNonExistingThema() throws Exception {
        // Get the thema
        restThemaMockMvc.perform(get("/api/themas/{id}", Long.MAX_VALUE))
            .andExpect(status().isNotFound());
    }

    @Test
    @Transactional
    public void updateThema() throws Exception {
        // Initialize the database
        themaRepository.saveAndFlush(thema);

        int databaseSizeBeforeUpdate = themaRepository.findAll().size();

        // Update the thema
        Thema updatedThema = themaRepository.findById(thema.getId()).get();
        // Disconnect from session so that the updates on updatedThema are not directly saved in db
        em.detach(updatedThema);
        updatedThema
            .themaID(UPDATED_THEMA_ID)
            .themaText(UPDATED_THEMA_TEXT);

        restThemaMockMvc.perform(put("/api/themas")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(updatedThema)))
            .andExpect(status().isOk());

        // Validate the Thema in the database
        List<Thema> themaList = themaRepository.findAll();
        assertThat(themaList).hasSize(databaseSizeBeforeUpdate);
        Thema testThema = themaList.get(themaList.size() - 1);
        assertThat(testThema.getThemaID()).isEqualTo(UPDATED_THEMA_ID);
        assertThat(testThema.getThemaText()).isEqualTo(UPDATED_THEMA_TEXT);

        // Validate the Thema in Elasticsearch
        verify(mockThemaSearchRepository, times(1)).save(testThema);
    }

    @Test
    @Transactional
    public void updateNonExistingThema() throws Exception {
        int databaseSizeBeforeUpdate = themaRepository.findAll().size();

        // Create the Thema

        // If the entity doesn't have an ID, it will throw BadRequestAlertException
        restThemaMockMvc.perform(put("/api/themas")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(thema)))
            .andExpect(status().isBadRequest());

        // Validate the Thema in the database
        List<Thema> themaList = themaRepository.findAll();
        assertThat(themaList).hasSize(databaseSizeBeforeUpdate);

        // Validate the Thema in Elasticsearch
        verify(mockThemaSearchRepository, times(0)).save(thema);
    }

    @Test
    @Transactional
    public void deleteThema() throws Exception {
        // Initialize the database
        themaRepository.saveAndFlush(thema);

        int databaseSizeBeforeDelete = themaRepository.findAll().size();

        // Get the thema
        restThemaMockMvc.perform(delete("/api/themas/{id}", thema.getId())
            .accept(TestUtil.APPLICATION_JSON_UTF8))
            .andExpect(status().isOk());

        // Validate the database is empty
        List<Thema> themaList = themaRepository.findAll();
        assertThat(themaList).hasSize(databaseSizeBeforeDelete - 1);

        // Validate the Thema in Elasticsearch
        verify(mockThemaSearchRepository, times(1)).deleteById(thema.getId());
    }

    @Test
    @Transactional
    public void searchThema() throws Exception {
        // Initialize the database
        themaRepository.saveAndFlush(thema);
        when(mockThemaSearchRepository.search(queryStringQuery("id:" + thema.getId())))
            .thenReturn(Collections.singletonList(thema));
        // Search the thema
        restThemaMockMvc.perform(get("/api/_search/themas?query=id:" + thema.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(thema.getId().intValue())))
            .andExpect(jsonPath("$.[*].themaID").value(hasItem(DEFAULT_THEMA_ID.intValue())))
            .andExpect(jsonPath("$.[*].themaText").value(hasItem(DEFAULT_THEMA_TEXT.toString())));
    }

    @Test
    @Transactional
    public void equalsVerifier() throws Exception {
        TestUtil.equalsVerifier(Thema.class);
        Thema thema1 = new Thema();
        thema1.setId(1L);
        Thema thema2 = new Thema();
        thema2.setId(thema1.getId());
        assertThat(thema1).isEqualTo(thema2);
        thema2.setId(2L);
        assertThat(thema1).isNotEqualTo(thema2);
        thema1.setId(null);
        assertThat(thema1).isNotEqualTo(thema2);
    }
}
