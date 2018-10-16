package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.BildAntwort;
import org.springframework.data.jpa.repository.*;
import org.springframework.stereotype.Repository;


/**
 * Spring Data  repository for the BildAntwort entity.
 */
@SuppressWarnings("unused")
@Repository
public interface BildAntwortRepository extends JpaRepository<BildAntwort, Long> {

}
