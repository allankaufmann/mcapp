package de.fernunihagen.mcapp.mcappweb.repository.search;

import de.fernunihagen.mcapp.mcappweb.domain.User;
import org.springframework.data.elasticsearch.repository.ElasticsearchRepository;

/**
 * Spring Data Elasticsearch repository for the User entity.
 */
public interface UserSearchRepository extends ElasticsearchRepository<User, Long> {
}
