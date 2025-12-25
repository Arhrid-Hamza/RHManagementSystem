# 🚀 RH Management System (.NET 8 MVC)

![Framework](https://img.shields.io/badge/.NET-8.0-512bd4?style=for-the-badge&logo=dotnet)
![Language](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp)
![Architecture](https://img.shields.io/badge/Architecture-MVC-blue?style=for-the-badge)
![Database](https://img.shields.io/badge/Database-SQL_Server-red?style=for-the-badge&logo=microsoft-sql-server)

Une application web moderne et performante de gestion des Ressources Humaines conçue pour centraliser et optimiser le suivi des employés, des départements et de la structure organisationnelle.

---

## 📋 Description Générale

**RH Management System** est une solution logicielle robuste développée avec **ASP.NET Core 8.0**. L'application utilise l'architecture **MVC (Model-View-Controller)** pour garantir une séparation claire des responsabilités, facilitant ainsi la maintenance et l'évolutivité. 

Le système permet aux administrateurs de gérer efficacement le cycle de vie des employés, d'organiser les départements et de superviser les activités internes via une interface utilisateur moderne, réactive et sécurisée.

## ✨ Fonctionnalités Clés

* **👥 Gestion des Employés :** Opérations CRUD complètes (Ajout, Consultation, Modification, Suppression).
* **🏢 Structure Organisationnelle :** Gestion des départements et affectation des employés.
* **🔐 Accès Sécurisé :** Système de gestion des rôles (Admin/User).
* **📊 Tableau de Bord (Dashboard) :** Vue d'ensemble des statistiques RH (nombre d'employés, nouveaux arrivants, etc.).
* **🎨 UI Moderne :** Interface ultra-dynamique avec effets de *Glassmorphism*, animations fluides et design responsive.

## 🛠️ Stack Technique

- **Backend :** C# | ASP.NET Core 8.0
- **Frontend :** Razor Pages, HTML5, CSS3 (Custom Animations), JavaScript
- **Base de données :** SQL Server via **Entity Framework Core (Code-First)**
- **Gestionnaire de paquets :** NuGet & NPM

## 📂 Structure du Projet

```text
RHManagementSystem/
├── Controllers/    # Logique métier et flux de contrôle
├── Models/         # Classes de données et contextes DB
├── Views/          # Interfaces utilisateur (Razor Pages)
├── Data/           # Contexte Entity Framework (DbContext)
├── Migrations/     # Historique de la structure DB
├── wwwroot/        # Fichiers statiques (CSS, JS, Images)
└── Program.cs      # Point d'entrée et configuration de l'app
