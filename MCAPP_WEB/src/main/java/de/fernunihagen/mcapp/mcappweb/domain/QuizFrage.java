package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.util.Objects;

/**
 * A QuizFrage.
 */
@Entity
@Table(name = "quiz_frage")
@Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
@Document(indexName = "quizfrage")
public class QuizFrage implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "richtig")
    private Boolean richtig;

    @ManyToOne
    @JsonIgnoreProperties("quizFrageIDS")
    private Frage frage;

    @ManyToOne
    @JsonIgnoreProperties("quizFrageIDS")
    private Quiz quiz;

    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Boolean isRichtig() {
        return richtig;
    }

    public QuizFrage richtig(Boolean richtig) {
        this.richtig = richtig;
        return this;
    }

    public void setRichtig(Boolean richtig) {
        this.richtig = richtig;
    }

    public Frage getFrage() {
        return frage;
    }

    public QuizFrage frage(Frage frage) {
        this.frage = frage;
        return this;
    }

    public void setFrage(Frage frage) {
        this.frage = frage;
    }

    public Quiz getQuiz() {
        return quiz;
    }

    public QuizFrage quiz(Quiz quiz) {
        this.quiz = quiz;
        return this;
    }

    public void setQuiz(Quiz quiz) {
        this.quiz = quiz;
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
        QuizFrage quizFrage = (QuizFrage) o;
        if (quizFrage.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), quizFrage.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "QuizFrage{" +
            "id=" + getId() +
            ", richtig='" + isRichtig() + "'" +
            "}";
    }
}
