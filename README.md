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

I also made a Dice script. This script would generate 2 random numbers and store them for player movement.
