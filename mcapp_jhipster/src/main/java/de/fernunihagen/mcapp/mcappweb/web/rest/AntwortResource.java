package de.fernunihagen.mcapp.mcappweb.web.rest;

import com.codahale.metrics.annotation.Timed;
import de.fernunihagen.mcapp.mcappweb.domain.Antwort;
import de.fernunihagen.mcapp.mcappweb.repository.AntwortRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.AntwortSearchRepository;
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
 * REST controller for managing Antwort.
 */
@RestController
@RequestMapping("/api")
public class AntwortResource {

    private final Logger log = LoggerFactory.getLogger(AntwortResource.class);

    private static final String ENTITY_NAME = "antwort";

    private final AntwortRepository antwortRepository;

    private final AntwortSearchRepository antwortSearchRepository;

    public AntwortResource(AntwortRepository antwortRepository, AntwortSearchRepository antwortSearchRepository) {
        this.antwortRepository = antwortRepository;
        this.antwortSearchRepository = antwortSearchRepository;
    }

    /**
     * POST  /antworts : Create a new antwort.
     *
     * @param antwort the antwort to create
     * @return the ResponseEntity with status 201 (Created) and with body the new antwort, or with status 400 (Bad Request) if the antwort has already an ID
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PostMapping("/antworts")
    @Timed
    public ResponseEntity<Antwort> createAntwort(@RequestBody Antwort antwort) throws URISyntaxException {
        log.debug("REST request to save Antwort : {}", antwort);
        if (antwort.getId() != null) {
            throw new BadRequestAlertException("A new antwort cannot already have an ID", ENTITY_NAME, "idexists");
        }
        Antwort result = antwortRepository.save(antwort);
        antwortSearchRepository.save(result);
        return ResponseEntity.created(new URI("/api/antworts/" + result.getId()))
            .headers(HeaderUtil.createEntityCreationAlert(ENTITY_NAME, result.getId().toString()))
            .body(result);
    }

    /**
     * PUT  /antworts : Updates an existing antwort.
     *
     * @param antwort the antwort to update
     * @return the ResponseEntity with status 200 (OK) and with body the updated antwort,
     * or with status 400 (Bad Request) if the antwort is not valid,
     * or with status 500 (Internal Server Error) if the antwort couldn't be updated
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PutMapping("/antworts")
    @Timed
    public ResponseEntity<Antwort> updateAntwort(@RequestBody Antwort antwort) throws URISyntaxException {
        log.debug("REST request to update Antwort : {}", antwort);
        if (antwort.getId() == null) {
            throw new BadRequestAlertException("Invalid id", ENTITY_NAME, "idnull");
        }
        Antwort result = antwortRepository.save(antwort);
        antwortSearchRepository.save(result);
        return ResponseEntity.ok()
            .headers(HeaderUtil.createEntityUpdateAlert(ENTITY_NAME, antwort.getId().toString()))
            .body(result);
    }

    /**
     * GET  /antworts : get all the antworts.
     *
     * @return the ResponseEntity with status 200 (OK) and the list of antworts in body
     */
    @GetMapping("/antworts")
    @Timed
    public List<Antwort> getAllAntworts() {
        log.debug("REST request to get all Antworts");
        return antwortRepository.findAll();
    }

    /**
     * GET  /antworts/:id : get the "id" antwort.
     *
     * @param id the id of the antwort to retrieve
     * @return the ResponseEntity with status 200 (OK) and with body the antwort, or with status 404 (Not Found)
     */
    @GetMapping("/antworts/{id}")
    @Timed
    public ResponseEntity<Antwort> getAntwort(@PathVariable Long id) {
        log.debug("REST request to get Antwort : {}", id);
        Optional<Antwort> antwort = antwortRepository.findById(id);
        return ResponseUtil.wrapOrNotFound(antwort);
    }

    /**
     * DELETE  /antworts/:id : delete the "id" antwort.
     *
     * @param id the id of the antwort to delete
     * @return the ResponseEntity with status 200 (OK)
     */
    @DeleteMapping("/antworts/{id}")
    @Timed
    public ResponseEntity<Void> deleteAntwort(@PathVariable Long id) {
        log.debug("REST request to delete Antwort : {}", id);

        antwortRepository.deleteById(id);
        antwortSearchRepository.deleteById(id);
        return ResponseEntity.ok().headers(HeaderUtil.createEntityDeletionAlert(ENTITY_NAME, id.toString())).build();
    }

    /**
     * SEARCH  /_search/antworts?query=:query : search for the antwort corresponding
     * to the query.
     *
     * @param query the query of the antwort search
     * @return the result of the search
     */
    @GetMapping("/_search/antworts")
    @Timed
    public List<Antwort> searchAntworts(@RequestParam String query) {
        log.debug("REST request to search Antworts for query {}", query);
        return StreamSupport
            .stream(antwortSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

}
