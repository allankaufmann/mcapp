package de.fernunihagen.mcapp.mcappweb.repositories;

import de.fernunihagen.mcapp.mcappweb.domain.Thema;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface ThemaRepository extends JpaRepository<Thema, Long> {


    List<Thema> findAll();  //?

    Thema findBythemaId(@Param("themaId") long themaId); // /themas/search/findBythemaId?themaId=1

    //public List<Thema> saveAll(List<Thema> themen);

}
