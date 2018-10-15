package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.Thema;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the Thema entity.
 */
public interface ThemaSearchRepository extends ElasticsearchRepository<Thema, Long> {
}
