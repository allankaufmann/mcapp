Das Projekt MCAPP_WEB ist einmal mit Maven zu bauen: 
	mvn clean install -DskipTests

Mithilfe einer Java-Entwicklungsumgebung ist die Main-Methode aus der Klasse 'McappWebApp’ zu starten.

Das Serverlog zeigt nach dem erfolgreichen Serverstart die Adresse, unter welcher der Autorenzugang zu erreichen ist. 

Der Servername oder die IP-Adresse muss noch für die MC-App hinterlegt werden (Achtung -> localhost funktioniert ggfls. nicht):
Im Projekt MCAPP_Project.Core ist in der Klasse 'MCAPP_PROPERTIES' die Serveradresse in der Variable 'SERVER_BASE_URL' einzutragen. 

Für den Start der App muss Visual Studio entweder auf einem Mac-Computer ausgeführt werden oder an einem Mac-Computer gekoppelt werden. Die MC-App wird dann über das Projekt MCAPP_Project.iOS gestartet. 
Sofern der Server erreichbar ist, so werden die Inhalte aus dem Autorenzugang geladen. Andernfalls wird die MC-App im Demomodus gestartet. 	


Für die Multiple-Choice-App wurde die Visual Studio Projektmappe ’MCAPP_UI’ angelegt.
Diese Projektmappe enthält vier Projekte:

• MCAPP_Project.Core - enthält sämtlichen plattformübergreifenden Quellcode. Dazu gehören
die Klassen der Modell- sowie der View-Modell-Schicht.
• MCAPP_Project.Droid - ist dafür vorgesehen, um Views für Android-Plattformen zu
enthalten.
• MCAPP_Project.iOS - enthält alle Views für iOS-Plattformen.
• MCAPP_Test - enthält Unittests für die Servicemethoden.


Das Projekt MCAPP_Project.Core enthält folgende Pakete:

• Models - enthält Entitäten und die benötigten Fachklassen.
• Repositories - enthält die benötigten Repositories für den Datenzugriff.
• Services - enthält Serviceklassen, welche nach den Fachthemen ’Frage’, ’Quiz’ und ’Webservice’
gruppiert sind. Die Services werden von den View-Modellen aufgerufen.
• ViewModels - enthält alle benötigten View-Modelle. Die View-Modelle werden an die Benutzeroberflächen
gebunden.

Das Projekt MCAPP_Project.iOS enthält folgende Pakete:
• Views - enthält die Storyboards für die iOS Benutzeroberflächen
Das Projekt MCAPP_TEST enthält folgende Pakete:
• Tests - enthält die vorhandenen Unittest-Sammlungen. Diese sind nach Services gruppiert.

	
	

