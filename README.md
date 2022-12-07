# Tesis 2022: DEFENSOR DE LA CULTURA
Gabriel Santiago Serna, Camilo Otalora, Arturo Rubio

Proyect Developed on Unity 2019.4.25f1. <br>
Data extracted with a script from Google Arts & Culture: https://artsandculture.google.com/
![image](https://user-images.githubusercontent.com/42653275/205780222-813e318a-a3f3-4303-9f55-1049b6a03645.png)

# Description

This proyect want to teach about the different art pieces, museums and places around the world in a interactive way. Inspired by the popular series "Where in the world is carmen sandiego" the player works as an investigator for a international agency in order to stop various criminals traveling the world. While in the move, the player can learn and see some of the most important interest places and museums of the different countries that are that are available.

# Objective

The main focus was to use real world data, extracted from Google Arts and Culture, for it to provide meaningful context for the player. The files the game uses are complete detached from the internal stucture. So interested parties can modify and create new version that fit their interests. From which contries are present, to the dialogues, npc and criminals. Everything is loaded dynamically so it can be moddable.

# Modding Guide

In order to add new content, there are some guidelines that need to be followed in order to achieve it.

To add a new contry, follow one of the files provided in the folder "Assets/Data/Countries", in the "cities" part add only the cities that will be avilable in the files. Any city that doesn't has a file will result in incorrect loading.

To add a new city, follow one of the files provided in the folder "Assets/Data/Cities", in the "interestPlaces" and "museums" add only the places and museums that will be avilable in the files. Any places and museums that don't have a file will result in incorrect loading. The "name" part needs to be the same as the one inserted in the "cities" part of the country it corresponds. 

Add the places the same way as stated above. Follow the "Assets/Data/Interes Sites" files, taking into account the "name" attribute for the relationship to be taken into account. For a city, you need to add an "Aeropuerto" place type for it to be able to travel to that location. Don't forget to add that site to the "interestPlaces" array.

For a museum, follow the same process with the "Assets/Data/Museums" files. Add only the art pieces that will be used.

For the art pieces, do the same but with the "Assets/Data/Art Pieces" files. Follow the example file and fill all the attributes accordingly.

To add an NPC, modify the "Assets/Data/Names/names.json" file and add the name of your npc.

To add a Robber, follow the files on "Assets/Data/Robbers"

To modify the dialogues, open "Assets/Data/Dialogues/dialogues.json". Misc are dialogues that don't give hints to the player. Helpful gives hints about the next place to travel. [city] will be replaced for the next city, and [place] for the name of the next place in that city. robber gives information about the criminal and [robber] will be replaced by one random attribute of the robber. [artPiece] command is to reffer for a random art piece of a museum that you need to visit next.

Finally, add an image with the same name used on the "name" fields of the files in the respective folders of "Assets/Data/Sprites". The image needs to be of .png format or else it won't be loaded.
