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
 * A Antwort.
 */
@Entity
@Table(name = "antwort")
@Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
@Document(indexName = "antwort")
public class Antwort implements Serializable {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "antwort_id")
    private Long antwortID;

    @Column(name = "position")
    private Long position;

    @Column(name = "wahr")
    private Boolean wahr;

    @Column(name = "text")
    private String text;

    @Lob
    @Column(name = "bild")
    private byte[] bild;

    @Column(name = "bild_content_type")
    private String bildContentType;

    @OneToMany(mappedBy = "antwort")
    @Cache(usage = CacheConcurrencyStrategy.NONSTRICT_READ_WRITE)
    private Set<Frage> frageIDS = new HashSet<>();
    // jhipster-needle-entity-add-field - JHipster will add fields here, do not remove
    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getAntwortID() {
        return antwortID;
    }

    public Antwort antwortID(Long antwortID) {
        this.antwortID = antwortID;
        return this;
    }

    public void setAntwortID(Long antwortID) {
        this.antwortID = antwortID;
    }

    public Long getPosition() {
        return position;
    }

    public Antwort position(Long position) {
        this.position = position;
        return this;
    }

    public void setPosition(Long position) {
        this.position = position;
    }

    public Boolean isWahr() {
        return wahr;
    }

    public Antwort wahr(Boolean wahr) {
        this.wahr = wahr;
        return this;
    }

    public void setWahr(Boolean wahr) {
        this.wahr = wahr;
    }

    public String getText() {
        return text;
    }

    public Antwort text(String text) {
        this.text = text;
        return this;
    }

    public void setText(String text) {
        this.text = text;
    }

    public byte[] getBild() {
        return bild;
    }

    public Antwort bild(byte[] bild) {
        this.bild = bild;
        return this;
    }

    public void setBild(byte[] bild) {
        this.bild = bild;
    }

    public String getBildContentType() {
        return bildContentType;
    }

    public Antwort bildContentType(String bildContentType) {
        this.bildContentType = bildContentType;
        return this;
    }

    public void setBildContentType(String bildContentType) {
        this.bildContentType = bildContentType;
    }

    public Set<Frage> getFrageIDS() {
        return frageIDS;
    }

    public Antwort frageIDS(Set<Frage> frages) {
        this.frageIDS = frages;
        return this;
    }

    public Antwort addFrageID(Frage frage) {
        this.frageIDS.add(frage);
        frage.setAntwort(this);
        return this;
    }

    public Antwort removeFrageID(Frage frage) {
        this.frageIDS.remove(frage);
        frage.setAntwort(null);
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
        Antwort antwort = (Antwort) o;
        if (antwort.getId() == null || getId() == null) {
            return false;
        }
        return Objects.equals(getId(), antwort.getId());
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(getId());
    }

    @Override
    public String toString() {
        return "Antwort{" +
            "id=" + getId() +
            ", antwortID=" + getAntwortID() +
            ", position=" + getPosition() +
            ", wahr='" + isWahr() + "'" +
            ", text='" + getText() + "'" +
            ", bild='" + getBild() + "'" +
            ", bildContentType='" + getBildContentType() + "'" +
            "}";
    }
}
