package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.QuizFrage;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the QuizFrage entity.
 */
public interface QuizFrageSearchRepository extends ElasticsearchRepository<QuizFrage, Long> {
}
