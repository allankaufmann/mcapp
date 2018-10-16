package de.fernunihagen.mcapp.mcappweb.web.rest;

import com.codahale.metrics.annotation.Timed;
import de.fernunihagen.mcapp.mcappweb.domain.BildAntwort;
import de.fernunihagen.mcapp.mcappweb.repository.BildAntwortRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.BildAntwortSearchRepository;
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
 * REST controller for managing BildAntwort.
 */
@RestController
@RequestMapping("/api")
public class BildAntwortResource {

    private final Logger log = LoggerFactory.getLogger(BildAntwortResource.class);

    private static final String ENTITY_NAME = "bildAntwort";

    private final BildAntwortRepository bildAntwortRepository;

    private final BildAntwortSearchRepository bildAntwortSearchRepository;

    public BildAntwortResource(BildAntwortRepository bildAntwortRepository, BildAntwortSearchRepository bildAntwortSearchRepository) {
        this.bildAntwortRepository = bildAntwortRepository;
        this.bildAntwortSearchRepository = bildAntwortSearchRepository;
    }

    /**
     * POST  /bild-antworts : Create a new bildAntwort.
     *
     * @param bildAntwort the bildAntwort to create
     * @return the ResponseEntity with status 201 (Created) and with body the new bildAntwort, or with status 400 (Bad Request) if the bildAntwort has already an ID
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PostMapping("/bild-antworts")
    @Timed
    public ResponseEntity<BildAntwort> createBildAntwort(@RequestBody BildAntwort bildAntwort) throws URISyntaxException {
        log.debug("REST request to save BildAntwort : {}", bildAntwort);
        if (bildAntwort.getId() != null) {
            throw new BadRequestAlertException("A new bildAntwort cannot already have an ID", ENTITY_NAME, "idexists");
        }
        BildAntwort result = bildAntwortRepository.save(bildAntwort);
        bildAntwortSearchRepository.save(result);
        return ResponseEntity.created(new URI("/api/bild-antworts/" + result.getId()))
            .headers(HeaderUtil.createEntityCreationAlert(ENTITY_NAME, result.getId().toString()))
            .body(result);
    }

    /**
     * PUT  /bild-antworts : Updates an existing bildAntwort.
     *
     * @param bildAntwort the bildAntwort to update
     * @return the ResponseEntity with status 200 (OK) and with body the updated bildAntwort,
     * or with status 400 (Bad Request) if the bildAntwort is not valid,
     * or with status 500 (Internal Server Error) if the bildAntwort couldn't be updated
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PutMapping("/bild-antworts")
    @Timed
    public ResponseEntity<BildAntwort> updateBildAntwort(@RequestBody BildAntwort bildAntwort) throws URISyntaxException {
        log.debug("REST request to update BildAntwort : {}", bildAntwort);
        if (bildAntwort.getId() == null) {
            throw new BadRequestAlertException("Invalid id", ENTITY_NAME, "idnull");
        }
        BildAntwort result = bildAntwortRepository.save(bildAntwort);
        bildAntwortSearchRepository.save(result);
        return ResponseEntity.ok()
            .headers(HeaderUtil.createEntityUpdateAlert(ENTITY_NAME, bildAntwort.getId().toString()))
            .body(result);
    }

    /**
     * GET  /bild-antworts : get all the bildAntworts.
     *
     * @return the ResponseEntity with status 200 (OK) and the list of bildAntworts in body
     */
    @GetMapping("/bild-antworts")
    @Timed
    public List<BildAntwort> getAllBildAntworts() {
        log.debug("REST request to get all BildAntworts");
        return bildAntwortRepository.findAll();
    }

    /**
     * GET  /bild-antworts/:id : get the "id" bildAntwort.
     *
     * @param id the id of the bildAntwort to retrieve
     * @return the ResponseEntity with status 200 (OK) and with body the bildAntwort, or with status 404 (Not Found)
     */
    @GetMapping("/bild-antworts/{id}")
    @Timed
    public ResponseEntity<BildAntwort> getBildAntwort(@PathVariable Long id) {
        log.debug("REST request to get BildAntwort : {}", id);
        Optional<BildAntwort> bildAntwort = bildAntwortRepository.findById(id);
        return ResponseUtil.wrapOrNotFound(bildAntwort);
    }

    /**
     * DELETE  /bild-antworts/:id : delete the "id" bildAntwort.
     *
     * @param id the id of the bildAntwort to delete
     * @return the ResponseEntity with status 200 (OK)
     */
    @DeleteMapping("/bild-antworts/{id}")
    @Timed
    public ResponseEntity<Void> deleteBildAntwort(@PathVariable Long id) {
        log.debug("REST request to delete BildAntwort : {}", id);

        bildAntwortRepository.deleteById(id);
        bildAntwortSearchRepository.deleteById(id);
        return ResponseEntity.ok().headers(HeaderUtil.createEntityDeletionAlert(ENTITY_NAME, id.toString())).build();
    }

    /**
     * SEARCH  /_search/bild-antworts?query=:query : search for the bildAntwort corresponding
     * to the query.
     *
     * @param query the query of the bildAntwort search
     * @return the result of the search
     */
    @GetMapping("/_search/bild-antworts")
    @Timed
    public List<BildAntwort> searchBildAntworts(@RequestParam String query) {
        log.debug("REST request to search BildAntworts for query {}", query);
        return StreamSupport
            .stream(bildAntwortSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

}
