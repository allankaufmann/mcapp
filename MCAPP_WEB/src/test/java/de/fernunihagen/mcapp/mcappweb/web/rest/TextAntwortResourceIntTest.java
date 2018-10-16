package de.fernunihagen.mcapp.mcappweb.web.rest;

import de.fernunihagen.mcapp.mcappweb.McappWebApp;

import de.fernunihagen.mcapp.mcappweb.domain.TextAntwort;
import de.fernunihagen.mcapp.mcappweb.repository.TextAntwortRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.TextAntwortSearchRepository;
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
 * Test class for the TextAntwortResource REST controller.
 *
 * @see TextAntwortResource
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes = McappWebApp.class)
public class TextAntwortResourceIntTest {

    private static final Long DEFAULT_POSITION = 1L;
    private static final Long UPDATED_POSITION = 2L;

    private static final Boolean DEFAULT_WAHR = false;
    private static final Boolean UPDATED_WAHR = true;

    private static final String DEFAULT_TEXT = "AAAAAAAAAA";
    private static final String UPDATED_TEXT = "BBBBBBBBBB";

    @Autowired
    private TextAntwortRepository textAntwortRepository;

    /**
     * This repository is mocked in the de.fernunihagen.mcapp.mcappweb.repository.search test package.
     *
     * @see de.fernunihagen.mcapp.mcappweb.repository.search.TextAntwortSearchRepositoryMockConfiguration
     */
    @Autowired
    private TextAntwortSearchRepository mockTextAntwortSearchRepository;

    @Autowired
    private MappingJackson2HttpMessageConverter jacksonMessageConverter;

    @Autowired
    private PageableHandlerMethodArgumentResolver pageableArgumentResolver;

    @Autowired
    private ExceptionTranslator exceptionTranslator;

    @Autowired
    private EntityManager em;

    private MockMvc restTextAntwortMockMvc;

    private TextAntwort textAntwort;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        final TextAntwortResource textAntwortResource = new TextAntwortResource(textAntwortRepository, mockTextAntwortSearchRepository);
        this.restTextAntwortMockMvc = MockMvcBuilders.standaloneSetup(textAntwortResource)
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
    public static TextAntwort createEntity(EntityManager em) {
        TextAntwort textAntwort = new TextAntwort()
            .position(DEFAULT_POSITION)
            .wahr(DEFAULT_WAHR)
            .text(DEFAULT_TEXT);
        return textAntwort;
    }

    @Before
    public void initTest() {
        textAntwort = createEntity(em);
    }

    @Test
    @Transactional
    public void createTextAntwort() throws Exception {
        int databaseSizeBeforeCreate = textAntwortRepository.findAll().size();

        // Create the TextAntwort
        restTextAntwortMockMvc.perform(post("/api/text-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(textAntwort)))
            .andExpect(status().isCreated());

        // Validate the TextAntwort in the database
        List<TextAntwort> textAntwortList = textAntwortRepository.findAll();
        assertThat(textAntwortList).hasSize(databaseSizeBeforeCreate + 1);
        TextAntwort testTextAntwort = textAntwortList.get(textAntwortList.size() - 1);
        assertThat(testTextAntwort.getPosition()).isEqualTo(DEFAULT_POSITION);
        assertThat(testTextAntwort.isWahr()).isEqualTo(DEFAULT_WAHR);
        assertThat(testTextAntwort.getText()).isEqualTo(DEFAULT_TEXT);

        // Validate the TextAntwort in Elasticsearch
        verify(mockTextAntwortSearchRepository, times(1)).save(testTextAntwort);
    }

    @Test
    @Transactional
    public void createTextAntwortWithExistingId() throws Exception {
        int databaseSizeBeforeCreate = textAntwortRepository.findAll().size();

        // Create the TextAntwort with an existing ID
        textAntwort.setId(1L);

        // An entity with an existing ID cannot be created, so this API call must fail
        restTextAntwortMockMvc.perform(post("/api/text-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(textAntwort)))
            .andExpect(status().isBadRequest());

        // Validate the TextAntwort in the database
        List<TextAntwort> textAntwortList = textAntwortRepository.findAll();
        assertThat(textAntwortList).hasSize(databaseSizeBeforeCreate);

        // Validate the TextAntwort in Elasticsearch
        verify(mockTextAntwortSearchRepository, times(0)).save(textAntwort);
    }

    @Test
    @Transactional
    public void getAllTextAntworts() throws Exception {
        // Initialize the database
        textAntwortRepository.saveAndFlush(textAntwort);

        // Get all the textAntwortList
        restTextAntwortMockMvc.perform(get("/api/text-antworts?sort=id,desc"))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(textAntwort.getId().intValue())))
            .andExpect(jsonPath("$.[*].position").value(hasItem(DEFAULT_POSITION.intValue())))
            .andExpect(jsonPath("$.[*].wahr").value(hasItem(DEFAULT_WAHR.booleanValue())))
            .andExpect(jsonPath("$.[*].text").value(hasItem(DEFAULT_TEXT.toString())));
    }
    
    @Test
    @Transactional
    public void getTextAntwort() throws Exception {
        // Initialize the database
        textAntwortRepository.saveAndFlush(textAntwort);

        // Get the textAntwort
        restTextAntwortMockMvc.perform(get("/api/text-antworts/{id}", textAntwort.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.id").value(textAntwort.getId().intValue()))
            .andExpect(jsonPath("$.position").value(DEFAULT_POSITION.intValue()))
            .andExpect(jsonPath("$.wahr").value(DEFAULT_WAHR.booleanValue()))
            .andExpect(jsonPath("$.text").value(DEFAULT_TEXT.toString()));
    }

    @Test
    @Transactional
    public void getNonExistingTextAntwort() throws Exception {
        // Get the textAntwort
        restTextAntwortMockMvc.perform(get("/api/text-antworts/{id}", Long.MAX_VALUE))
            .andExpect(status().isNotFound());
    }

    @Test
    @Transactional
    public void updateTextAntwort() throws Exception {
        // Initialize the database
        textAntwortRepository.saveAndFlush(textAntwort);

        int databaseSizeBeforeUpdate = textAntwortRepository.findAll().size();

        // Update the textAntwort
        TextAntwort updatedTextAntwort = textAntwortRepository.findById(textAntwort.getId()).get();
        // Disconnect from session so that the updates on updatedTextAntwort are not directly saved in db
        em.detach(updatedTextAntwort);
        updatedTextAntwort
            .position(UPDATED_POSITION)
            .wahr(UPDATED_WAHR)
            .text(UPDATED_TEXT);

        restTextAntwortMockMvc.perform(put("/api/text-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(updatedTextAntwort)))
            .andExpect(status().isOk());

        // Validate the TextAntwort in the database
        List<TextAntwort> textAntwortList = textAntwortRepository.findAll();
        assertThat(textAntwortList).hasSize(databaseSizeBeforeUpdate);
        TextAntwort testTextAntwort = textAntwortList.get(textAntwortList.size() - 1);
        assertThat(testTextAntwort.getPosition()).isEqualTo(UPDATED_POSITION);
        assertThat(testTextAntwort.isWahr()).isEqualTo(UPDATED_WAHR);
        assertThat(testTextAntwort.getText()).isEqualTo(UPDATED_TEXT);

        // Validate the TextAntwort in Elasticsearch
        verify(mockTextAntwortSearchRepository, times(1)).save(testTextAntwort);
    }

    @Test
    @Transactional
    public void updateNonExistingTextAntwort() throws Exception {
        int databaseSizeBeforeUpdate = textAntwortRepository.findAll().size();

        // Create the TextAntwort

        // If the entity doesn't have an ID, it will throw BadRequestAlertException
        restTextAntwortMockMvc.perform(put("/api/text-antworts")
            .contentType(TestUtil.APPLICATION_JSON_UTF8)
            .content(TestUtil.convertObjectToJsonBytes(textAntwort)))
            .andExpect(status().isBadRequest());

        // Validate the TextAntwort in the database
        List<TextAntwort> textAntwortList = textAntwortRepository.findAll();
        assertThat(textAntwortList).hasSize(databaseSizeBeforeUpdate);

        // Validate the TextAntwort in Elasticsearch
        verify(mockTextAntwortSearchRepository, times(0)).save(textAntwort);
    }

    @Test
    @Transactional
    public void deleteTextAntwort() throws Exception {
        // Initialize the database
        textAntwortRepository.saveAndFlush(textAntwort);

        int databaseSizeBeforeDelete = textAntwortRepository.findAll().size();

        // Get the textAntwort
        restTextAntwortMockMvc.perform(delete("/api/text-antworts/{id}", textAntwort.getId())
            .accept(TestUtil.APPLICATION_JSON_UTF8))
            .andExpect(status().isOk());

        // Validate the database is empty
        List<TextAntwort> textAntwortList = textAntwortRepository.findAll();
        assertThat(textAntwortList).hasSize(databaseSizeBeforeDelete - 1);

        // Validate the TextAntwort in Elasticsearch
        verify(mockTextAntwortSearchRepository, times(1)).deleteById(textAntwort.getId());
    }

    @Test
    @Transactional
    public void searchTextAntwort() throws Exception {
        // Initialize the database
        textAntwortRepository.saveAndFlush(textAntwort);
        when(mockTextAntwortSearchRepository.search(queryStringQuery("id:" + textAntwort.getId())))
            .thenReturn(Collections.singletonList(textAntwort));
        // Search the textAntwort
        restTextAntwortMockMvc.perform(get("/api/_search/text-antworts?query=id:" + textAntwort.getId()))
            .andExpect(status().isOk())
            .andExpect(content().contentType(MediaType.APPLICATION_JSON_UTF8_VALUE))
            .andExpect(jsonPath("$.[*].id").value(hasItem(textAntwort.getId().intValue())))
            .andExpect(jsonPath("$.[*].position").value(hasItem(DEFAULT_POSITION.intValue())))
            .andExpect(jsonPath("$.[*].wahr").value(hasItem(DEFAULT_WAHR.booleanValue())))
            .andExpect(jsonPath("$.[*].text").value(hasItem(DEFAULT_TEXT.toString())));
    }

    @Test
    @Transactional
    public void equalsVerifier() throws Exception {
        TestUtil.equalsVerifier(TextAntwort.class);
        TextAntwort textAntwort1 = new TextAntwort();
        textAntwort1.setId(1L);
        TextAntwort textAntwort2 = new TextAntwort();
        textAntwort2.setId(textAntwort1.getId());
        assertThat(textAntwort1).isEqualTo(textAntwort2);
        textAntwort2.setId(2L);
        assertThat(textAntwort1).isNotEqualTo(textAntwort2);
        textAntwort1.setId(null);
        assertThat(textAntwort1).isNotEqualTo(textAntwort2);
    }
}
