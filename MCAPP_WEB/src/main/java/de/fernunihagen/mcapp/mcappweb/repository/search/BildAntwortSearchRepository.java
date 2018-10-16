package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.BildAntwort;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the BildAntwort entity.
 */
public interface BildAntwortSearchRepository extends ElasticsearchRepository<BildAntwort, Long> {
}
