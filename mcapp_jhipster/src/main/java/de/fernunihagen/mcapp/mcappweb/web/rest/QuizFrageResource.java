package de.fernunihagen.mcapp.mcappweb.web.rest;

import com.codahale.metrics.annotation.Timed;
import de.fernunihagen.mcapp.mcappweb.domain.QuizFrage;
import de.fernunihagen.mcapp.mcappweb.repository.QuizFrageRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.QuizFrageSearchRepository;
import de.fernunihagen.mcapp.mcappweb.web.rest.errors.BadRequestAlertException;
import de.fernunihagen.mcapp.mcappweb.web.rest.util.HeaderUtil;
import io.github.jhipster.web.util.ResponseUtil;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.net.URI;
import java.net.URISyntaxException;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

import static org.elasticsearch.index.query.QueryBuilders.*;

/**
 * REST controller for managing QuizFrage.
 */
@RestController
@RequestMapping("/api")
public class QuizFrageResource {

    private final Logger log = LoggerFactory.getLogger(QuizFrageResource.class);

    private static final String ENTITY_NAME = "quizFrage";

    private final QuizFrageRepository quizFrageRepository;

    private final QuizFrageSearchRepository quizFrageSearchRepository;

    public QuizFrageResource(QuizFrageRepository quizFrageRepository, QuizFrageSearchRepository quizFrageSearchRepository) {
        this.quizFrageRepository = quizFrageRepository;
        this.quizFrageSearchRepository = quizFrageSearchRepository;
    }

    /**
     * POST  /quiz-frages : Create a new quizFrage.
     *
     * @param quizFrage the quizFrage to create
     * @return the ResponseEntity with status 201 (Created) and with body the new quizFrage, or with status 400 (Bad Request) if the quizFrage has already an ID
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PostMapping("/quiz-frages")
    @Timed
    public ResponseEntity<QuizFrage> createQuizFrage(@RequestBody QuizFrage quizFrage) throws URISyntaxException {
        log.debug("REST request to save QuizFrage : {}", quizFrage);
        if (quizFrage.getId() != null) {
            throw new BadRequestAlertException("A new quizFrage cannot already have an ID", ENTITY_NAME, "idexists");
        }
        QuizFrage result = quizFrageRepository.save(quizFrage);
        quizFrageSearchRepository.save(result);
        return ResponseEntity.created(new URI("/api/quiz-frages/" + result.getId()))
            .headers(HeaderUtil.createEntityCreationAlert(ENTITY_NAME, result.getId().toString()))
            .body(result);
    }

    /**
     * PUT  /quiz-frages : Updates an existing quizFrage.
     *
     * @param quizFrage the quizFrage to update
     * @return the ResponseEntity with status 200 (OK) and with body the updated quizFrage,
     * or with status 400 (Bad Request) if the quizFrage is not valid,
     * or with status 500 (Internal Server Error) if the quizFrage couldn't be updated
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PutMapping("/quiz-frages")
    @Timed
    public ResponseEntity<QuizFrage> updateQuizFrage(@RequestBody QuizFrage quizFrage) throws URISyntaxException {
        log.debug("REST request to update QuizFrage : {}", quizFrage);
        if (quizFrage.getId() == null) {
            throw new BadRequestAlertException("Invalid id", ENTITY_NAME, "idnull");
        }
        QuizFrage result = quizFrageRepository.save(quizFrage);
        quizFrageSearchRepository.save(result);
        return ResponseEntity.ok()
            .headers(HeaderUtil.createEntityUpdateAlert(ENTITY_NAME, quizFrage.getId().toString()))
            .body(result);
    }

    /**
     * GET  /quiz-frages : get all the quizFrages.
     *
     * @return the ResponseEntity with status 200 (OK) and the list of quizFrages in body
     */
    @GetMapping("/quiz-frages")
    @Timed
    public List<QuizFrage> getAllQuizFrages() {
        log.debug("REST request to get all QuizFrages");
        return quizFrageRepository.findAll();
    }

    /**
     * GET  /quiz-frages/:id : get the "id" quizFrage.
     *
     * @param id the id of the quizFrage to retrieve
     * @return the ResponseEntity with status 200 (OK) and with body the quizFrage, or with status 404 (Not Found)
     */
    @GetMapping("/quiz-frages/{id}")
    @Timed
    public ResponseEntity<QuizFrage> getQuizFrage(@PathVariable Long id) {
        log.debug("REST request to get QuizFrage : {}", id);
        Optional<QuizFrage> quizFrage = quizFrageRepository.findById(id);
        return ResponseUtil.wrapOrNotFound(quizFrage);
    }

    /**
     * DELETE  /quiz-frages/:id : delete the "id" quizFrage.
     *
     * @param id the id of the quizFrage to delete
     * @return the ResponseEntity with status 200 (OK)
     */
    @DeleteMapping("/quiz-frages/{id}")
    @Timed
    public ResponseEntity<Void> deleteQuizFrage(@PathVariable Long id) {
        log.debug("REST request to delete QuizFrage : {}", id);

        quizFrageRepository.deleteById(id);
        quizFrageSearchRepository.deleteById(id);
        return ResponseEntity.ok().headers(HeaderUtil.createEntityDeletionAlert(ENTITY_NAME, id.toString())).build();
    }

    /**
     * SEARCH  /_search/quiz-frages?query=:query : search for the quizFrage corresponding
     * to the query.
     *
     * @param query the query of the quizFrage search
     * @return the result of the search
     */
    @GetMapping("/_search/quiz-frages")
    @Timed
    public List<QuizFrage> searchQuizFrages(@RequestParam String query) {
        log.debug("REST request to search QuizFrages for query {}", query);
        return StreamSupport
            .stream(quizFrageSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

}
