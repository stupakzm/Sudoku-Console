public class SudokuSolver {
    private const int GridSize = 9;

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

    public static void PrintBoard(int[,] board) {
        string line = "---------------------";
        for (int i = 0; i < GridSize; i++) {
            if (i % 3 == 0) {
                Console.WriteLine(line);
            }
            for (int j = 0; j < GridSize; j++) {
                if (j % 3 == 0) {
                    Console.Write("|");
                }
                if (j == GridSize - 1) {
                    Console.Write("{0}|", board[i, j]);
                }
                else {
                    Console.Write("{0} ", board[i, j]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine(line);
    }

    public static void Main() {
        int[,] board = new int[,]{
    { 0,3,1,0,6,0,0,0,0},
    { 6,0,0,0,0,0,0,0,5},
    { 9,0,0,0,2,0,8,0,1},
    { 7,0,0,0,0,3,0,0,6},
    { 0,0,0,0,0,0,0,0,7},
    { 0,0,9,2,0,0,0,0,0},
    { 0,0,8,7,0,0,4,3,0},
    { 0,1,6,9,0,0,5,7,2},
    { 0,0,0,5,4,0,0,0,0},
};

        if (SolveSudoku(board))
            PrintBoard(board);
    }

}