package de.fernunihagen.mcapp.mcappweb.web.rest;

import de.fernunihagen.mcapp.mcappweb.McappWebApp;

import de.fernunihagen.mcapp.mcappweb.domain.QuizFrage;
import de.fernunihagen.mcapp.mcappweb.repository.QuizFrageRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.QuizFrageSearchRepository;
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
 * Test class for the QuizFrageResource REST controller.
 *
 * @see QuizFrageResource
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes = McappWebApp.class)
public class QuizFrageResourceIntTest {

    private static final Boolean DEFAULT_RICHTIG = false;
    private static final Boolean UPDATED_RICHTIG = true;

    @Autowired
    private QuizFrageRepository quizFrageRepository;

    /**
     * This repository is mocked in the de.fernunihagen.mcapp.mcappweb.repository.search test package.
     *
     * @see de.fernunihagen.mcapp.mcappweb.repository.search.QuizFrageSearchRepositoryMockConfiguration
     */
    @Autowired
    private QuizFrageSearchRepository mockQuizFrageSearchRepository;

    @Autowired
    private MappingJackson2HttpMessageConverter jacksonMessageConverter;

    @Autowired
    private PageableHandlerMethodArgumentResolver pageableArgumentResolver;

    @Autowired
    private ExceptionTranslator exceptionTranslator;

    @Autowired
    private EntityManager em;

    private MockMvc restQuizFrageMockMvc;

    private QuizFrage quizFrage;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        final QuizFrageResource quizFrageResource = new QuizFrageResource(quizFrageRepository, mockQuizFrageSearchRepository);
        this.restQuizFrageMockMvc = MockMvcBuilders.standaloneSetup(quizFrageResource)
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
    public static QuizFrage createEntity(EntityManager em) {
        QuizFrage quizFrage = new QuizFrage()
            .richtig(DEFAULT_RICHTIG);
        return quizFrage;
    }

    @Before
    public void initTest() {
        quizFrage = createEntity(em);
    }

    @Test
    @Transactional
    public void createQuizFrage() throws Exception {
        int databaseSizeBeforeCreate = quizFrageRepository.findAll().size();

        // Create the QuizFrage
        restQuizFrageMockMvc.perform(post("/api/quiz-frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(quizFrage)))
            .andExpect(status().isCreated());

        // Validate the QuizFrage in the database
        List<QuizFrage> quizFrageList = quizFrageRepository.findAll();
        assertThat(quizFrageList).hasSize(databaseSizeBeforeCreate + 1);
        QuizFrage testQuizFrage = quizFrageList.get(quizFrageList.size() - 1);
        assertThat(testQuizFrage.isRichtig()).isEqualTo(DEFAULT_RICHTIG);

        // Validate the QuizFrage in Elasticsearch
        verify(mockQuizFrageSearchRepository, times(1)).save(testQuizFrage);
    }

    @Test
    @Transactional
    public void createQuizFrageWithExistingId() throws Exception {
        int databaseSizeBeforeCreate = quizFrageRepository.findAll().size();

        // Create the QuizFrage with an existing ID
        quizFrage.setId(1L);

        // An entity with an existing ID cannot be created, so this API call must fail
        restQuizFrageMockMvc.perform(post("/api/quiz-frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(quizFrage)))
            .andExpect(status().isBadRequest());

        // Validate the QuizFrage in the database
        List<QuizFrage> quizFrageList = quizFrageRepository.findAll();
        assertThat(quizFrageList).hasSize(databaseSizeBeforeCreate);

        // Validate the QuizFrage in Elasticsearch
        verify(mockQuizFrageSearchRepository, times(0)).save(quizFrage);
    }

    @Test
    @Transactional
    public void getAllQuizFrages() throws Exception {
        // Initialize the database
        quizFrageRepository.saveAndFlush(quizFrage);

        // Get all the quizFrageList
        restQuizFrageMockMvc.perform(get("/api/quiz-frages?sort=id,desc"))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(quizFrage.getId().intValue())))
            .andExpect(jsonPath("$.[*].richtig").value(hasItem(DEFAULT_RICHTIG.booleanValue())));
    }
    
    @Test
    @Transactional
    public void getQuizFrage() throws Exception {
        // Initialize the database
        quizFrageRepository.saveAndFlush(quizFrage);

        // Get the quizFrage
        restQuizFrageMockMvc.perform(get("/api/quiz-frages/{id}", quizFrage.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.id").value(quizFrage.getId().intValue()))
            .andExpect(jsonPath("$.richtig").value(DEFAULT_RICHTIG.booleanValue()));
    }

    @Test
    @Transactional
    public void getNonExistingQuizFrage() throws Exception {
        // Get the quizFrage
        restQuizFrageMockMvc.perform(get("/api/quiz-frages/{id}", Long.MAX_VALUE))
            .andExpect(status().isNotFound());
    }

    @Test
    @Transactional
    public void updateQuizFrage() throws Exception {
        // Initialize the database
        quizFrageRepository.saveAndFlush(quizFrage);

        int databaseSizeBeforeUpdate = quizFrageRepository.findAll().size();

        // Update the quizFrage
        QuizFrage updatedQuizFrage = quizFrageRepository.findById(quizFrage.getId()).get();
        // Disconnect from session so that the updates on updatedQuizFrage are not directly saved in db
        em.detach(updatedQuizFrage);
        updatedQuizFrage
            .richtig(UPDATED_RICHTIG);

        restQuizFrageMockMvc.perform(put("/api/quiz-frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(updatedQuizFrage)))
            .andExpect(status().isOk());

        // Validate the QuizFrage in the database
        List<QuizFrage> quizFrageList = quizFrageRepository.findAll();
        assertThat(quizFrageList).hasSize(databaseSizeBeforeUpdate);
        QuizFrage testQuizFrage = quizFrageList.get(quizFrageList.size() - 1);
        assertThat(testQuizFrage.isRichtig()).isEqualTo(UPDATED_RICHTIG);

        // Validate the QuizFrage in Elasticsearch
        verify(mockQuizFrageSearchRepository, times(1)).save(testQuizFrage);
    }

    @Test
    @Transactional
    public void updateNonExistingQuizFrage() throws Exception {
        int databaseSizeBeforeUpdate = quizFrageRepository.findAll().size();

        // Create the QuizFrage

        // If the entity doesn't have an ID, it will throw BadRequestAlertException
        restQuizFrageMockMvc.perform(put("/api/quiz-frages")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(quizFrage)))
            .andExpect(status().isBadRequest());

        // Validate the QuizFrage in the database
        List<QuizFrage> quizFrageList = quizFrageRepository.findAll();
        assertThat(quizFrageList).hasSize(databaseSizeBeforeUpdate);

        // Validate the QuizFrage in Elasticsearch
        verify(mockQuizFrageSearchRepository, times(0)).save(quizFrage);
    }

    @Test
    @Transactional
    public void deleteQuizFrage() throws Exception {
        // Initialize the database
        quizFrageRepository.saveAndFlush(quizFrage);

        int databaseSizeBeforeDelete = quizFrageRepository.findAll().size();

        // Get the quizFrage
        restQuizFrageMockMvc.perform(delete("/api/quiz-frages/{id}", quizFrage.getId())
            .accept(TestUtil.APPLICATION_JSON_UTF8))
            .andExpect(status().isOk());

        // Validate the database is empty
        List<QuizFrage> quizFrageList = quizFrageRepository.findAll();
        assertThat(quizFrageList).hasSize(databaseSizeBeforeDelete - 1);

        // Validate the QuizFrage in Elasticsearch
        verify(mockQuizFrageSearchRepository, times(1)).deleteById(quizFrage.getId());
    }

    @Test
    @Transactional
    public void searchQuizFrage() throws Exception {
        // Initialize the database
        quizFrageRepository.saveAndFlush(quizFrage);
        when(mockQuizFrageSearchRepository.search(queryStringQuery("id:" + quizFrage.getId())))
            .thenReturn(Collections.singletonList(quizFrage));
        // Search the quizFrage
        restQuizFrageMockMvc.perform(get("/api/_search/quiz-frages?query=id:" + quizFrage.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(quizFrage.getId().intValue())))
            .andExpect(jsonPath("$.[*].richtig").value(hasItem(DEFAULT_RICHTIG.booleanValue())));
    }

    @Test
    @Transactional
    public void equalsVerifier() throws Exception {
        TestUtil.equalsVerifier(QuizFrage.class);
        QuizFrage quizFrage1 = new QuizFrage();
        quizFrage1.setId(1L);
        QuizFrage quizFrage2 = new QuizFrage();
        quizFrage2.setId(quizFrage1.getId());
        assertThat(quizFrage1).isEqualTo(quizFrage2);
        quizFrage2.setId(2L);
        assertThat(quizFrage1).isNotEqualTo(quizFrage2);
        quizFrage1.setId(null);
        assertThat(quizFrage1).isNotEqualTo(quizFrage2);
    }
}
