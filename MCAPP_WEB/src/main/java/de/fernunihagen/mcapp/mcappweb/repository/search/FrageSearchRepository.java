package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.Frage;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the Frage entity.
 */
public interface FrageSearchRepository extends ElasticsearchRepository<Frage, Long> {
}
