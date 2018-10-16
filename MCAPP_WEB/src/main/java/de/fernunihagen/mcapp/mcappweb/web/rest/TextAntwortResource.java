package de.fernunihagen.mcapp.mcappweb.web.rest;

import com.codahale.metrics.annotation.Timed;
import de.fernunihagen.mcapp.mcappweb.domain.TextAntwort;
import de.fernunihagen.mcapp.mcappweb.repository.TextAntwortRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.TextAntwortSearchRepository;
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
 * REST controller for managing TextAntwort.
 */
@RestController
@RequestMapping("/api")
public class TextAntwortResource {

    private final Logger log = LoggerFactory.getLogger(TextAntwortResource.class);

    private static final String ENTITY_NAME = "textAntwort";

    private final TextAntwortRepository textAntwortRepository;

    private final TextAntwortSearchRepository textAntwortSearchRepository;

    public TextAntwortResource(TextAntwortRepository textAntwortRepository, TextAntwortSearchRepository textAntwortSearchRepository) {
        this.textAntwortRepository = textAntwortRepository;
        this.textAntwortSearchRepository = textAntwortSearchRepository;
    }

    /**
     * POST  /text-antworts : Create a new textAntwort.
     *
     * @param textAntwort the textAntwort to create
     * @return the ResponseEntity with status 201 (Created) and with body the new textAntwort, or with status 400 (Bad Request) if the textAntwort has already an ID
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PostMapping("/text-antworts")
    @Timed
    public ResponseEntity<TextAntwort> createTextAntwort(@RequestBody TextAntwort textAntwort) throws URISyntaxException {
        log.debug("REST request to save TextAntwort : {}", textAntwort);
        if (textAntwort.getId() != null) {
            throw new BadRequestAlertException("A new textAntwort cannot already have an ID", ENTITY_NAME, "idexists");
        }
        TextAntwort result = textAntwortRepository.save(textAntwort);
        textAntwortSearchRepository.save(result);
        return ResponseEntity.created(new URI("/api/text-antworts/" + result.getId()))
            .headers(HeaderUtil.createEntityCreationAlert(ENTITY_NAME, result.getId().toString()))
            .body(result);
    }

    /**
     * PUT  /text-antworts : Updates an existing textAntwort.
     *
     * @param textAntwort the textAntwort to update
     * @return the ResponseEntity with status 200 (OK) and with body the updated textAntwort,
     * or with status 400 (Bad Request) if the textAntwort is not valid,
     * or with status 500 (Internal Server Error) if the textAntwort couldn't be updated
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PutMapping("/text-antworts")
    @Timed
    public ResponseEntity<TextAntwort> updateTextAntwort(@RequestBody TextAntwort textAntwort) throws URISyntaxException {
        log.debug("REST request to update TextAntwort : {}", textAntwort);
        if (textAntwort.getId() == null) {
            throw new BadRequestAlertException("Invalid id", ENTITY_NAME, "idnull");
        }
        TextAntwort result = textAntwortRepository.save(textAntwort);
        textAntwortSearchRepository.save(result);
        return ResponseEntity.ok()
            .headers(HeaderUtil.createEntityUpdateAlert(ENTITY_NAME, textAntwort.getId().toString()))
            .body(result);
    }

    /**
     * GET  /text-antworts : get all the textAntworts.
     *
     * @return the ResponseEntity with status 200 (OK) and the list of textAntworts in body
     */
    @GetMapping("/text-antworts")
    @Timed
    public List<TextAntwort> getAllTextAntworts() {
        log.debug("REST request to get all TextAntworts");
        return textAntwortRepository.findAll();
    }

    /**
     * GET  /text-antworts/:id : get the "id" textAntwort.
     *
     * @param id the id of the textAntwort to retrieve
     * @return the ResponseEntity with status 200 (OK) and with body the textAntwort, or with status 404 (Not Found)
     */
    @GetMapping("/text-antworts/{id}")
    @Timed
    public ResponseEntity<TextAntwort> getTextAntwort(@PathVariable Long id) {
        log.debug("REST request to get TextAntwort : {}", id);
        Optional<TextAntwort> textAntwort = textAntwortRepository.findById(id);
        return ResponseUtil.wrapOrNotFound(textAntwort);
    }

    /**
     * DELETE  /text-antworts/:id : delete the "id" textAntwort.
     *
     * @param id the id of the textAntwort to delete
     * @return the ResponseEntity with status 200 (OK)
     */
    @DeleteMapping("/text-antworts/{id}")
    @Timed
    public ResponseEntity<Void> deleteTextAntwort(@PathVariable Long id) {
        log.debug("REST request to delete TextAntwort : {}", id);

        textAntwortRepository.deleteById(id);
        textAntwortSearchRepository.deleteById(id);
        return ResponseEntity.ok().headers(HeaderUtil.createEntityDeletionAlert(ENTITY_NAME, id.toString())).build();
    }

    /**
     * SEARCH  /_search/text-antworts?query=:query : search for the textAntwort corresponding
     * to the query.
     *
     * @param query the query of the textAntwort search
     * @return the result of the search
     */
    @GetMapping("/_search/text-antworts")
    @Timed
    public List<TextAntwort> searchTextAntworts(@RequestParam String query) {
        log.debug("REST request to search TextAntworts for query {}", query);
        return StreamSupport
            .stream(textAntwortSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

}
