# ChatApp Frontend (Desktop)

Cette application est un **client de messagerie desktop** développé avec **C#** et **Avalonia UI**, suivant l’architecture **MVVM**. Elle communique avec une API backend (Spring Boot) et permet la gestion des utilisateurs, des canaux et des messages en temps réel via WebSocket.

---

## 🧰 Technologies utilisées

- [C#](https://learn.microsoft.com/fr-fr/dotnet/csharp/)
- [Avalonia UI](https://avaloniaui.net/) (cross-platform)
- MVVM (CommunityToolkit.MVVM)
- WebSocket
- REST API (authentification, gestion des canaux, etc.)

---

## 🖥️ Fonctionnalités

- Connexion/Inscription d'utilisateurs
- Liste des canaux et messages
- Envoi et réception de messages en temps réel
- Interface utilisateur réactive et moderne
- Gestion des rôles et permissions par canal

---

## 🚀 Lancement du projet

### Prérequis

- [.NET SDK 7+](https://dotnet.microsoft.com/)
- IDE recommandé : [Rider](https://www.jetbrains.com/rider/) ou [Visual Studio](https://visualstudio.microsoft.com/)
- Accès au backend (voir dépôt [chatapp_backend](https://github.com/Rijenth/chatapp_backend))

### Compilation et exécution

```bash
dotnet restore
dotnet build
dotnet run
```

Ou via l'IDE : ouvrir `DCDesktop.csproj` et exécuter le projet.

---

## 🗂️ Structure du projet

```
chatapp-frontend/
├── Models/                # Modèles de données (User, Channel, Message...)
├── Views/                 # Pages Avalonia (axaml)
├── ViewModels/            # Logique de présentation (MVVM)
├── Converters/            # Converters pour l'UI
├── Assets/                # Logos et ressources graphiques
├── App.axaml              # Définition globale de l'application
└── MainWindow.axaml       # Point d'entrée visuel principal
```

---

## 🔌 Connexion au backend

Le frontend s'attend à ce que le backend soit lancé et disponible sur :
```
http://localhost:8000
```

Les WebSockets se connectent sur :
```
ws://localhost:8000/ws
```

Le backend doit être en cours d'exécution avant de lancer l'application.

---
