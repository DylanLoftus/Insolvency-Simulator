# Insolvency-Simulator
4th Year Mobile Applications Unity Project

# Developer Diary

The game that I will be developing is called "Insolvency Simulator" which is a clone and tweak of Monopoly.

## Week 1

After receiving the design document I quickly realized that the scope of the project was quite large given the deadline of this
project. I emailed my designer and asked if some of the features could be taken out to shape the project to the deadline.

"Hello David,

I was wondering if it would be possible to scale the project back a bit? Would it be okay if the auto-save, trade,
auction and bonds feature be left out as I'd find it hard to implement all of these features with the given deadline.

All the best,
Dylan."

After that I began work on the project by added temporary sprites.

To create the game board I thought it would be best to use a temporary monopoly board sprite and place a GameObject on each tile.
Eeach GameOject would have a transform (x,y) associated with it which could be used to move the player GameObjects around the
board.

I also made a Dice script. This script generates 2 random numbers and store them for player movement.

## Week 2

This week I created a barebones main menu with "Play Game", "Load Game", "Settings", and "Quit" buttons. No functionality added
yet.

Also created property and player scripts. These scripts contain data that is specific to a property or a player. For example a
player has their "Player ID" and a property has a boolean "IsOwned".

## Week 3

This week I added player movement. I only used 1 player for the moment. To get the player moving from tile to tile I used a linked
list of tile transforms. Each tile knew which the next tile was. The Dice script generates the number of spaces a player moves
and the player moves that random number of "next tiles".

## Week 4

Originally I thought a good way of getting the game to realize what type of tile a player has landed on would be to use
colliders but that proved to be getting me nowhere so instead I gave each type of tile a "tile type" variable (Property, Go, Jail etc). I quickly realized at this point that I needed some sort of GameManager to handle all of this. When a player rolls the dice a method inside the GameManager script "CheckTileType()" is run which loops over every tile until the player's transform matches the transform of the tile the player is sat on. After this a switch statement is ran comparing the 'Tile type' variable with the varius tile types in the game. After this a method is ran depending on which type of tile the player has landed on for example "PropertyHandler()". I thought this would be the best way of handling all interactions in the game.

## Week 5

This week I thought it would be good to add more players to the game. I'd also have to implement a turn system as well. I thought the best way to do this would be to have 3 booleans in the gamemanager that would track each player's turn. One for when a player is done rolling the dice, moving, and having an interaction(example buying a property). I also would have to check who's turn it is. To do this I used a variable called "CurrentPlayerID" and compared it to all the player objects. If the IDs matched it was that players turn. After all the booleans are set to true the player's turn is over and the NewTurn() method is called which increments the "CurrentPlayerID" by 1 and sets all the player turn booleans to false.

## Week 6

This week was heavily focused on implementing the functionality to each tile. I also added the logic for if a player rolled a double which caused some major problems. If a player rolled a double a bool "DoubleRoll" was set to true and at the end of the player's turn a method is ran to check if a double was rolled. If the bool is true reset all the player turn bools to false but DON'T increment the "CurrentPlayerID" by 1. The probelm that this caused for a while was that I was never setting the "DoubleRoll" bool back to false. As a result the GameManager thought that the player's were rolling doubles all the time and as a result of this all the players ended up in jail because the GameManager thought they had rolled 3 doubles. After a load of troubleshooting I realized what the mistake was.

## Week 7
This week was focused on implementing all tile types. I've already got the CheckTileType() method letting the GameManager know what tile a player is on and all I had to do was create methods which ran depending on which case was met in the switch statement. To get the chance cards/community chests working I created a 2 GameObjects (ChanceCards and CommunityChests) and populated them with GameObjects with a script attatched to them holding a ChanceCard/CommunityChest number and the cards action which was just a String for the UI later on. I then created a ChanceCard/Community chest array and picked a random index of that array and ran that through a switch statement that implemented the ChanceCard/Community chest's action. The RailRoad tiles work exaclty the same as property tiles. The GoToJail tile sets the players transfrom and currentTile to the JailTile. I also implemented the landed/passing Go feature. Originally the player would only get €200 if they landed on go but now if their nextTile[] is the GoTile that is when the €200 is added. I also added the lose condition. If a player has <= 0 money they get booted from the game. To do this I just set their gameObject to false, set all of their isOwned properties to false and if they had any houses to remove them also but monopolies/houses will be added later.

## Week 8
This week was mainly focused around UI, adding music and tidying up any bugs. With the UI I added UI elements for every scenario for example when a player lands on a property that isn't owned they are asked through UI if they would like to buy that house. I also added monopoly detection so that if a player landed on a property owned by themselves it would check to see if they had a monopoly(owned all the properties of the same colour). Originally there was a huge bug with this as the way I was going about checking it was checking if the player's ID matched the ownedPlayerID of each property in that group. By default the ownedPlayerID variable was set to 0 so if player 1(who's id is 0) landed on a property they owned and let's say there were 2 properties in that group with only 1 of them owned the game would think that player 1 had a monopoly as both properties have a ownedPlayerID of 0. After all bug fixes and I built the game to both Android and PC.
