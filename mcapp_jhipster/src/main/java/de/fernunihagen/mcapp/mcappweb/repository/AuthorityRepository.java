package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.Authority;

import org.springframework.data.jpa.repository.JpaRepository;

/**
 * Spring Data JPA repository for the Authority entity.
 */
public interface AuthorityRepository extends JpaRepository<Authority, String> {
}
