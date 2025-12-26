# RH Management System (.NET 8 MVC)

![Framework](https://img.shields.io/badge/Framework-ASP.NET%20Core%208.0-blue)
![Language](https://img.shields.io/badge/Language-C%23-green)
![Database](https://img.shields.io/badge/Database-SQLServer-yellow)

Une application web moderne et performante de gestion des Ressources Humaines conçue pour centraliser et optimiser le suivi des employés, des départements et de la structure organisationnelle.

---

## Description Générale

RH Management System est une solution logicielle robuste développée avec ASP.NET Core 8.0. L'application utilise l'architecture MVC (Model-View-Controller) pour garantir une séparation claire des responsabilités, facilitant ainsi la maintenance et l'évolutivité.

Le système permet aux administrateurs de gérer efficacement le cycle de vie des employés, d'organiser les départements et de superviser les activités internes via une interface utilisateur moderne, réactive et sécurisée.

---

## ✨ Fonctionnalités Clés

- **Gestion des Employés** : Opérations CRUD complètes (Ajout, Consultation, Modification, Suppression).
- **Structure Organisationnelle** : Gestion des départements et affectation des employés.
- **Accès Sécurisé** : Système de gestion des rôles (Admin/User).
- **Tableau de Bord (Dashboard)** : Vue d'ensemble des statistiques RH (nombre d'employés, nouveaux arrivants, etc.).
- **UI Moderne** : Interface ultra-dynamique avec effets de Glassmorphism, animations fluides et design responsive.

---

## ️Stack Technique

- **Backend** : C# | ASP.NET Core 8.0
- **Frontend** : Razor Pages, HTML5, CSS3, Bootstrap
- **Base de données** : SQL Server
- **Authentification** : Sessions ASP.NET Core, gestion des rôles

---

## 📁 Structure du Projet

RHManagementSystem/
│
├── Controllers/ # Logique des contrôleurs MVC
├── Models/ # Modèles de données
├── Views/ # Pages Razor
│ ├── UserDashboard/ # Tableau de bord utilisateur
│ ├── EmployeesMVC/ # Vue employés
│ └── DepartmentsMVC/# Vue départements
├── Data/ # Contexte de la base de données
├── Migrations/ # Migrations EF Core
├── wwwroot/ # Fichiers statiques (CSS, JS, images)
├── Program.cs # Configuration de l'application
└── appsettings.json # Configuration de la DB et app

yaml
Copier le code

---

## 🚀 Instructions pour le Développement

1. Cloner le dépôt :
```bash
git clone https://github.com/Arhrid-Hamza/RHManagementSystem.git
Ouvrir le projet dans Visual Studio 2022/2023.

Restaurer les packages NuGet.

Configurer la chaîne de connexion dans appsettings.json.

Appliquer les migrations EF Core :

bash
Copier le code
Update-Database
Lancer l'application :

bash
Copier le code
dotnet run
📌 Notes
Les utilisateurs Admin ont accès à toutes les fonctionnalités.

Les utilisateurs User ont accès uniquement à la visualisation et à l’édition de leur profil.

Toutes les pages sans action nécessitent des vues Razor correspondantes dans le dossier Views.

Pour éviter les erreurs de type View not found, assurez-vous que les vues existent pour :

/UserDashboard/Employees

/UserDashboard/Departments

/UserDashboard/Projects

/UserDashboard/Reports

Le tableau de bord utilisateur (UserDashboard) contient uniquement des liens de navigation vers ces vues.

💡 Contribution
Les contributions sont les bienvenues ! Merci de forker le projet et de créer un pull request.

📜 Licence
Ce projet est sous licence MIT.

less
Copier le code

Si tu veux, je peux **créer toutes les vues Razor vides nécessaires pour UserDashboard** (`Employees`, `Departments`, `Projects`, `Reports`) avec juste un tableau d’exemple afin que les liens fonctionnent et que tu n’aies plus d’erreur 404.  

Veux‑tu que je fasse ça maintenant ?
