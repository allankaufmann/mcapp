package de.fernunihagen.mcapp.mcappweb.web.rest;

import com.codahale.metrics.annotation.Timed;
import de.fernunihagen.mcapp.mcappweb.domain.Thema;
import de.fernunihagen.mcapp.mcappweb.repository.ThemaRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.ThemaSearchRepository;
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
 * REST controller for managing Thema.
 */
@RestController
@RequestMapping("/api")
public class ThemaResource {

    private final Logger log = LoggerFactory.getLogger(ThemaResource.class);

    private static final String ENTITY_NAME = "thema";

    private final ThemaRepository themaRepository;

    private final ThemaSearchRepository themaSearchRepository;

    public ThemaResource(ThemaRepository themaRepository, ThemaSearchRepository themaSearchRepository) {
        this.themaRepository = themaRepository;
        this.themaSearchRepository = themaSearchRepository;
    }

    /**
     * POST  /themas : Create a new thema.
     *
     * @param thema the thema to create
     * @return the ResponseEntity with status 201 (Created) and with body the new thema, or with status 400 (Bad Request) if the thema has already an ID
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PostMapping("/themas")
    @Timed
    public ResponseEntity<Thema> createThema(@RequestBody Thema thema) throws URISyntaxException {
        log.debug("REST request to save Thema : {}", thema);
        if (thema.getId() != null) {
            throw new BadRequestAlertException("A new thema cannot already have an ID", ENTITY_NAME, "idexists");
        }
        Thema result = themaRepository.save(thema);
        themaSearchRepository.save(result);
        return ResponseEntity.created(new URI("/api/themas/" + result.getId()))
            .headers(HeaderUtil.createEntityCreationAlert(ENTITY_NAME, result.getId().toString()))
            .body(result);
    }

    /**
     * PUT  /themas : Updates an existing thema.
     *
     * @param thema the thema to update
     * @return the ResponseEntity with status 200 (OK) and with body the updated thema,
     * or with status 400 (Bad Request) if the thema is not valid,
     * or with status 500 (Internal Server Error) if the thema couldn't be updated
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PutMapping("/themas")
    @Timed
    public ResponseEntity<Thema> updateThema(@RequestBody Thema thema) throws URISyntaxException {
        log.debug("REST request to update Thema : {}", thema);
        if (thema.getId() == null) {
            throw new BadRequestAlertException("Invalid id", ENTITY_NAME, "idnull");
        }
        Thema result = themaRepository.save(thema);
        themaSearchRepository.save(result);
        return ResponseEntity.ok()
            .headers(HeaderUtil.createEntityUpdateAlert(ENTITY_NAME, thema.getId().toString()))
            .body(result);
    }

    /**
     * GET  /themas : get all the themas.
     *
     * @return the ResponseEntity with status 200 (OK) and the list of themas in body
     */
    @GetMapping("/themas")
    @Timed
    public List<Thema> getAllThemas() {
        log.debug("REST request to get all Themas");
        return themaRepository.findAll();
    }

    /**
     * GET  /themas/:id : get the "id" thema.
     *
     * @param id the id of the thema to retrieve
     * @return the ResponseEntity with status 200 (OK) and with body the thema, or with status 404 (Not Found)
     */
    @GetMapping("/themas/{id}")
    @Timed
    public ResponseEntity<Thema> getThema(@PathVariable Long id) {
        log.debug("REST request to get Thema : {}", id);
        Optional<Thema> thema = themaRepository.findById(id);
        return ResponseUtil.wrapOrNotFound(thema);
    }

    /**
     * DELETE  /themas/:id : delete the "id" thema.
     *
     * @param id the id of the thema to delete
     * @return the ResponseEntity with status 200 (OK)
     */
    @DeleteMapping("/themas/{id}")
    @Timed
    public ResponseEntity<Void> deleteThema(@PathVariable Long id) {
        log.debug("REST request to delete Thema : {}", id);

        themaRepository.deleteById(id);
        themaSearchRepository.deleteById(id);
        return ResponseEntity.ok().headers(HeaderUtil.createEntityDeletionAlert(ENTITY_NAME, id.toString())).build();
    }

    /**
     * SEARCH  /_search/themas?query=:query : search for the thema corresponding
     * to the query.
     *
     * @param query the query of the thema search
     * @return the result of the search
     */
    @GetMapping("/_search/themas")
    @Timed
    public List<Thema> searchThemas(@RequestParam String query) {
        log.debug("REST request to search Themas for query {}", query);
        return StreamSupport
            .stream(themaSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

}
