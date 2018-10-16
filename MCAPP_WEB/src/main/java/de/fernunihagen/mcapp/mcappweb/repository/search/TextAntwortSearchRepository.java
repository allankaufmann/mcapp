package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.TextAntwort;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the TextAntwort entity.
 */
public interface TextAntwortSearchRepository extends ElasticsearchRepository<TextAntwort, Long> {
}
