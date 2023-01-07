# Filament-App

Es handelt sich um eine Filament Management App die mit Xamarin.Forms umgesetzt wurde und für die Verwaltung von Filamenten und Maschinen dient. Die App bietet hierbei die Umsetzung von CRUD Operationen für Filamente/Maschinen mit Nutzung einer lokalen Datenbank. Man hat die Möglichkeit Filamente in Maschinen einzusetzen sowie herauszunehmen und mittels Records sich eine Historie dazu anzeigen zu lassen. Beim Herausnehmen/Einsetzen wird zudem eine MQTT Nachricht mit allen darauf bezogenen Daten im JSON Format versendet.Die verwendete Programmiersprache ist C-Sharp.


# Voraussetzungen:

-Visual Studio

-Mobile-Entwicklung mit .Net Workload

-Google Android Emulator (für die Nutzung eines Emulators)


# Installation:

1- des Repositories

2-Öffnen des Projekts in Visual Studio

start FilamentManagement/FilamentManagement.sln

3-Navigation zum Android Device Manager und Erstellung eines Emulators

4-Drücken des Startbutton neben dem ausgewählten Emulator


# Anmerkung

Die App wurde ausschließlich unter Android entwickelt und getestet. Somit bietet die Readme keine nähere Beschreibung zum Aufsetzen eines lauffähigen Projektes unter einem iOS Gerät.

Das Starten der Anwendung kann beim initialen Laden auf Grund des Deployments länger benötigen.

Bei einem Hinzufügen oder Herausnehmen eines Filaments in bzw. aus einer Maschine wird jeweils eine MQTT Nachricht zu dem Server broker.hivemq.com auf der Topic thm/sfm/filament-management gesendet. Das hierbei eingehaltene Json Format der Payload entspricht dem Datenmodell *RecordNotification.
