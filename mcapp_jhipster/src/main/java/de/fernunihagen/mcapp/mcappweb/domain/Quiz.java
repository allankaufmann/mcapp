package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.time.LocalDate;
import java.util.Objects;

/**
 * A Quiz.
 */
@Entity
@Table(name = "quiz")
@Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
@Document(indexName = "quiz")
public class Quiz implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "quiz_id")
    private Long quizID;

    @Column(name = "datum")
    private LocalDate datum;

    @ManyToOne
    @JsonIgnoreProperties("quizIDS")
    private QuizFrage quizFrage;

    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getQuizID() {
        return quizID;
    }

    public Quiz quizID(Long quizID) {
        this.quizID = quizID;
        return this;
    }

    public void setQuizID(Long quizID) {
        this.quizID = quizID;
    }

    public LocalDate getDatum() {
        return datum;
    }

    public Quiz datum(LocalDate datum) {
        this.datum = datum;
        return this;
    }

    public void setDatum(LocalDate datum) {
        this.datum = datum;
    }

    public QuizFrage getQuizFrage() {
        return quizFrage;
    }

    public Quiz quizFrage(QuizFrage quizFrage) {
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
        Quiz quiz = (Quiz) o;
        if (quiz.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), quiz.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "Quiz{" +
            "id=" + getId() +
            ", quizID=" + getQuizID() +
            ", datum='" + getDatum() + "'" +
            "}";
    }
}
