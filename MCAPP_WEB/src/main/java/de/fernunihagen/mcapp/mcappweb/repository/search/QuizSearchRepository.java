package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.Quiz;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the Quiz entity.
 */
public interface QuizSearchRepository extends ElasticsearchRepository<Quiz, Long> {
}
