package de.fernunihagen.mcapp.mcappweb.web.rest;

import de.fernunihagen.mcapp.mcappweb.McappWebApp;

import de.fernunihagen.mcapp.mcappweb.domain.Frage;
import de.fernunihagen.mcapp.mcappweb.repository.FrageRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.FrageSearchRepository;
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

import de.fernunihagen.mcapp.mcappweb.domain.enumeration.Fragetyp;
/**
 * Test class for the FrageResource REST controller.
 *
 * @see FrageResource
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes = McappWebApp.class)
public class FrageResourceIntTest {

    private static final Long DEFAULT_FRAGE_ID = 1L;
    private static final Long UPDATED_FRAGE_ID = 2L;

    private static final Fragetyp DEFAULT_FRAGE_TYP = Fragetyp.TEXT;
    private static final Fragetyp UPDATED_FRAGE_TYP = Fragetyp.BILD;

    private static final Long DEFAULT_THEMA_ID = 1L;
    private static final Long UPDATED_THEMA_ID = 2L;

    @Autowired
    private FrageRepository frageRepository;

    /**
     * This repository is mocked in the de.fernunihagen.mcapp.mcappweb.repository.search test package.
     *
     * @see de.fernunihagen.mcapp.mcappweb.repository.search.FrageSearchRepositoryMockConfiguration
     */
    @Autowired
    private FrageSearchRepository mockFrageSearchRepository;

    @Autowired
    private MappingJackson2HttpMessageConverter jacksonMessageConverter;

    @Autowired
    private PageableHandlerMethodArgumentResolver pageableArgumentResolver;

    @Autowired
    private ExceptionTranslator exceptionTranslator;

    @Autowired
    private EntityManager em;

    private MockMvc restFrageMockMvc;

    private Frage frage;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        final FrageResource frageResource = new FrageResource(frageRepository, mockFrageSearchRepository);
        this.restFrageMockMvc = MockMvcBuilders.standaloneSetup(frageResource)
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
    public static Frage createEntity(EntityManager em) {
        Frage frage = new Frage()
            .frageID(DEFAULT_FRAGE_ID)
            .frageTyp(DEFAULT_FRAGE_TYP)
            .themaID(DEFAULT_THEMA_ID);
        return frage;
    }

    @Before
    public void initTest() {
        frage = createEntity(em);
    }

    @Test
    @Transactional
    public void createFrage() throws Exception {
        int databaseSizeBeforeCreate = frageRepository.findAll().size();

        // Create the Frage
        restFrageMockMvc.perform(post("/api/frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(frage)))
            .andExpect(status().isCreated());

        // Validate the Frage in the database
        List<Frage> frageList = frageRepository.findAll();
        assertThat(frageList).hasSize(databaseSizeBeforeCreate + 1);
        Frage testFrage = frageList.get(frageList.size() - 1);
        assertThat(testFrage.getFrageID()).isEqualTo(DEFAULT_FRAGE_ID);
        assertThat(testFrage.getFrageTyp()).isEqualTo(DEFAULT_FRAGE_TYP);
        assertThat(testFrage.getThemaID()).isEqualTo(DEFAULT_THEMA_ID);

        // Validate the Frage in Elasticsearch
        verify(mockFrageSearchRepository, times(1)).save(testFrage);
    }

    @Test
    @Transactional
    public void createFrageWithExistingId() throws Exception {
        int databaseSizeBeforeCreate = frageRepository.findAll().size();

        // Create the Frage with an existing ID
        frage.setId(1L);

        // An entity with an existing ID cannot be created, so this API call must fail
        restFrageMockMvc.perform(post("/api/frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(frage)))
            .andExpect(status().isBadRequest());

        // Validate the Frage in the database
        List<Frage> frageList = frageRepository.findAll();
        assertThat(frageList).hasSize(databaseSizeBeforeCreate);

        // Validate the Frage in Elasticsearch
        verify(mockFrageSearchRepository, times(0)).save(frage);
    }

    @Test
    @Transactional
    public void getAllFrages() throws Exception {
        // Initialize the database
        frageRepository.saveAndFlush(frage);

        // Get all the frageList
        restFrageMockMvc.perform(get("/api/frages?sort=id,desc"))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(frage.getId().intValue())))
            .andExpect(jsonPath("$.[*].frageID").value(hasItem(DEFAULT_FRAGE_ID.intValue())))
            .andExpect(jsonPath("$.[*].frageTyp").value(hasItem(DEFAULT_FRAGE_TYP.toString())))
            .andExpect(jsonPath("$.[*].themaID").value(hasItem(DEFAULT_THEMA_ID.intValue())));
    }
    
    @Test
    @Transactional
    public void getFrage() throws Exception {
        // Initialize the database
        frageRepository.saveAndFlush(frage);

        // Get the frage
        restFrageMockMvc.perform(get("/api/frages/{id}", frage.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.id").value(frage.getId().intValue()))
            .andExpect(jsonPath("$.frageID").value(DEFAULT_FRAGE_ID.intValue()))
            .andExpect(jsonPath("$.frageTyp").value(DEFAULT_FRAGE_TYP.toString()))
            .andExpect(jsonPath("$.themaID").value(DEFAULT_THEMA_ID.intValue()));
    }

    @Test
    @Transactional
    public void getNonExistingFrage() throws Exception {
        // Get the frage
        restFrageMockMvc.perform(get("/api/frages/{id}", Long.MAX_VALUE))
            .andExpect(status().isNotFound());
    }

    @Test
    @Transactional
    public void updateFrage() throws Exception {
        // Initialize the database
        frageRepository.saveAndFlush(frage);

        int databaseSizeBeforeUpdate = frageRepository.findAll().size();

        // Update the frage
        Frage updatedFrage = frageRepository.findById(frage.getId()).get();
        // Disconnect from session so that the updates on updatedFrage are not directly saved in db
        em.detach(updatedFrage);
        updatedFrage
            .frageID(UPDATED_FRAGE_ID)
            .frageTyp(UPDATED_FRAGE_TYP)
            .themaID(UPDATED_THEMA_ID);

        restFrageMockMvc.perform(put("/api/frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(updatedFrage)))
            .andExpect(status().isOk());

        // Validate the Frage in the database
        List<Frage> frageList = frageRepository.findAll();
        assertThat(frageList).hasSize(databaseSizeBeforeUpdate);
        Frage testFrage = frageList.get(frageList.size() - 1);
        assertThat(testFrage.getFrageID()).isEqualTo(UPDATED_FRAGE_ID);
        assertThat(testFrage.getFrageTyp()).isEqualTo(UPDATED_FRAGE_TYP);
        assertThat(testFrage.getThemaID()).isEqualTo(UPDATED_THEMA_ID);

        // Validate the Frage in Elasticsearch
        verify(mockFrageSearchRepository, times(1)).save(testFrage);
    }

    @Test
    @Transactional
    public void updateNonExistingFrage() throws Exception {
        int databaseSizeBeforeUpdate = frageRepository.findAll().size();

        // Create the Frage

        // If the entity doesn't have an ID, it will throw BadRequestAlertException
        restFrageMockMvc.perform(put("/api/frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(frage)))
            .andExpect(status().isBadRequest());

        // Validate the Frage in the database
        List<Frage> frageList = frageRepository.findAll();
        assertThat(frageList).hasSize(databaseSizeBeforeUpdate);

        // Validate the Frage in Elasticsearch
        verify(mockFrageSearchRepository, times(0)).save(frage);
    }

    @Test
    @Transactional
    public void deleteFrage() throws Exception {
        // Initialize the database
        frageRepository.saveAndFlush(frage);

        int databaseSizeBeforeDelete = frageRepository.findAll().size();

        // Get the frage
        restFrageMockMvc.perform(delete("/api/frages/{id}", frage.getId())
            .accept(TestUtil.APPLICATION_JSON_UTF8))
            .andExpect(status().isOk());

        // Validate the database is empty
        List<Frage> frageList = frageRepository.findAll();
        assertThat(frageList).hasSize(databaseSizeBeforeDelete - 1);

        // Validate the Frage in Elasticsearch
        verify(mockFrageSearchRepository, times(1)).deleteById(frage.getId());
    }

    @Test
    @Transactional
    public void searchFrage() throws Exception {
        // Initialize the database
        frageRepository.saveAndFlush(frage);
        when(mockFrageSearchRepository.search(queryStringQuery("id:" + frage.getId())))
            .thenReturn(Collections.singletonList(frage));
        // Search the frage
        restFrageMockMvc.perform(get("/api/_search/frages?query=id:" + frage.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(frage.getId().intValue())))
            .andExpect(jsonPath("$.[*].frageID").value(hasItem(DEFAULT_FRAGE_ID.intValue())))
            .andExpect(jsonPath("$.[*].frageTyp").value(hasItem(DEFAULT_FRAGE_TYP.toString())))
            .andExpect(jsonPath("$.[*].themaID").value(hasItem(DEFAULT_THEMA_ID.intValue())));
    }

    @Test
    @Transactional
    public void equalsVerifier() throws Exception {
        TestUtil.equalsVerifier(Frage.class);
        Frage frage1 = new Frage();
        frage1.setId(1L);
        Frage frage2 = new Frage();
        frage2.setId(frage1.getId());
        assertThat(frage1).isEqualTo(frage2);
        frage2.setId(2L);
        assertThat(frage1).isNotEqualTo(frage2);
        frage1.setId(null);
        assertThat(frage1).isNotEqualTo(frage2);
    }
}
