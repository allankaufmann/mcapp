package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnore;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.util.HashSet;
import java.util.Set;
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

    @OneToMany(mappedBy = "quizFrage")
    @Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
    private Set<Quiz> quizIDS = new HashSet<>();
    @OneToMany(mappedBy = "quizFrage")
    @Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
    private Set<Frage> frageIDS = new HashSet<>();
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

    public Set<Quiz> getQuizIDS() {
        return quizIDS;
    }

    public QuizFrage quizIDS(Set<Quiz> quizzes) {
        this.quizIDS = quizzes;
        return this;
    }

    public QuizFrage addQuizID(Quiz quiz) {
        this.quizIDS.add(quiz);
        quiz.setQuizFrage(this);
        return this;
    }

    public QuizFrage removeQuizID(Quiz quiz) {
        this.quizIDS.remove(quiz);
        quiz.setQuizFrage(null);
        return this;
    }

    public void setQuizIDS(Set<Quiz> quizzes) {
        this.quizIDS = quizzes;
    }

    public Set<Frage> getFrageIDS() {
        return frageIDS;
    }

    public QuizFrage frageIDS(Set<Frage> frages) {
        this.frageIDS = frages;
        return this;
    }

    public QuizFrage addFrageID(Frage frage) {
        this.frageIDS.add(frage);
        frage.setQuizFrage(this);
        return this;
    }

    public QuizFrage removeFrageID(Frage frage) {
        this.frageIDS.remove(frage);
        frage.setQuizFrage(null);
        return this;
    }

    public void setFrageIDS(Set<Frage> frages) {
        this.frageIDS = frages;
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
