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

    @Column(name = "frage_text")
    private String frageText;

    @Enumerated(EnumType.STRING)
    @Column(name = "frage_typ")
    private Fragetyp frageTyp;

    @ManyToOne
    @JsonIgnoreProperties("frageIDS")
    private Thema thema;

    @OneToMany(mappedBy = "frage", fetch=FetchType.EAGER)
    @Cache(usage = CacheConcurrencyStrategy.NONE)
    private Set<TextAntwort> textAntwortIDS = new HashSet<>();

    @OneToMany(mappedBy = "frage", fetch=FetchType.EAGER)
    @Cache(usage = CacheConcurrencyStrategy.NONE)
    private Set<BildAntwort> bildAntwortIDS = new HashSet<>();

    @OneToMany(mappedBy = "frage")
    @Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
    private Set<QuizFrage> quizFrageIDS = new HashSet<>();
    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getFrageText() {
        return frageText;
    }

    public Frage frageText(String frageText) {
        this.frageText = frageText;
        return this;
    }

    public void setFrageText(String frageText) {
        this.frageText = frageText;
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

    public Thema getThema() {
        return thema;
    }

    public Frage thema(Thema thema) {
        this.thema = thema;
        return this;
    }

    public void setThema(Thema thema) {
        this.thema = thema;
    }

    public Set<TextAntwort> getTextAntwortIDS() {
        return textAntwortIDS;
    }

    public Frage textAntwortIDS(Set<TextAntwort> textAntworts) {
        this.textAntwortIDS = textAntworts;
        return this;
    }

    public Frage addTextAntwortID(TextAntwort textAntwort) {
        this.textAntwortIDS.add(textAntwort);
        textAntwort.setFrage(this);
        return this;
    }

    public Frage removeTextAntwortID(TextAntwort textAntwort) {
        this.textAntwortIDS.remove(textAntwort);
        textAntwort.setFrage(null);
        return this;
    }

    public void setTextAntwortIDS(Set<TextAntwort> textAntworts) {
        this.textAntwortIDS = textAntworts;
    }

    public Set<BildAntwort> getBildAntwortIDS() {
        return bildAntwortIDS;
    }

    public Frage bildAntwortIDS(Set<BildAntwort> bildAntworts) {
        this.bildAntwortIDS = bildAntworts;
        return this;
    }

    public Frage addBildAntwortID(BildAntwort bildAntwort) {
        this.bildAntwortIDS.add(bildAntwort);
        bildAntwort.setFrage(this);
        return this;
    }

    public Frage removeBildAntwortID(BildAntwort bildAntwort) {
        this.bildAntwortIDS.remove(bildAntwort);
        bildAntwort.setFrage(null);
        return this;
    }

    public void setBildAntwortIDS(Set<BildAntwort> bildAntworts) {
        this.bildAntwortIDS = bildAntworts;
    }

    public Set<QuizFrage> getQuizFrageIDS() {
        return quizFrageIDS;
    }

    public Frage quizFrageIDS(Set<QuizFrage> quizFrages) {
        this.quizFrageIDS = quizFrages;
        return this;
    }

    public Frage addQuizFrageID(QuizFrage quizFrage) {
        this.quizFrageIDS.add(quizFrage);
        quizFrage.setFrage(this);
        return this;
    }

    public Frage removeQuizFrageID(QuizFrage quizFrage) {
        this.quizFrageIDS.remove(quizFrage);
        quizFrage.setFrage(null);
        return this;
    }

    public void setQuizFrageIDS(Set<QuizFrage> quizFrages) {
        this.quizFrageIDS = quizFrages;
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
            ", frageText='" + getFrageText() + "'" +
            ", frageTyp='" + getFrageTyp() + "'" +
            "}";
    }
}
