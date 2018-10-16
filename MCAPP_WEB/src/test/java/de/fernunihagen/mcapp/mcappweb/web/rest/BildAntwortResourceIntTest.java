package de.fernunihagen.mcapp.mcappweb.web.rest;

import de.fernunihagen.mcapp.mcappweb.McappWebApp;

import de.fernunihagen.mcapp.mcappweb.domain.BildAntwort;
import de.fernunihagen.mcapp.mcappweb.repository.BildAntwortRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.BildAntwortSearchRepository;
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
 * Test class for the BildAntwortResource REST controller.
 *
 * @see BildAntwortResource
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes = McappWebApp.class)
public class BildAntwortResourceIntTest {

    private static final Long DEFAULT_POSITION = 1L;
    private static final Long UPDATED_POSITION = 2L;

    private static final Boolean DEFAULT_WAHR = false;
    private static final Boolean UPDATED_WAHR = true;

    private static final byte[] DEFAULT_BILD = TestUtil.createByteArray(1, "0");
    private static final byte[] UPDATED_BILD = TestUtil.createByteArray(1, "1");
    private static final String DEFAULT_BILD_CONTENT_TYPE = "image/jpg";
    private static final String UPDATED_BILD_CONTENT_TYPE = "image/png";

    @Autowired
    private BildAntwortRepository bildAntwortRepository;

    /**
     * This repository is mocked in the de.fernunihagen.mcapp.mcappweb.repository.search test package.
     *
     * @see de.fernunihagen.mcapp.mcappweb.repository.search.BildAntwortSearchRepositoryMockConfiguration
     */
    @Autowired
    private BildAntwortSearchRepository mockBildAntwortSearchRepository;

    @Autowired
    private MappingJackson2HttpMessageConverter jacksonMessageConverter;

    @Autowired
    private PageableHandlerMethodArgumentResolver pageableArgumentResolver;

    @Autowired
    private ExceptionTranslator exceptionTranslator;

    @Autowired
    private EntityManager em;

    private MockMvc restBildAntwortMockMvc;

    private BildAntwort bildAntwort;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        final BildAntwortResource bildAntwortResource = new BildAntwortResource(bildAntwortRepository, mockBildAntwortSearchRepository);
        this.restBildAntwortMockMvc = MockMvcBuilders.standaloneSetup(bildAntwortResource)
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
    public static BildAntwort createEntity(EntityManager em) {
        BildAntwort bildAntwort = new BildAntwort()
            .position(DEFAULT_POSITION)
            .wahr(DEFAULT_WAHR)
            .bild(DEFAULT_BILD)
            .bildContentType(DEFAULT_BILD_CONTENT_TYPE);
        return bildAntwort;
    }

    @Before
    public void initTest() {
        bildAntwort = createEntity(em);
    }

    @Test
    @Transactional
    public void createBildAntwort() throws Exception {
        int databaseSizeBeforeCreate = bildAntwortRepository.findAll().size();

        // Create the BildAntwort
        restBildAntwortMockMvc.perform(post("/api/bild-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(bildAntwort)))
            .andExpect(status().isCreated());

        // Validate the BildAntwort in the database
        List<BildAntwort> bildAntwortList = bildAntwortRepository.findAll();
        assertThat(bildAntwortList).hasSize(databaseSizeBeforeCreate + 1);
        BildAntwort testBildAntwort = bildAntwortList.get(bildAntwortList.size() - 1);
        assertThat(testBildAntwort.getPosition()).isEqualTo(DEFAULT_POSITION);
        assertThat(testBildAntwort.isWahr()).isEqualTo(DEFAULT_WAHR);
        assertThat(testBildAntwort.getBild()).isEqualTo(DEFAULT_BILD);
        assertThat(testBildAntwort.getBildContentType()).isEqualTo(DEFAULT_BILD_CONTENT_TYPE);

        // Validate the BildAntwort in Elasticsearch
        verify(mockBildAntwortSearchRepository, times(1)).save(testBildAntwort);
    }

    @Test
    @Transactional
    public void createBildAntwortWithExistingId() throws Exception {
        int databaseSizeBeforeCreate = bildAntwortRepository.findAll().size();

        // Create the BildAntwort with an existing ID
        bildAntwort.setId(1L);

        // An entity with an existing ID cannot be created, so this API call must fail
        restBildAntwortMockMvc.perform(post("/api/bild-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(bildAntwort)))
            .andExpect(status().isBadRequest());

        // Validate the BildAntwort in the database
        List<BildAntwort> bildAntwortList = bildAntwortRepository.findAll();
        assertThat(bildAntwortList).hasSize(databaseSizeBeforeCreate);

        // Validate the BildAntwort in Elasticsearch
        verify(mockBildAntwortSearchRepository, times(0)).save(bildAntwort);
    }

    @Test
    @Transactional
    public void getAllBildAntworts() throws Exception {
        // Initialize the database
        bildAntwortRepository.saveAndFlush(bildAntwort);

        // Get all the bildAntwortList
        restBildAntwortMockMvc.perform(get("/api/bild-antworts?sort=id,desc"))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(bildAntwort.getId().intValue())))
            .andExpect(jsonPath("$.[*].position").value(hasItem(DEFAULT_POSITION.intValue())))
            .andExpect(jsonPath("$.[*].wahr").value(hasItem(DEFAULT_WAHR.booleanValue())))
            .andExpect(jsonPath("$.[*].bildContentType").value(hasItem(DEFAULT_BILD_CONTENT_TYPE)))
            .andExpect(jsonPath("$.[*].bild").value(hasItem(Base64Utils.encodeToString(DEFAULT_BILD))));
    }
    
    @Test
    @Transactional
    public void getBildAntwort() throws Exception {
        // Initialize the database
        bildAntwortRepository.saveAndFlush(bildAntwort);

        // Get the bildAntwort
        restBildAntwortMockMvc.perform(get("/api/bild-antworts/{id}", bildAntwort.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.id").value(bildAntwort.getId().intValue()))
            .andExpect(jsonPath("$.position").value(DEFAULT_POSITION.intValue()))
            .andExpect(jsonPath("$.wahr").value(DEFAULT_WAHR.booleanValue()))
            .andExpect(jsonPath("$.bildContentType").value(DEFAULT_BILD_CONTENT_TYPE))
            .andExpect(jsonPath("$.bild").value(Base64Utils.encodeToString(DEFAULT_BILD)));
    }

    @Test
    @Transactional
    public void getNonExistingBildAntwort() throws Exception {
        // Get the bildAntwort
        restBildAntwortMockMvc.perform(get("/api/bild-antworts/{id}", Long.MAX_VALUE))
            .andExpect(status().isNotFound());
    }

    @Test
    @Transactional
    public void updateBildAntwort() throws Exception {
        // Initialize the database
        bildAntwortRepository.saveAndFlush(bildAntwort);

        int databaseSizeBeforeUpdate = bildAntwortRepository.findAll().size();

        // Update the bildAntwort
        BildAntwort updatedBildAntwort = bildAntwortRepository.findById(bildAntwort.getId()).get();
        // Disconnect from session so that the updates on updatedBildAntwort are not directly saved in db
        em.detach(updatedBildAntwort);
        updatedBildAntwort
            .position(UPDATED_POSITION)
            .wahr(UPDATED_WAHR)
            .bild(UPDATED_BILD)
            .bildContentType(UPDATED_BILD_CONTENT_TYPE);

        restBildAntwortMockMvc.perform(put("/api/bild-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(updatedBildAntwort)))
            .andExpect(status().isOk());

        // Validate the BildAntwort in the database
        List<BildAntwort> bildAntwortList = bildAntwortRepository.findAll();
        assertThat(bildAntwortList).hasSize(databaseSizeBeforeUpdate);
        BildAntwort testBildAntwort = bildAntwortList.get(bildAntwortList.size() - 1);
        assertThat(testBildAntwort.getPosition()).isEqualTo(UPDATED_POSITION);
        assertThat(testBildAntwort.isWahr()).isEqualTo(UPDATED_WAHR);
        assertThat(testBildAntwort.getBild()).isEqualTo(UPDATED_BILD);
        assertThat(testBildAntwort.getBildContentType()).isEqualTo(UPDATED_BILD_CONTENT_TYPE);

        // Validate the BildAntwort in Elasticsearch
        verify(mockBildAntwortSearchRepository, times(1)).save(testBildAntwort);
    }

    @Test
    @Transactional
    public void updateNonExistingBildAntwort() throws Exception {
        int databaseSizeBeforeUpdate = bildAntwortRepository.findAll().size();

        // Create the BildAntwort

        // If the entity doesn't have an ID, it will throw BadRequestAlertException
        restBildAntwortMockMvc.perform(put("/api/bild-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(bildAntwort)))
            .andExpect(status().isBadRequest());

        // Validate the BildAntwort in the database
        List<BildAntwort> bildAntwortList = bildAntwortRepository.findAll();
        assertThat(bildAntwortList).hasSize(databaseSizeBeforeUpdate);

        // Validate the BildAntwort in Elasticsearch
        verify(mockBildAntwortSearchRepository, times(0)).save(bildAntwort);
    }

    @Test
    @Transactional
    public void deleteBildAntwort() throws Exception {
        // Initialize the database
        bildAntwortRepository.saveAndFlush(bildAntwort);

        int databaseSizeBeforeDelete = bildAntwortRepository.findAll().size();

        // Get the bildAntwort
        restBildAntwortMockMvc.perform(delete("/api/bild-antworts/{id}", bildAntwort.getId())
            .accept(TestUtil.APPLICATION_JSON_UTF8))
            .andExpect(status().isOk());

        // Validate the database is empty
        List<BildAntwort> bildAntwortList = bildAntwortRepository.findAll();
        assertThat(bildAntwortList).hasSize(databaseSizeBeforeDelete - 1);

        // Validate the BildAntwort in Elasticsearch
        verify(mockBildAntwortSearchRepository, times(1)).deleteById(bildAntwort.getId());
    }

    @Test
    @Transactional
    public void searchBildAntwort() throws Exception {
        // Initialize the database
        bildAntwortRepository.saveAndFlush(bildAntwort);
        when(mockBildAntwortSearchRepository.search(queryStringQuery("id:" + bildAntwort.getId())))
            .thenReturn(Collections.singletonList(bildAntwort));
        // Search the bildAntwort
        restBildAntwortMockMvc.perform(get("/api/_search/bild-antworts?query=id:" + bildAntwort.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(bildAntwort.getId().intValue())))
            .andExpect(jsonPath("$.[*].position").value(hasItem(DEFAULT_POSITION.intValue())))
            .andExpect(jsonPath("$.[*].wahr").value(hasItem(DEFAULT_WAHR.booleanValue())))
            .andExpect(jsonPath("$.[*].bildContentType").value(hasItem(DEFAULT_BILD_CONTENT_TYPE)))
            .andExpect(jsonPath("$.[*].bild").value(hasItem(Base64Utils.encodeToString(DEFAULT_BILD))));
    }

    @Test
    @Transactional
    public void equalsVerifier() throws Exception {
        TestUtil.equalsVerifier(BildAntwort.class);
        BildAntwort bildAntwort1 = new BildAntwort();
        bildAntwort1.setId(1L);
        BildAntwort bildAntwort2 = new BildAntwort();
        bildAntwort2.setId(bildAntwort1.getId());
        assertThat(bildAntwort1).isEqualTo(bildAntwort2);
        bildAntwort2.setId(2L);
        assertThat(bildAntwort1).isNotEqualTo(bildAntwort2);
        bildAntwort1.setId(null);
        assertThat(bildAntwort1).isNotEqualTo(bildAntwort2);
    }
}
