using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core
{
    public class MCAPP_PROPERTIES
    {
        static readonly public String SERVER_BASE_URL = "http://192.168.178.34:8080/";

        static readonly public String SERVER_USER = "user";

        static readonly public String SERVER_PASSWORD = "user";

        // Vor der Sycronisation zwischen App und Server-DB wird geprüft, ob der Server online ist. Damit die Anwendung nicht
        // längere Zeit hängt, wird mit dieser Variable ein Timeout vereinbart. Wenn der Server innerhalb dieser
        // Sekunden nicht reagiert, findet kein Abgleich statt. Im Entwicklungssystem waren 5 Sekunden ausreichend. Später im WWW-Betrieb
        // könnte hier aber ein längeres Timeout eingestellt werden.
        static readonly public long TIMEOUT_IN_SECONDS_FOR_ALIVE_CHECK = 5; 

        // Wird von Anwendung auf true gesetzt, 
        // wenn lokale DB leer und Web-Service nicht vorhanden ist oder keine Themen angelegt wurden.
        static public Boolean DEMO_MODUS = false;

        // DATENBANK_IN SMARTPHONE kann im Entwicklungssysstem gesetzt werden, wenn ein bestimtmer Pfad der Datenbankdatei erwünscht ist. 
        // Es macht aber Sinn, im richtigen Betrieb diese Variable auf null zu setzen. Die DB wird dann in einem lokalen Verzeichnis gespeichert.
        static readonly public String DATENBANK_IN_SMARTPHONE = "/Users/allan/test.db";

        static readonly public String DATENBANK_IN_SMARTPHONE_NAME = "mcapp.db";

    }
}
