Ce fichier README a été généré le 08/02/2024 par le Groupe 1 : Arthur | Hervé | Laurine | Victor.

Dernière mise-à-jour le : [08/02/2024].

# INFORMATIONS GENERALES

## Titre du projet : Premier exercice d'architecture logicielle (Application Banque)

## Conditions environnementales / experimentales : 

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
