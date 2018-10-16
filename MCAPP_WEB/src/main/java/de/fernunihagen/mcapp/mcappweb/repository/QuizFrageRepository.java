package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.QuizFrage;
import org.springframework.data.jpa.repository.*;
import org.springframework.stereotype.Repository;


/**
 * Spring Data  repository for the QuizFrage entity.
 */
@SuppressWarnings("unused")
@Repository
public interface QuizFrageRepository extends JpaRepository<QuizFrage, Long> {

}
