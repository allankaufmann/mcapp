using System;
using System.Collections.Generic;
using System.Text;

namespace MCAPP_Project.Core
{
    public class MCAPP_PROPERTIES
    {
        static readonly public String SERVER_BASE_URL = "http://localhost:8080/";

        static readonly public String SERVER_USER = "user";

        static readonly public String SERVER_PASSWORD = "user";

        // Wird von Anwendung auf true gesetzt, 
        // wenn lokale DB leer und Web-Service nicht vorhanden ist oder keine Themen angelegt wurden.
        static public Boolean DEMO_MODUS = false;


    }
}
