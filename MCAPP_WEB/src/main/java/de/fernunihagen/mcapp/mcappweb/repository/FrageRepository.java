package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.Frage;
import org.springframework.data.jpa.repository.*;
import org.springframework.stereotype.Repository;


/**
 * Spring Data  repository for the Frage entity.
 */
@SuppressWarnings("unused")
@Repository
public interface FrageRepository extends JpaRepository<Frage, Long> {

}
