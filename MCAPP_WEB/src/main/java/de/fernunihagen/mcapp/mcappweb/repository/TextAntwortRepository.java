package de.fernunihagen.mcapp.mcappweb.repository;

import de.fernunihagen.mcapp.mcappweb.domain.TextAntwort;
import org.springframework.data.jpa.repository.*;
import org.springframework.stereotype.Repository;


/**
 * Spring Data  repository for the TextAntwort entity.
 */
@SuppressWarnings("unused")
@Repository
public interface TextAntwortRepository extends JpaRepository<TextAntwort, Long> {

}
