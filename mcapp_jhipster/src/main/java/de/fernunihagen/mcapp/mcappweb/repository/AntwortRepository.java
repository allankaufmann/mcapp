package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.Antwort;
import org.springframework.data.jpa.repository.*;
import org.springframework.stereotype.Repository;


/**
 * Spring Data  repository for the Antwort entity.
 */
@SuppressWarnings("unused")
@Repository
public interface AntwortRepository extends JpaRepository<Antwort, Long> {

}
