package de.fernunihagen.mcapp.mcappweb.web.rest;

import com.codahale.metrics.annotation.Timed;
import de.fernunihagen.mcapp.mcappweb.domain.Frage;
import de.fernunihagen.mcapp.mcappweb.repository.FrageRepository;
import de.fernunihagen.mcapp.mcappweb.repository.search.FrageSearchRepository;
import de.fernunihagen.mcapp.mcappweb.web.rest.errors.BadRequestAlertException;
import de.fernunihagen.mcapp.mcappweb.web.rest.util.HeaderUtil;
import io.github.jhipster.web.util.ResponseUtil;
import io.micrometer.core.annotation.TimedSet;
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
 * REST controller for managing Frage.
 */
@RestController
@RequestMapping("/api")
public class FrageResource {

    private final Logger log = LoggerFactory.getLogger(FrageResource.class);

    private static final String ENTITY_NAME = "frage";

    private final FrageRepository frageRepository;

    private final FrageSearchRepository frageSearchRepository;

    public FrageResource(FrageRepository frageRepository, FrageSearchRepository frageSearchRepository) {
        this.frageRepository = frageRepository;
        this.frageSearchRepository = frageSearchRepository;
    }

    /**
     * POST  /frages : Create a new frage.
     *
     * @param frage the frage to create
     * @return the ResponseEntity with status 201 (Created) and with body the new frage, or with status 400 (Bad Request) if the frage has already an ID
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PostMapping("/frages")
    @Timed
    public ResponseEntity<Frage> createFrage(@RequestBody Frage frage) throws URISyntaxException {
        log.debug("REST request to save Frage : {}", frage);
        if (frage.getId() != null) {
            throw new BadRequestAlertException("A new frage cannot already have an ID", ENTITY_NAME, "idexists");
        }
        Frage result = frageRepository.save(frage);
        frageSearchRepository.save(result);
        return ResponseEntity.created(new URI("/api/frages/" + result.getId()))
            .headers(HeaderUtil.createEntityCreationAlert(ENTITY_NAME, result.getId().toString()))
            .body(result);
    }

    /**
     * PUT  /frages : Updates an existing frage.
     *
     * @param frage the frage to update
     * @return the ResponseEntity with status 200 (OK) and with body the updated frage,
     * or with status 400 (Bad Request) if the frage is not valid,
     * or with status 500 (Internal Server Error) if the frage couldn't be updated
     * @throws URISyntaxException if the Location URI syntax is incorrect
     */
    @PutMapping("/frages")
    @Timed
    public ResponseEntity<Frage> updateFrage(@RequestBody Frage frage) throws URISyntaxException {
        log.debug("REST request to update Frage : {}", frage);
        if (frage.getId() == null) {
            throw new BadRequestAlertException("Invalid id", ENTITY_NAME, "idnull");
        }
        Frage result = frageRepository.save(frage);
        frageSearchRepository.save(result);
        return ResponseEntity.ok()
            .headers(HeaderUtil.createEntityUpdateAlert(ENTITY_NAME, frage.getId().toString()))
            .body(result);
    }

    /**
     * GET  /frages : get all the frages.
     *
     * @return the ResponseEntity with status 200 (OK) and the list of frages in body
     */
    @GetMapping("/frages")
    @Timed
    public List<Frage> getAllFrages() {
        log.debug("REST request to get all Frages");
        return frageRepository.findAll();
    }

    /**
     * Methode sollte Fragen für Thema liefert. Methode ist für Tabelle nciht mehr notwendig,
     * da Thema bereits frageIDS enthält. Abfrage wäre aber so ebenfalls möglich gewesen.
     *
     * @param themaid Thema zu der Fragen geliefert werden
     * @return Liste der Fragen zum gesuchten Thema.
     */

    @GetMapping("/fragesByThema/{themaid}")
    @Timed
    public List<Frage> GetAllFragesByThema(@PathVariable Long themaid) {
        String query = "THEMA = " + themaid;
        log.debug("REST request to search Frages for query {}", query);
        return StreamSupport
            .stream(frageSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

    /**
     * GET  /frages/:id : get the "id" frage.
     *
     * @param id the id of the frage to retrieve
     * @return the ResponseEntity with status 200 (OK) and with body the frage, or with status 404 (Not Found)
     */
    @GetMapping("/frages/{id}")
    @Timed
    public ResponseEntity<Frage> getFrage(@PathVariable Long id) {
        log.debug("REST request to get Frage : {}", id);
        Optional<Frage> frage = frageRepository.findById(id);
        return ResponseUtil.wrapOrNotFound(frage);
    }

    /**
     * DELETE  /frages/:id : delete the "id" frage.
     *
     * @param id the id of the frage to delete
     * @return the ResponseEntity with status 200 (OK)
     */
    @DeleteMapping("/frages/{id}")
    @Timed
    public ResponseEntity<Void> deleteFrage(@PathVariable Long id) {
        log.debug("REST request to delete Frage : {}", id);

        frageRepository.deleteById(id);
        frageSearchRepository.deleteById(id);
        return ResponseEntity.ok().headers(HeaderUtil.createEntityDeletionAlert(ENTITY_NAME, id.toString())).build();
    }

    /**
     * SEARCH  /_search/frages?query=:query : search for the frage corresponding
     * to the query.
     *
     * @param query the query of the frage search
     * @return the result of the search
     */
    @GetMapping("/_search/frages")
    @Timed
    public List<Frage> searchFrages(@RequestParam String query) {
        log.debug("REST request to search Frages for query {}", query);
        return StreamSupport
            .stream(frageSearchRepository.search(queryStringQuery(query)).spliterator(), false)
            .collect(Collectors.toList());
    }

}
