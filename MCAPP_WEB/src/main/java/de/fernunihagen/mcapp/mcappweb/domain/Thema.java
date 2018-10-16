package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.util.Objects;

/**
 * A Thema.
 */
@Entity
@Table(name = "thema")
@Document(indexName = "thema")
public class Thema implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "thema_text")
    private String themaText;

    @ManyToOne
    @JsonIgnoreProperties("themaIDS")
    private Frage frage;

    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getThemaText() {
        return themaText;
    }

    public Thema themaText(String themaText) {
        this.themaText = themaText;
        return this;
    }

    public void setThemaText(String themaText) {
        this.themaText = themaText;
    }

    public Frage getFrage() {
        return frage;
    }

    public Thema frage(Frage frage) {
        this.frage = frage;
        return this;
    }

    public void setFrage(Frage frage) {
        this.frage = frage;
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
        Thema thema = (Thema) o;
        if (thema.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), thema.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "Thema{" +
            "id=" + getId() +
            ", themaText='" + getThemaText() + "'" +
            "}";
    }
}
