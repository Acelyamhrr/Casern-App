# 🚒 SDIS67 - Système de Gestion de Caserne de Pompiers

> **Projet C# / .NET** - Application complète de gestion des missions, du personnel et des engins pour les Services Départementaux d'Incendie et de Secours

## À propos

**SDIS67** est une application Windows Forms permettant la gestion complète d'une caserne de pompiers : suivi des missions en temps réel, gestion du personnel (pompiers professionnels et volontaires), suivi des engins et de leur maintenance, et génération de statistiques détaillées.

### Contexte du projet
- **Langage** : C# (.NET Framework / WinForms)
- **Base de données** : SQLite
- **Objectif** : Application professionnelle de gestion opérationnelle pour casernes de pompiers
- **Compétences** : Architecture MVC, gestion de BDD, transactions SQL, génération PDF, interfaces graphiques


![Volet principal d'affichage des missions.](/Illustration.png)


## Technologies utilisées

| Composant | Technologies |
|-----------|-------------|
| **Langage** | C# (.NET Framework 4.7+) |
| **Interface** | Windows Forms |
| **Base de données** | SQLite3 avec ADO.NET |
| **Génération PDF** | iText7 |
| **Contrôles personnalisés** | UserControls WinForms |
| **Cryptographie** | BouncyCastle |
| **Gestion de projet** | Git, NuGet |

---

## Fonctionnalités

### Gestion des missions

**Attribution automatique des moyens**
- Analyse du type de sinistre (incendie, accident, secours à personne...)
- Sélection automatique des engins nécessaires selon le type de mission
- Attribution des pompiers selon leurs habilitations requises
- Vérification de la disponibilité (engins non en panne, pompiers non en congé)
- Algorithme de recherche multi-casernes si la caserne initiale est insuffisante

**Suivi en temps réel**
- Missions en cours avec statut (départ, sur place, retour)
- Historique complet des missions terminées
- Compte-rendu d'intervention
- Suivi des réparations éventuelles sur les engins

**Génération de rapports PDF**
- Rapport détaillé par mission (horaires, moyens engagés, compte-rendu)
- Export automatique avec iText7
- Différenciation missions en cours / terminées

### Gestion du personnel

**Fiches pompiers complètes**
- Informations personnelles (nom, prénom, date de naissance, sexe)
- Coordonnées (téléphone portable, numéro de bip)
- Statut professionnel (professionnel/volontaire)
- Grade et type de pompier
- Statut opérationnel (disponible, en mission, en congé)

**Historique et habilitations**
- Liste des affectations passées (casernes, dates)
- Habilitations détaillées (FDF, Conduite SPV, Chef d'agrès, etc.)
- Dates d'obtention des habilitations
- Gestion des modifications avec transactions SQL

**Contrôles d'accès**
- Authentification administrateur pour modifications sensibles
- Gestion sécurisée des identifiants
- Séparation des droits (consultation / modification)

### Gestion des engins

**Catalogue complet**
- Engins par caserne (VSAV, FPT, EPA, CCF, etc.)
- Navigation intuitive (premier, précédent, suivant, dernier)
- Affichage des caractéristiques (numéro, type, date de réception)
- Images illustratives par type d'engin
- Statuts : disponible, en mission, en panne

**Maintenance**
- Suivi des pannes et réparations
- Commentaires de maintenance lors de clôture de mission
- Mise en panne automatique si réparation nécessaire

### Statistiques avancées

**Vue d'ensemble SDIS**
- Interventions par type de sinistre (avec graphiques)
- Habilitations les plus sollicitées
- Pompiers par habilitation

**Statistiques par caserne**
- Engins les plus utilisés
- Cumul d'heures d'utilisation par engin
- Analyse de la performance opérationnelle

**Contrôles personnalisés**
- `ctrlStatsEngins` : Affichage statistiques engins
- `ctrlIntervParTypeSin` : Interventions par type de sinistre
- `ctrlPompHabilit` : Pompiers et leurs habilitations

### Base de données SQLite

**Tables principales**
```sql
-- Pompiers
CREATE TABLE Pompier (
    matricule INTEGER PRIMARY KEY,
    nom TEXT NOT NULL,
    prenom TEXT NOT NULL,
    sexe TEXT CHECK(sexe IN ('m','f')),
    dateNaissance DATE,
    portable TEXT,
    bip INTEGER,
    type TEXT CHECK(type IN ('p','v')), -- professionnel/volontaire
    dateEmbauche DATE,
    codeGrade TEXT,
    enMission BOOLEAN DEFAULT 0,
    enConge BOOLEAN DEFAULT 0
);

-- Missions
CREATE TABLE Mission (
    id INTEGER PRIMARY KEY,
    dateHeureDepart DATETIME,
    dateHeureRetour DATETIME,
    motifAppel TEXT,
    adresse TEXT,
    cp TEXT,
    ville TEXT,
    terminee BOOLEAN DEFAULT 0,
    compteRendu TEXT,
    idNatureSinistre INTEGER,
    idCaserne INTEGER
);

-- Engins
CREATE TABLE Engin (
    idCaserne INTEGER,
    codeTypeEngin TEXT,
    numero INTEGER,
    dateReception DATE,
    enMission BOOLEAN DEFAULT 0,
    enPanne BOOLEAN DEFAULT 0,
    PRIMARY KEY (idCaserne, codeTypeEngin, numero)
);
```

**Relations et tables de liaison**
- `Affectation` : Historique des affectations pompiers/casernes
- `Passer` : Habilitations obtenues par les pompiers
- `Mobiliser` : Pompiers mobilisés par mission
- `PartirAvec` : Engins engagés par mission
- `Embarquer` : Nombre de pompiers requis par type d'engin
- `Necessiter` : Types d'engins requis par type de sinistre

---

### Principes de conception

**Singleton pour la connexion**
```csharp
internal class Connexion
{
    private static SQLiteConnection connec;
    
    public static SQLiteConnection Connec
    {
        get
        {
            if (connec == null)
            {
                string chaine = @"Data Source = SDIS67.db";
                connec = new SQLiteConnection(chaine);
                connec.Open();
            }
            return connec;
        }
    }
}
```

**DataSet global pour cache**
```csharp
public class MesDatas
{
    private static DataSet dsGlobal = new DataSet();
    public static DataSet DsGlobal { get { return MesDatas.dsGlobal; } }
}
```

---

## Installation

### Prérequis
- **Visual Studio** 2019 ou supérieur
- **.NET Framework** 4.7.2+
- **SQLite** (inclus via NuGet)
- **Windows** 7/8/10/11

### Packages NuGet requis
```
System.Data.SQLite
iText7
BouncyCastle.Cryptography
```

### Étapes d'installation

**1. Cloner le dépôt**
```bash
git clone https://github.com/votre-username/sdis67-gestion-caserne.git
cd sdis67-gestion-caserne
```

**2. Ouvrir avec Visual Studio**
```
Fichier > Ouvrir > Projet/Solution
Sélectionner SAE_Caserne.sln
```

**3. Restaurer les packages NuGet**
```
Outils > Gestionnaire de packages NuGet > Restaurer les packages
```

**4. Vérifier la base de données**
- Le fichier `SDIS67.db` doit être à la racine du projet
- Propriété "Copier dans le répertoire de sortie" : Toujours copier

**5. Compiler et exécuter**
```
F5 ou Déboguer > Démarrer le débogage
```

---

## Utilisation

### Premier lancement

**1. Connexion administrateur**
- Login par défaut : `admin`
- Mot de passe : (consultez la table `Admin` dans la BDD)
- Nécessaire pour modifier les données

**2. Charger les données**
Au démarrage, l'application charge automatiquement :
- Toutes les tables de la base SDIS67.db dans le DataSet global
- Les casernes disponibles
- L'état des missions en cours

### Scénarios d'utilisation

**Créer une nouvelle mission**
```
1. Cliquer sur [+ Nouvelle Mission]
2. Sélectionner le type de sinistre (incendie, accident...)
3. Saisir l'adresse complète
4. Saisir le motif de l'appel
5. Sélectionner la caserne d'intervention
6. Cliquer sur [Valider]
   → L'application attribue automatiquement :
      - Les engins nécessaires
      - Les pompiers avec habilitations requises
7. Confirmation avec liste des moyens engagés
```

**Clôturer une mission**
```
1. Dans le tableau de bord, cliquer sur [Clôturer]
2. Saisir le compte-rendu d'intervention
3. Pour chaque engin, indiquer si réparation nécessaire
4. Valider
   → Mise à jour automatique des statuts
   → Génération du rapport PDF
```

**Ajouter un pompier**
```
1. Onglet [Personnel] > [+ Ajouter un pompier]
2. Remplir les informations (nom, prénom, grade...)
3. Sélectionner le type (professionnel/volontaire)
4. Ajouter les habilitations avec dates d'obtention
5. Valider
   → Matricule attribué automatiquement
   → Affectation à la caserne sélectionnée
```

**Modifier un pompier**
```
1. Cliquer sur [Modifier] sur la carte du pompier
2. Modifier les informations (téléphone, grade, caserne...)
3. Ajouter/Supprimer des habilitations
4. Valider ou Annuler
   → Transaction SQL pour cohérence des données
```

**Consulter les statistiques**
```
1. Onglet [Statistiques]
2. Vue d'ensemble : stats globales SDIS
3. Sélectionner une caserne pour stats détaillées
   - Engins les plus utilisés
   - Heures d'utilisation cumulées
```

---

## Concepts techniques implémentés

**Classes métier avec responsabilité unique**
- `AffichagePompiers` : Logique d'affichage et modification personnel
- `AffichageEngin` : Navigation et affichage engins avec BindingSource
- `AjouterMission` : Algorithme d'attribution automatique des moyens
- `tableauDeBord` : Chargement et filtrage missions
- `statistiques` : Calculs et affichage statistiques

**UserControls réutilisables**
- Encapsulation de l'interface et du comportement
- Propriétés personnalisées avec attributs `[Description]`
- Événements personnalisés via délégués

**Transactions SQL pour intégrité**

**Requêtes paramétrées (prévention injection SQL)**

**DataSet pour cache en mémoire**
- Chargement au démarrage de toutes les tables
- Accès rapide sans requêtes répétées
- Synchronisation avec la BDD lors des modifications

**BindingSource pour liaison de données**

**Filtrage dynamique**

**Génération dynamique de contrôles**

**Attribution automatique des moyens (AjouterMission.cs)**

**Calcul dynamique du prochain matricule**

---

## Dépannage

**Problème : "Unable to open database file"**
- Vérifier que `SDIS67.db` existe dans le dossier du projet
- Propriété du fichier : "Copier dans le répertoire de sortie" → Toujours copier

**Problème : Erreur de connexion SQLite**
- Vérifier le NuGet `System.Data.SQLite` installé
- Reconstruire la solution (Ctrl+Shift+B)

**Problème : Images engins/pompiers non affichées**
- Vérifier les chemins relatifs dans le code (`@"..\\..\\..\\ImagesEngins\\"`)
- S'assurer que les dossiers `ImagesEngins`, `ImagesSexePompier`, etc. existent

**Problème : Génération PDF échoue**
- Vérifier le package NuGet `iText7` installé
- Vérifier les droits d'écriture dans le dossier de sortie

---
