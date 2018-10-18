package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.util.Objects;

/**
 * A TextAntwort.
 */
@Entity
@Table(name = "text_antwort")
@Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
@Document(indexName = "textantwort")
public class TextAntwort implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "position")
    private Long position;

    @Column(name = "wahr")
    private Boolean wahr;

    @Column(name = "text")
    private String text;

    @ManyToOne
    @JsonIgnoreProperties("textAntwortIDS")
    private Frage frage;

    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getPosition() {
        return position;
    }

    public TextAntwort position(Long position) {
        this.position = position;
        return this;
    }

    public void setPosition(Long position) {
        this.position = position;
    }

    public Boolean isWahr() {
        return wahr;
    }

    public TextAntwort wahr(Boolean wahr) {
        this.wahr = wahr;
        return this;
    }

    public void setWahr(Boolean wahr) {
        this.wahr = wahr;
    }

    public String getText() {
        return text;
    }

    public TextAntwort text(String text) {
        this.text = text;
        return this;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Frage getFrage() {
        return frage;
    }

    public TextAntwort frage(Frage frage) {
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
        TextAntwort textAntwort = (TextAntwort) o;
        if (textAntwort.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), textAntwort.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "TextAntwort{" +
            "id=" + getId() +
            ", position=" + getPosition() +
            ", wahr='" + isWahr() + "'" +
            ", text='" + getText() + "'" +
            "}";
    }
}
