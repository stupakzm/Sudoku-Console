# **SudokuSolver**

This is a C# implementation of a Sudoku solver. The program uses a backtracking algorithm to solve a given Sudoku puzzle.

****How It Works****

**The SudokuSolver class contains the following methods:**

**SolveSudoku(int[,] board):** This method takes a 2D integer array representing the Sudoku board as input and attempts to solve the puzzle. It uses a backtracking algorithm to find a valid solution. The method returns true if a solution is found, and false otherwise.

**IsSafe(int[,] board, int row, int col, int num):** This method checks if it is safe to place the given number (num) at the specified position (row, col) in the Sudoku board. It verifies the number's validity in terms of rows, columns, and 3x3 squares.

**PrintBoard(int[,] board):** This method prints the Sudoku board in a formatted manner, displaying the current state of the puzzle.

**Main():** The entry point of the program. It initializes a Sudoku board with some initial values and calls the SolveSudoku method to solve the puzzle. If a solution is found, it prints the solved Sudoku board using the PrintBoard method.


****Usage****

To use the Sudoku solver, you can create an instance of the SudokuSolver class and call the SolveSudoku method, passing in a Sudoku board as a 2D integer array. The solver will modify the board in-place, filling in the missing numbers to solve the puzzle.


The PrintBoard method can be used to print the Sudoku board at any point to visualize the current state of the puzzle.
