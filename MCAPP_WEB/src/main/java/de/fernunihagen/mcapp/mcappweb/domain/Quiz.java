package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonIgnore;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.time.LocalDate;
import java.util.HashSet;
import java.util.Set;
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

    @Column(name = "datum")
    @JsonFormat(pattern = "yyyy-MM-dd")
    private LocalDate datum;

    @OneToMany(mappedBy = "quiz")
    @Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
    private Set<QuizFrage> quizFrageIDS = new HashSet<>();
    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
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

    public Set<QuizFrage> getQuizFrageIDS() {
        return quizFrageIDS;
    }

    public Quiz quizFrageIDS(Set<QuizFrage> quizFrages) {
        this.quizFrageIDS = quizFrages;
        return this;
    }

    public Quiz addQuizFrageID(QuizFrage quizFrage) {
        this.quizFrageIDS.add(quizFrage);
        quizFrage.setQuiz(this);
        return this;
    }

    public Quiz removeQuizFrageID(QuizFrage quizFrage) {
        this.quizFrageIDS.remove(quizFrage);
        quizFrage.setQuiz(null);
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
            ", datum='" + getDatum() + "'" +
            "}";
    }
}
