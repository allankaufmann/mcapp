package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.Antwort;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the Antwort entity.
 */
public interface AntwortSearchRepository extends ElasticsearchRepository<Antwort, Long> {
}
