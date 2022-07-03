
# Labyrinth

### Description

Ceci est un projet francophone de jeu collaboratif à but caritatif, développé en live sur twitch : https://www.twitch.tv/kheranos_projects  

Le but est de réunir les gens autour d'un projet sympa et dont le développement sera retransmis sur twitch de manière régulière.

La finalité étant de publier le jeu à un prix modeste pendant une période donnée avant de la passer gratuit, pour ensuite reverser l'intégralité des fonds à une association d'aide aux animaux.

### Game Design

#### Titre 

Labyrinth  
*(nom provisoire, sera voté par la communauté)*

#### Concept

Affrontez les épreuves du donjon labyrinthique et tentez de trouver la sortie !
*(concept provisoire, évoluera pendant le développent)*

#### Inspiration

The Legend of Zelda: Link's Awakening
Enter the Gungeon
The Binding of Isaac

#### Genres

Action, Puzzle, Rogue-Like
*(genres provisoires, pourront évoluer pendant le développent)*

#### Présentation du jeu

Le joueur incarne un aventurier s'attaque à un donjon labyrinthique pour aller libérer les animaux enlevés, il devra parcourir les salles les unes après les autres jusqu'à trouver le maître du donjon et mettre fin à ses agissements.
Monstres et énigmes attendent l'aventurier pour le ralentir dans sa quête.
Pour s'aider l'aventurier pourra trouver différents artefacts aux innombrables effets ou compter sur l'aide des animaux qu'il aura secouru pendant son exploration.
*(idée de base, évoluera pendant le développent)*

#### Expérience recherchée

Le but est que le joueur ai envie de pousser l'exploration et que chaque fin de partie ne soit qu'un prétexte pour en relancer une et voir quel options de gameplay s'offriront à lui avec les effets des différents artefacts et animaux qu'il découvrira.
*(évoluera pendant le développent)*

#### Style Audio et Visuel

*(sera défini par la communauté)*

#### Lore du jeu

*(sera défini par la communauté)*

#### Monétisation

Le jeu sera mis en vente sur Steam ou une autre plateforme à un prix modeste pendant une période *(à définir)*
Puis passera gratuit.

#### Plateforme,

PC
Possible portage Android

####  Technologie

Unity, en 2D

#### Portée du développement

L'objectif final serait d'avoir une gameloop d'1h re-jouable.
A voir avec la communauté comment nous arriveront là.

Avoir une first-playable pour Septembre 2022 serait un bon objectif
puis pour Décembre 2022 atteindre la beta.

Objectif : livraison 2nd trimestre 2023.

#### Game loop

Le joueur entre dans le labyrinthe, 
parcours un étage (caractérisé par différentes salles, changeant de pattern et contenu à chaque nouvelles exploration)
trouve des artefact ou un animal pour l'aider
poursuis à l'étage suivant,
jusqu'à atteindre le maître du donjon ou être vaincu, auquel cas, il finis au hub de départ et dépense les trésors trouvé en route pour
être plus fort pour la prochaine exploration, ainsi jusqu'à pouvoir vaincre le maître du donjon.

#### Systémes de jeu

* Contrôles avec Joystick | ZQSD/Touches directionelles, une touche d'attaque, une touche d'action/artefact
* Génération procédurale de labyrinth avec des salles de différentes formes et différents challenges (monstres et/ou puzzle)
* Système d'armes (via le hub le joueur peux choisir son arme, ce qui change son attaque pour son aventure)
* Système de camera qui s'adapte à la room (si taille de 1*1) ou suis le joueur et se clamp sur les bords (si taille > 1*1)
* IA pour différents type d'ennemis (à définir)
* IA pour les compagnons animaux (à définir)
* Système d'artefacts pouvant avoir des effets cachés si réunis.
