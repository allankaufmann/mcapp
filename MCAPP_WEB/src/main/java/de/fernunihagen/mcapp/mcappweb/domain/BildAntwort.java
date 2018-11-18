package de.fernunihagen.mcapp.mcappweb.domain;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import org.hibernate.annotations.Cache;
import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

import org.springframework.data.elasticsearch.annotations.Document;
import java.io.Serializable;
import java.util.Objects;

/**
 * A BildAntwort.
 */
@Entity
@Table(name = "bild_antwort")
@Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
@Document(indexName = "bildantwort")
public class BildAntwort implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "position")
    private Long position;

    @Column(name = "wahr")
    private Boolean wahr;

    @Lob
    @Column(name = "bild")
    private byte[] bild;

    @Column(name = "bild_content_type")
    private String bildContentType;

    @ManyToOne
    @JsonIgnoreProperties({"bildAntwortIDS", "textAntwortIDS"})
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

    public BildAntwort position(Long position) {
        this.position = position;
        return this;
    }

    public void setPosition(Long position) {
        this.position = position;
    }

    public Boolean isWahr() {
        return wahr;
    }

    public BildAntwort wahr(Boolean wahr) {
        this.wahr = wahr;
        return this;
    }

    public void setWahr(Boolean wahr) {
        this.wahr = wahr;
    }

    public byte[] getBild() {
        return bild;
    }

    public BildAntwort bild(byte[] bild) {
        this.bild = bild;
        return this;
    }

    public void setBild(byte[] bild) {
        this.bild = bild;
    }

    public String getBildContentType() {
        return bildContentType;
    }

    public BildAntwort bildContentType(String bildContentType) {
        this.bildContentType = bildContentType;
        return this;
    }

    public void setBildContentType(String bildContentType) {
        this.bildContentType = bildContentType;
    }

    public Frage getFrage() {
        return frage;
    }

    public BildAntwort frage(Frage frage) {
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
        BildAntwort bildAntwort = (BildAntwort) o;
        if (bildAntwort.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), bildAntwort.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "BildAntwort{" +
            "id=" + getId() +
            ", position=" + getPosition() +
            ", wahr='" + isWahr() + "'" +
            ", bild='" + getBild() + "'" +
            ", bildContentType='" + getBildContentType() + "'" +
            "}";
    }
}
