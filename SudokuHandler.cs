using Sudoku;

public static class SudokuHandler {
    private const int GridSize = 9;
    private static int[] indexColSelected = { 2, 4, 6, 10, 12, 14, 18, 20, 22 };// Need to visualize selected column

    // Generate a Sudoku puzzle
    public static int[,] GenerateSudoku() {
        int[,] board = new int[GridSize, GridSize];
        GenerateSudokuHelper(board);
        return board;
    }

    // Helper function to generate Sudoku recursively
    private static bool GenerateSudokuHelper(int[,] board) {
        int row, col;

        // Find an unassigned cell
        if (!FindUnassignedCell(board, out row, out col)) {
            return true; // If all cells are assigned, puzzle is complete
        }

        // Generate random permutation of numbers 1 to GridSize
        int[] numbers = GenerateRandomPermutation();

        // Try placing a number in the cell
        foreach (int num in numbers) {
            if (IsSafe(board, row, col, num)) {
                board[row, col] = num;

                // Recur to fill remaining cells
                if (GenerateSudokuHelper(board)) {
                    return true; // If solution found, return true
                }

                // If placing num at (row, col) doesn't lead to solution, backtrack
                board[row, col] = 0;
            }
        }
        // No number can be placed, return false to trigger backtracking
        return false;
    }

    // Find an unassigned cell
    private static bool FindUnassignedCell(int[,] board, out int row, out int col) {
        for (row = 0; row < GridSize; row++) {
            for (col = 0; col < GridSize; col++) {
                if (board[row, col] == 0) {
                    return true;
                }
            }
        }
        row = -1; col = -1; // If no unassigned cell found
        return false;
    }

    // Check if it's safe to place num at (row, col)
    public static bool IsSafe(int[,] board, int row, int col, int num) {
        // Check row and column
        for (int i = 0; i < GridSize; i++) {
            if (board[row, i] == num || board[i, col] == num) {
                return false;
            }
        }

        // Check 3x3 square
        int boxRow = row - row % 3;
        int boxCol = col - col % 3;
        for (int i = boxRow; i < boxRow + 3; i++) {
            for (int j = boxCol; j < boxCol + 3; j++) {
                if (board[i, j] == num) {
                    return false;
                }
            }
        }

        return true;
    }

    // Check if it's safe placed num at (row, col)
    public static bool IsSafePlaced(int[,] board, int row, int col, int num) {
        // Check row and column
        for (int i = 0; i < GridSize; i++) {
            if (board[row, i] == num && i == col) {
                continue;
            }
            if (board[i, col] == num && i == row) {
                continue;
            }
            if (board[row, i] == num || board[i, col] == num) {
                return false;
            }
        }

        // Check 3x3 square
        int boxRow = row - row % 3;
        int boxCol = col - col % 3;
        for (int i = boxRow; i < boxRow + 3; i++) {
            for (int j = boxCol; j < boxCol + 3; j++) {
                if (i == row && j == col) {
                    continue;
                }
                if (board[i, j] == num) {
                    return false;
                }
            }
        }

        return true;
    }

    // Generate a random permutation of numbers 1 to GridSize
    private static int[] GenerateRandomPermutation() {
        int[] numbers = new int[GridSize];
        for (int i = 0; i < GridSize; i++) {
            numbers[i] = i + 1;
        }

        Random rand = new Random();
        for (int i = GridSize - 1; i > 0; i--) {
            int j = rand.Next(i + 1);
            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        return numbers;
    }

    public static void PrintBoard(int[,] board, ConsoleColor color, GameAction gameAction = 0, int selectedCol = -1, int selectedRow = -1) {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Inserted, ");
        Console.ForegroundColor = ConsoleColor.Red;
        //Console.Write("         Selected");
        Console.Write("Selected, ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Warning, ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Safe Position");
        Console.ResetColor();
        Console.WriteLine();
        char[] line = AsignLine();

        for (int i = 0; i < GridSize; i++) {
            if (i % 3 == 0) {
                if (i == 0 && gameAction == GameAction.InputColumn) {// Visualization before user input of the column
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(line);
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else if (i == 0 && selectedCol != -1) {// Visualization of the selected column
                    for (int k = 0; k < line.Length; k++) {
                        if (k == indexColSelected[selectedCol]) {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(line[k]);
                            Console.ResetColor();
                        }
                        else {
                            Console.Write(line[k]);
                        }
                    }
                    Console.WriteLine();
                }
                else {
                    Console.WriteLine(line);
                }
            }
            for (int j = 0; j < GridSize; j++) {
                if (j % 3 == 0) {
                    if (gameAction == GameAction.InputRow && j == 0) {// Visualization before user input of the row
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("| ");
                        Console.ResetColor();
                    }
                    else if (selectedRow == i && j == 0 && gameAction != GameAction.InputColumn) {// Visualization of the selected row
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("| ");
                        Console.ResetColor();
                    }
                    else {
                        Console.Write("| ");
                    }
                }

                string boardSymbol = board[i, j] == 0 ? "*" : board[i, j].ToString();
                if (selectedRow == i && selectedCol == j) {// Print the selected position in color
                    Console.ForegroundColor = color;
                    Console.Write("{0} ", boardSymbol);
                    Console.ResetColor();
                    continue;
                }

                boardSymbol = board[i, j] == 0 ? "~" : board[i, j].ToString();
                if (j == GridSize - 1) {
                    Console.Write("{0} ", boardSymbol);
                }
                else {
                    Console.Write("{0} ", boardSymbol);
                }

            }
            Console.Write("|");
            Console.WriteLine();
        }
        Console.WriteLine(line);
        Console.WriteLine(FindAllFoundNumbers(board));
    }

    private static string FindAllFoundNumbers(int[,] board) {
        string stringToReturn = "Found numbers: ";
        int[] numbers = new int[GridSize];
        for (int i = 0; i < GridSize; i++) {
            for (int j = 0; j < GridSize; j++) {
                if (board[i, j] != 0) {
                    numbers[board[i, j] - 1]++;
                }
            }
        }
        for (int i = 0; i < GridSize; i++) {
            if (numbers[i] >= GridSize) {
                //stringToReturn.Concat((i + 1) + " ");
                stringToReturn += (i + 1 + " ").ToString();
            }
            else {
                //stringToReturn.Concat("~ ");
                stringToReturn += "~ ";
            }
        }
        return stringToReturn;
    }

    public static int[,] RemoveNumbers(int[,] board, int numberOfCellsToRemove) {// numberOfCellsToRemove [40-70]
        Random random = new Random();
        int cellsRemoved = 0;

        while (cellsRemoved < numberOfCellsToRemove) {
            int row = random.Next(GridSize);
            int col = random.Next(GridSize);

            // Skip if the cell is already empty
            if (board[row, col] == 0) {
                continue;
            }

            // Backup the current value in case we need to backtrack
            int backup = board[row, col];
            board[row, col] = 0; // Remove the number from the cell

            // Check if the puzzle still has a unique solution
            if (!HasUniqueSolution(board)) {
                // If the puzzle doesn't have a unique solution, revert the change
                board[row, col] = backup;
            }
            else {
                cellsRemoved++;
            }
        }
        return board;
    }

    private static bool HasUniqueSolution(int[,] board) {
        int[,] tempBoard = (int[,])board.Clone(); // Clone the board to avoid modifying the original
        return SolveSudoku(tempBoard);
    }

    public static bool SolveSudoku(int[,] board) {
        int row = 0, col = 0;
        bool isDone = true;

        // Find unassigned cell
        for (row = 0; row < GridSize; row++) {
            for (col = 0; col < GridSize; col++) {
                if (board[row, col] == 0) {
                    isDone = false;
                    break;
                }
            }
            if (!isDone) {
                break;
            }
        }

        // No unassigned cells left
        if (isDone) {
            return true;
        }

        // Try every digit from 1 to 9
        for (int num = 1; num <= GridSize; num++) {
            if (IsSafe(board, row, col, num)) {
                board[row, col] = num;
                if (SolveSudoku(board)) {
                    return true;
                }
                board[row, col] = 0; // backtrack
            }
        }

        return false;
    }

    public static SudokuSequence GetHint(int[,] board) {
        int row = 0, col = 0;
        bool isDone = true;

        // Find unassigned cell
        for (row = 0; row < GridSize; row++) {
            for (col = 0; col < GridSize; col++) {
                if (board[row, col] == 0) {
                    for (int num = 1; num <= GridSize; num++) {
                        if (IsSafe(board, row, col, num)) {
                            // Check 3x3 square
                            int boxRow = row - row % 3;
                            int boxCol = col - col % 3;
                            bool definitely = true;
                            for (int i = boxRow; i < boxRow + 3; i++) {
                                for (int j = boxCol; j < boxCol + 3; j++) {
                                    if ((i == row && j == col) || !definitely) {
                                        continue;
                                    }
                                    if (board[i, j] == 0) {
                                        // Check row and column
                                        if (IsSafe(board, i, j, num)) {
                                            definitely = false;
                                        }
                                    }
                                }
                            }
                            if (definitely) {
                                return new SudokuSequence(row, col, board[row, col], num);
                            }
                        }
                    }
                }
            }
            if (!isDone) {
                break;
            }
        }
        return new SudokuSequence();
    }
    private static char[] AsignLine() {
        int length = 25;
        char[] result = new char[length];
        for (int i = 0; i < length; i++) {
            result[i] = '-';
        }
        return result;
    }
}