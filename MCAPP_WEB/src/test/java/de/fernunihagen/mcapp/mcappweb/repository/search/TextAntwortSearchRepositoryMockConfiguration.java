package de.fernunihagen.mcapp.mcappweb.repository.search;

import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.context.annotation.Configuration;

/**
 * Configure a Mock version of TextAntwortSearchRepository to test the
 * application without starting Elasticsearch.
 */
@Configuration
public class TextAntwortSearchRepositoryMockConfiguration {

    @MockBean
    private TextAntwortSearchRepository mockTextAntwortSearchRepository;

}
