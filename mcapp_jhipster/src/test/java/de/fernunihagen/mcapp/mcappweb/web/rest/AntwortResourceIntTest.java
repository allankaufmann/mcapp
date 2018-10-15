package de.fernunihagen.mcapp.mcappweb.web.rest;

import de.fernunihagen.mcapp.mcappweb.McappWebApp;

import de.fernunihagen.mcapp.mcappweb.domain.Antwort;
import de.fernunihagen.mcapp.mcappweb.repository.AntwortRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.AntwortSearchRepository;
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
import org.springframework.util.Base64Utils;

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
 * Test class for the AntwortResource REST controller.
 *
 * @see AntwortResource
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes = McappWebApp.class)
public class AntwortResourceIntTest {

    private static final Long DEFAULT_ANTWORT_ID = 1L;
    private static final Long UPDATED_ANTWORT_ID = 2L;

    private static final Long DEFAULT_POSITION = 1L;
    private static final Long UPDATED_POSITION = 2L;

    private static final Boolean DEFAULT_WAHR = false;
    private static final Boolean UPDATED_WAHR = true;

    private static final String DEFAULT_TEXT = "AAAAAAAAAA";
    private static final String UPDATED_TEXT = "BBBBBBBBBB";

    private static final byte[] DEFAULT_BILD = TestUtil.createByteArray(1, "0");
    private static final byte[] UPDATED_BILD = TestUtil.createByteArray(1, "1");
    private static final String DEFAULT_BILD_CONTENT_TYPE = "image/jpg";
    private static final String UPDATED_BILD_CONTENT_TYPE = "image/png";

    @Autowired
    private AntwortRepository antwortRepository;

    /**
     * This repository is mocked in the de.fernunihagen.mcapp.mcappweb.repository.search test package.
     *
     * @see de.fernunihagen.mcapp.mcappweb.repository.search.AntwortSearchRepositoryMockConfiguration
     */
    @Autowired
    private AntwortSearchRepository mockAntwortSearchRepository;

    @Autowired
    private MappingJackson2HttpMessageConverter jacksonMessageConverter;

    @Autowired
    private PageableHandlerMethodArgumentResolver pageableArgumentResolver;

    @Autowired
    private ExceptionTranslator exceptionTranslator;

    @Autowired
    private EntityManager em;

    private MockMvc restAntwortMockMvc;

    private Antwort antwort;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        final AntwortResource antwortResource = new AntwortResource(antwortRepository, mockAntwortSearchRepository);
        this.restAntwortMockMvc = MockMvcBuilders.standaloneSetup(antwortResource)
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
    public static Antwort createEntity(EntityManager em) {
        Antwort antwort = new Antwort()
            .antwortID(DEFAULT_ANTWORT_ID)
            .position(DEFAULT_POSITION)
            .wahr(DEFAULT_WAHR)
            .text(DEFAULT_TEXT)
            .bild(DEFAULT_BILD)
            .bildContentType(DEFAULT_BILD_CONTENT_TYPE);
        return antwort;
    }

    @Before
    public void initTest() {
        antwort = createEntity(em);
    }

    @Test
    @Transactional
    public void createAntwort() throws Exception {
        int databaseSizeBeforeCreate = antwortRepository.findAll().size();

        // Create the Antwort
        restAntwortMockMvc.perform(post("/api/antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(antwort)))
            .andExpect(status().isCreated());

        // Validate the Antwort in the database
        List<Antwort> antwortList = antwortRepository.findAll();
        assertThat(antwortList).hasSize(databaseSizeBeforeCreate + 1);
        Antwort testAntwort = antwortList.get(antwortList.size() - 1);
        assertThat(testAntwort.getAntwortID()).isEqualTo(DEFAULT_ANTWORT_ID);
        assertThat(testAntwort.getPosition()).isEqualTo(DEFAULT_POSITION);
        assertThat(testAntwort.isWahr()).isEqualTo(DEFAULT_WAHR);
        assertThat(testAntwort.getText()).isEqualTo(DEFAULT_TEXT);
        assertThat(testAntwort.getBild()).isEqualTo(DEFAULT_BILD);
        assertThat(testAntwort.getBildContentType()).isEqualTo(DEFAULT_BILD_CONTENT_TYPE);

        // Validate the Antwort in Elasticsearch
        verify(mockAntwortSearchRepository, times(1)).save(testAntwort);
    }

    @Test
    @Transactional
    public void createAntwortWithExistingId() throws Exception {
        int databaseSizeBeforeCreate = antwortRepository.findAll().size();

        // Create the Antwort with an existing ID
        antwort.setId(1L);

        // An entity with an existing ID cannot be created, so this API call must fail
        restAntwortMockMvc.perform(post("/api/antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(antwort)))
            .andExpect(status().isBadRequest());

        // Validate the Antwort in the database
        List<Antwort> antwortList = antwortRepository.findAll();
        assertThat(antwortList).hasSize(databaseSizeBeforeCreate);

        // Validate the Antwort in Elasticsearch
        verify(mockAntwortSearchRepository, times(0)).save(antwort);
    }

    @Test
    @Transactional
    public void getAllAntworts() throws Exception {
        // Initialize the database
        antwortRepository.saveAndFlush(antwort);

        // Get all the antwortList
        restAntwortMockMvc.perform(get("/api/antworts?sort=id,desc"))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(antwort.getId().intValue())))
            .andExpect(jsonPath("$.[*].antwortID").value(hasItem(DEFAULT_ANTWORT_ID.intValue())))
            .andExpect(jsonPath("$.[*].position").value(hasItem(DEFAULT_POSITION.intValue())))
            .andExpect(jsonPath("$.[*].wahr").value(hasItem(DEFAULT_WAHR.booleanValue())))
            .andExpect(jsonPath("$.[*].text").value(hasItem(DEFAULT_TEXT.toString())))
            .andExpect(jsonPath("$.[*].bildContentType").value(hasItem(DEFAULT_BILD_CONTENT_TYPE)))
            .andExpect(jsonPath("$.[*].bild").value(hasItem(Base64Utils.encodeToString(DEFAULT_BILD))));
    }
    
    @Test
    @Transactional
    public void getAntwort() throws Exception {
        // Initialize the database
        antwortRepository.saveAndFlush(antwort);

        // Get the antwort
        restAntwortMockMvc.perform(get("/api/antworts/{id}", antwort.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.id").value(antwort.getId().intValue()))
            .andExpect(jsonPath("$.antwortID").value(DEFAULT_ANTWORT_ID.intValue()))
            .andExpect(jsonPath("$.position").value(DEFAULT_POSITION.intValue()))
            .andExpect(jsonPath("$.wahr").value(DEFAULT_WAHR.booleanValue()))
            .andExpect(jsonPath("$.text").value(DEFAULT_TEXT.toString()))
            .andExpect(jsonPath("$.bildContentType").value(DEFAULT_BILD_CONTENT_TYPE))
            .andExpect(jsonPath("$.bild").value(Base64Utils.encodeToString(DEFAULT_BILD)));
    }

    @Test
    @Transactional
    public void getNonExistingAntwort() throws Exception {
        // Get the antwort
        restAntwortMockMvc.perform(get("/api/antworts/{id}", Long.MAX_VALUE))
            .andExpect(status().isNotFound());
    }

    @Test
    @Transactional
    public void updateAntwort() throws Exception {
        // Initialize the database
        antwortRepository.saveAndFlush(antwort);

        int databaseSizeBeforeUpdate = antwortRepository.findAll().size();

        // Update the antwort
        Antwort updatedAntwort = antwortRepository.findById(antwort.getId()).get();
        // Disconnect from session so that the updates on updatedAntwort are not directly saved in db
        em.detach(updatedAntwort);
        updatedAntwort
            .antwortID(UPDATED_ANTWORT_ID)
            .position(UPDATED_POSITION)
            .wahr(UPDATED_WAHR)
            .text(UPDATED_TEXT)
            .bild(UPDATED_BILD)
            .bildContentType(UPDATED_BILD_CONTENT_TYPE);

        restAntwortMockMvc.perform(put("/api/antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(updatedAntwort)))
            .andExpect(status().isOk());

        // Validate the Antwort in the database
        List<Antwort> antwortList = antwortRepository.findAll();
        assertThat(antwortList).hasSize(databaseSizeBeforeUpdate);
        Antwort testAntwort = antwortList.get(antwortList.size() - 1);
        assertThat(testAntwort.getAntwortID()).isEqualTo(UPDATED_ANTWORT_ID);
        assertThat(testAntwort.getPosition()).isEqualTo(UPDATED_POSITION);
        assertThat(testAntwort.isWahr()).isEqualTo(UPDATED_WAHR);
        assertThat(testAntwort.getText()).isEqualTo(UPDATED_TEXT);
        assertThat(testAntwort.getBild()).isEqualTo(UPDATED_BILD);
        assertThat(testAntwort.getBildContentType()).isEqualTo(UPDATED_BILD_CONTENT_TYPE);

        // Validate the Antwort in Elasticsearch
        verify(mockAntwortSearchRepository, times(1)).save(testAntwort);
    }

    @Test
    @Transactional
    public void updateNonExistingAntwort() throws Exception {
        int databaseSizeBeforeUpdate = antwortRepository.findAll().size();

        // Create the Antwort

        // If the entity doesn't have an ID, it will throw BadRequestAlertException
        restAntwortMockMvc.perform(put("/api/antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(antwort)))
            .andExpect(status().isBadRequest());

        // Validate the Antwort in the database
        List<Antwort> antwortList = antwortRepository.findAll();
        assertThat(antwortList).hasSize(databaseSizeBeforeUpdate);

        // Validate the Antwort in Elasticsearch
        verify(mockAntwortSearchRepository, times(0)).save(antwort);
    }

    @Test
    @Transactional
    public void deleteAntwort() throws Exception {
        // Initialize the database
        antwortRepository.saveAndFlush(antwort);

        int databaseSizeBeforeDelete = antwortRepository.findAll().size();

        // Get the antwort
        restAntwortMockMvc.perform(delete("/api/antworts/{id}", antwort.getId())
            .accept(TestUtil.APPLICATION_JSON_UTF8))
            .andExpect(status().isOk());

        // Validate the database is empty
        List<Antwort> antwortList = antwortRepository.findAll();
        assertThat(antwortList).hasSize(databaseSizeBeforeDelete - 1);

        // Validate the Antwort in Elasticsearch
        verify(mockAntwortSearchRepository, times(1)).deleteById(antwort.getId());
    }

    @Test
    @Transactional
    public void searchAntwort() throws Exception {
        // Initialize the database
        antwortRepository.saveAndFlush(antwort);
        when(mockAntwortSearchRepository.search(queryStringQuery("id:" + antwort.getId())))
            .thenReturn(Collections.singletonList(antwort));
        // Search the antwort
        restAntwortMockMvc.perform(get("/api/_search/antworts?query=id:" + antwort.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(antwort.getId().intValue())))
            .andExpect(jsonPath("$.[*].antwortID").value(hasItem(DEFAULT_ANTWORT_ID.intValue())))
            .andExpect(jsonPath("$.[*].position").value(hasItem(DEFAULT_POSITION.intValue())))
            .andExpect(jsonPath("$.[*].wahr").value(hasItem(DEFAULT_WAHR.booleanValue())))
            .andExpect(jsonPath("$.[*].text").value(hasItem(DEFAULT_TEXT.toString())))
            .andExpect(jsonPath("$.[*].bildContentType").value(hasItem(DEFAULT_BILD_CONTENT_TYPE)))
            .andExpect(jsonPath("$.[*].bild").value(hasItem(Base64Utils.encodeToString(DEFAULT_BILD))));
    }

    @Test
    @Transactional
    public void equalsVerifier() throws Exception {
        TestUtil.equalsVerifier(Antwort.class);
        Antwort antwort1 = new Antwort();
        antwort1.setId(1L);
        Antwort antwort2 = new Antwort();
        antwort2.setId(antwort1.getId());
        assertThat(antwort1).isEqualTo(antwort2);
        antwort2.setId(2L);
        assertThat(antwort1).isNotEqualTo(antwort2);
        antwort1.setId(null);
        assertThat(antwort1).isNotEqualTo(antwort2);
    }
}
