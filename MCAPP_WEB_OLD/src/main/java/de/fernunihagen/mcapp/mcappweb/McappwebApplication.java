package de.fernunihagen.mcapp.mcappweb;

import de.fernunihagen.mcapp.mcappweb.domain.Thema;
import de.fernunihagen.mcapp.mcappweb.repositories.ThemaRepository;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@SpringBootApplication
@EnableJpaRepositories(basePackageClasses = ThemaRepository.class)
@EntityScan(basePackageClasses = Thema.class)
		public class McappwebApplication {

	public static void main(String[] args) {
		SpringApplication.run(McappwebApplication.class, args);
	}
}
