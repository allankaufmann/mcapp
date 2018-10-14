package de.fernunihagen.mcapp.mcappweb.domain;

import javax.persistence.Cacheable;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Size;

@Entity
@Cacheable
public class Thema {



    @Id
    @GeneratedValue
    private long themaId;

    @NotNull
    @Size(min=1, max=20)
    private String themaText;


    public long getThemaId() {
        return themaId;
    }

    public void setThemaId(long themaId) {
        this.themaId = themaId;
    }

    public String getThemaText() {
        return themaText;
    }

    public void setThemaText(String themaText) {
        this.themaText = themaText;
    }

}
