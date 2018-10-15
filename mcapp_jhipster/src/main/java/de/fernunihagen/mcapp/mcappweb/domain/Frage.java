package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.util.HashSet;
import java.util.Set;
import java.util.Objects;

import de.fernunihagen.mcapp.mcappweb.domain.enumeration.Fragetyp;

/**
 * A Frage.
 */
@Entity
@Table(name = "frage")
@Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
@Document(indexName = "frage")
public class Frage implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "frage_id")
    private Long frageID;

    @Enumerated(EnumType.STRING)
    @Column(name = "frage_typ")
    private Fragetyp frageTyp;

    @Column(name = "thema_id")
    private Long themaID;

    @OneToMany(mappedBy = "frage")
    @Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
    private Set<Thema> themaIDS = new HashSet<>();
    @ManyToOne
    @JsonIgnoreProperties("frageIDS")
    private Antwort antwort;

    @ManyToOne
    @JsonIgnoreProperties("frageIDS")
    private QuizFrage quizFrage;

    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getFrageID() {
        return frageID;
    }

    public Frage frageID(Long frageID) {
        this.frageID = frageID;
        return this;
    }

    public void setFrageID(Long frageID) {
        this.frageID = frageID;
    }

    public Fragetyp getFrageTyp() {
        return frageTyp;
    }

    public Frage frageTyp(Fragetyp frageTyp) {
        this.frageTyp = frageTyp;
        return this;
    }

    public void setFrageTyp(Fragetyp frageTyp) {
        this.frageTyp = frageTyp;
    }

    public Long getThemaID() {
        return themaID;
    }

    public Frage themaID(Long themaID) {
        this.themaID = themaID;
        return this;
    }

    public void setThemaID(Long themaID) {
        this.themaID = themaID;
    }

    public Set<Thema> getThemaIDS() {
        return themaIDS;
    }

    public Frage themaIDS(Set<Thema> themas) {
        this.themaIDS = themas;
        return this;
    }

    public Frage addThemaID(Thema thema) {
        this.themaIDS.add(thema);
        thema.setFrage(this);
        return this;
    }

    public Frage removeThemaID(Thema thema) {
        this.themaIDS.remove(thema);
        thema.setFrage(null);
        return this;
    }

    public void setThemaIDS(Set<Thema> themas) {
        this.themaIDS = themas;
    }

    public Antwort getAntwort() {
        return antwort;
    }

    public Frage antwort(Antwort antwort) {
        this.antwort = antwort;
        return this;
    }

    public void setAntwort(Antwort antwort) {
        this.antwort = antwort;
    }

    public QuizFrage getQuizFrage() {
        return quizFrage;
    }

    public Frage quizFrage(QuizFrage quizFrage) {
        this.quizFrage = quizFrage;
        return this;
    }

    public void setQuizFrage(QuizFrage quizFrage) {
        this.quizFrage = quizFrage;
    }
    // jhipster-needle-entity-add-getters-setters - JHipster will add getters and setters here, do not remove

    @Override
    public boolean equals(Object o) {
        if (this == o) {
            return true;
        }
        if (o == null || getClass() != o.getClass()) {
            return false;
        }
        Frage frage = (Frage) o;
        if (frage.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), frage.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "Frage{" +
            "id=" + getId() +
            ", frageID=" + getFrageID() +
            ", frageTyp='" + getFrageTyp() + "'" +
            ", themaID=" + getThemaID() +
            "}";
    }
}
