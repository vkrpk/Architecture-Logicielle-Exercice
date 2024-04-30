# API REST pour la récupération de clients et de leurs comptes bancaires

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

### Installation de l'application :
```bash
docker compose up -d
```
L'API sera alors accessible depuis ce [lien](http://localhost:8000/swagger) et l'administration de la base de données sera disponible depuis ce [lien](http://localhost:8080).  
Vous pouvez vous connecter avec les identifiants suivants :
- Système : MS SQL (beta)
- Serveur : app-db
- Utilisateur : sa
- Mot de passe : password@12345#

### Informations sur les tests :  
Des tests unitaires et des tests d'intégration ont été réalisés dans le projet "Architecture.Tests".

### Procédures d’assurance-qualité appliquées sur le code :
Pour assurer une haute qualité du code et une maintenance facile de l'application, nous avons intégré SonarCloud, un service d'analyse continue. SonarCloud analyse automatiquement notre code pour détecter les bugs, les vulnérabilités, et les "code smells", tout en fournissant des feedbacks sur la couverture des tests.

Le tableau de bord SonarCloud est accessible depuis ce [lien](https://sonarcloud.io/summary/overall?id=vkrpk_Architecture-Logicielle-Exercice).

### Installation de l'application (sans Docker) :

Avant de démarrer l'application, il convient de créer un fichier "appsettings.json" dans le projet "Architecture" en se suivant ce modèle (appsettings.Template.json) :

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
Ainsi, au démarrage de l'application, une base de données sera créée automatiquement, et sera placée au chemin indiqué. 




### Base de données avec Entity Framework :

Cette application est faite en langage C#, avec le framework ASP.NET
Une base de données peut être générée (sans données) grâce à Entity Framework Core (veillez à bien installer les packages au préalable)

### Optionnel | Démarrage de l'application avec Swagger :

Au lancement de l'application (veillez à bien avoir comme projet de démarrage Architecture), une fenêtre de navigateur d'ouvre, montrant une erreur 404. 
Afin d'accéder à l'interface de swagger pour tester les méthodes des contrôleurs, il suffit de rajouter "/swagger" dans l'URL.

Dernière mise-à-jour le : [05/04/2024]





