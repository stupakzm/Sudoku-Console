## Sudoku Console Game

### Overview

This project is a console-based Sudoku game developed in C#. The game features a dynamically generated Sudoku puzzle with a customizable difficulty level based on the number of digits removed from the complete board. The primary goal is to provide a user-friendly and engaging way to play Sudoku directly from your console.

### Features

**Dynamic Puzzle Generation**

Randomized Sudoku Grid: At the start of each game, the software generates a complete and valid 9x9 Sudoku grid.
Digit Removal for Difficulty: The game initially removes a specific number of digits (default is 20) to create the puzzle. This number can be adjusted by the player to increase or decrease the difficulty.

**Interactive Gameplay**

- Cell Selection: Players select a cell to fill by entering the coordinates (column and row). The selected cell is highlighted in a different color to distinguish it.
- Number Insertion: After selecting a cell, the player inputs a number. If the number is valid for that cell according to Sudoku rules, it is placed; otherwise, feedback is provided via color coding:
Red: Indicates an invalid or unsafe placement.
Blue: Indicates a correct placement under Sudoku rules.
Yellow: Used when a number violates Sudoku constraints.
Green: Marks cells that cannot be changed (initial numbers of the puzzle).

- Hints and Corrections: Players can opt to receive hints or correct wrong entries, which are also color-highlighted to indicate the suggested numbers.

**Game Progress and Logging**

- Undo Actions: The game supports undoing the last action, allowing players to correct mistakes without restarting the game.
- Game Logs: Each action (insert, replace, hint) is logged for potential review, providing insights into the game progress.
- Stopwatch: A built-in timer keeps track of the duration spent on the current game, encouraging players to improve their solving speed.

**Endgame and Restart**

- Completion Check: The game checks if the board is completely and correctly filled. Upon successful completion, a congratulatory message is displayed, including the time taken to complete the puzzle and the number of digits initially removed.
- Restart Options: Players can start a new game or adjust the number of digits removed for a new challenge without exiting the program.

![Sudoku](https://github.com/stupakzm/Sudoku-Console/blob/main/sudokuEx.png)

**Technical Implementation**

- Data Structures: The game utilizes matrices to store the board states and boolean arrays to track which cells contain initial numbers versus player-added numbers.
- Custom Commands and Input Handling: Implements custom command handling for game actions like requesting hints, undoing moves, or restarting the game, enhancing interactive gameplay.
- Performance Optimization: Utilizes efficient algorithms to ensure that the game's performance remains optimal, even with complex board states and frequent updates.


This console-based Sudoku game combines traditional Sudoku gameplay with the convenience of a command-line interface, tailored for both beginners and seasoned players looking for a quick Sudoku challenge.
