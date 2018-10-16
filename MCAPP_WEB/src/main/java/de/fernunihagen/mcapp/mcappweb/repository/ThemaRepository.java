package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.Thema;
import org.springframework.data.jpa.repository.*;
import org.springframework.stereotype.Repository;


/**
 * Spring Data  repository for the Thema entity.
 */
@SuppressWarnings("unused")
@Repository
public interface ThemaRepository extends JpaRepository<Thema, Long> {

}
