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
