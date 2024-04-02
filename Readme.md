Ce fichier README a été généré le [08/02/2024] par [Groupe 1 : Arthur | Hervé | Laurine | Victor].

Dernière mise-à-jour le : [08/02/2024].

# INFORMATIONS GENERALES

## Titre du projet : Premier exercice d'architecture logicielle (Application Banque)

## Démarrage de l'application : 

Avant de démarrer l'application, il convient de créer un fichier "appsettings.json" dans le projet "Architecture".
En se suivant ce modèle (appsettings.Template.json) :

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Database": "Your connection string here"
  }
}

```
Remplacer "Your connection string here" par la chaîne de connexion souhaitée vers la base de données. 


## Informations concernant l'application :

Cette application est une API réalisée en langage C#, avec le framework ASP.NET.
Le Framework Entity Framework Core a été utilisé pour gérer la base de données.
Le Framework MS Tests a été utilisé pour la création et réalisation de tests.
Swagger a été utilisé pour documenter et avoir une interface sur laquelle tester les méthodes exposées par les endpoints de l'API. 

### Arborescence : 

L'Arborescence de cette application est organisée comme suit : 

    - Architecture : Projet Web contenant les Contrôleurs
    - Architecture.Domain : Projet Librairie contenant les Modèles
    - Architecture.Impl : Projet Librairie contenant les Repositories
    - Architecture.Impl.EFDatabase : Projet Librairie contenant la classe implémentée pour la base de données, les mappings et les migrations avec EF Core
    - Architecture.Tests : Projet de tests MS Tests. 

## Informations sur les tests :

Des tests unitaires et des tests d'intégration ont été réalisés dans le projet "Architecture.Tests".

## Optionnel | Base de données avec Entity Framework :

Cette application est faite en langage C#, avec le framework ASP.NET
Une base de données peut être générée (sans données) grâce à Entity Framework Core (veillez à bien installer les packages au préalable)
Voici la procédure en ouvrant la solution avec Visual Studio : 
- Accéder au code de la classe "ApDbContext"
- Modifier la chaine de connexion dans la méthode OnConfiguring (cf "Votre chaîne de connexion ici")
- Faire un click droit sur le projet "Architecture.Impl.EFDatabase
- Cliquer sur "ouvrir avec le terminal"
- Dans le terminal, rentrer les lignes de code suivantes : 
    dotnet ef migrations add InitialCreate
    dotnet ef database update
- Une base de données se crée ensuite

## Optionnel | Démarrage de l'application avec Swagger :

Au lancement de l'application (veilez à bien avoir comme projet de démarrage Architecture), une fenêtre de navigateur d'ouvre, montrant une erreur 404. 
Afin d'accéder à l'interface de swagger pour tester les méthodes des contrôleurs, il suffit de rajouter "/swagger" dans l'URL. 


## Procédures d’assurance-qualité appliquées sur le code :
Pour assurer une haute qualité du code et une maintenance facile de l'application, nous avons intégré SonarCloud, un service d'analyse continue. SonarCloud analyse automatiquement notre code pour détecter les bugs, les vulnérabilités, et les "code smells", tout en fournissant des feedbacks sur la couverture des tests.

URL du tableau de bord SonarCloud : [sonarcloud](https://sonarcloud.io/summary/overall?id=vkrpk_Architecture-Logicielle-Exercice)

## Démarrage de l'application avec Docker :
Pour faciliter le déploiement et assurer la cohérence des environnements de développement, nous avons dockerisé l'application.   
Vous pouvez démarrer l'application en utilisant Docker Compose.  
Naviguez jusqu'à la racine du projet où se trouve le fichier docker-compose.yml.  
Exécutez la commande suivante pour démarrer tous les services :  
```bash
docker compose up -d
```

