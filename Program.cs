using Sudoku;
using System.Diagnostics;

class Program {
    private static Stopwatch stopwatch = new Stopwatch();
    private static int[,] board;// A board with which the user interacts
    private static int[,] boardFull;// Generated solution
    private static bool[,] boardSafePosition;// Starting positions
    private static bool tookHint;
    private static SudokuSequence hint;
    private static int removedDigits = 0;
    private static List<SudokuLog> log = new List<SudokuLog>();
    private static Stack<SudokuSequence> sequence = new Stack<SudokuSequence>();

    private static void Main(string[] args) {
        tookHint = false;
        removedDigits = 20;
        Restart();
    }

    private static void PrintBoardMain() {
        int col, row;
        //need to move here after hint O
        if (!tookHint) {
            col = InputToInt("Input Column: ", 1, 9);
            if (col == -1) return;
            ClearConsole();
            SudokuHandler.PrintBoard(board, ConsoleColor.White, gameAction: GameAction.InputRow, selectedCol: col - 1);
            row = InputToInt("Input Row: ", 1, 9);
            if (row == -1) return;
            if (boardSafePosition[row - 1, col - 1]) {// Can`t change safe position
                ClearConsole();
                SudokuHandler.PrintBoard(board, ConsoleColor.Green, GameAction.InputColumn, col - 1, row - 1);
                return;
            }
            ClearConsole();
            SudokuHandler.PrintBoard(board, ConsoleColor.Red, 0, col - 1, row - 1);
        }
        else {
            row = hint.positionRow + 1;
            col = hint.positionCol + 1;
        }
        tookHint = false;
        //need to move here after hint H
        int numberToInsert = InputToInt("Input number to insert: ", 1, 9);
        if (numberToInsert == -1) return;
        bool condition = SudokuHandler.IsSafe(board, row - 1, col - 1, numberToInsert);//need to do this before adding number to board
        int numberBefore = board[row - 1, col - 1];
        sequence.Push(new SudokuSequence(row, col, numberBefore, numberToInsert));
        if (board[row - 1, col - 1] == 0) {
            log.Add(new SudokuLog(row, col, numberBefore, numberToInsert, Const.ACTION_INSERT));
        }
        else {
            log.Add(new SudokuLog(row, col, numberBefore, numberToInsert, Const.ACTION_REPLACE));
        }
        board[row - 1, col - 1] = numberToInsert;

        ClearConsole();
        if (condition) {
            SudokuHandler.PrintBoard(board, ConsoleColor.Blue, GameAction.InputColumn, col - 1, row - 1);
        }
        else {
            SudokuHandler.PrintBoard(board, ConsoleColor.Yellow, GameAction.InputColumn, col - 1, row - 1);
        }

        CheckingIfBoardFull();
    }

    private static bool IsAllSafe() {
        for (int i = 0; i < board.GetLength(0) - 1; i++) {
            for (int j = 0; j < board.GetLength(1) - 1; j++) {
                if (SudokuHandler.IsSafePlaced(board, i, j, board[i, j])) {
                    //all good
                }
                else {
                    return false;
                }
            }
        }
        return true;
    }

    private static void CheckingIfBoardFull()  {
        if (IsBoardFull(board) && IsAllSafe()) {
            string messageToRestart = "To start a new game write [r], to change the number of missing digits write [d]";
            stopwatch.Stop();
            ClearConsole();
            SudokuHandler.PrintBoard(board, ConsoleColor.Gray);
            Console.WriteLine($"Congratulations, you have completed the game in {stopwatch.Elapsed:mm\\:ss}, the number of missing digits was {removedDigits}.");
            log.Add(new SudokuLog(0, 0, 0, 0, Const.ACTION_WON_GAME));
            while (true) {
                Console.WriteLine(messageToRestart);
                var inputKey = Console.ReadKey();
                if (inputKey.KeyChar == 'r') {
                    Restart();
                }
                else if (inputKey.KeyChar == 'd') {
                    int inputDigits = InputToInt("Input the number of missing digits u want [40-70]: ", 40, 70);
                    removedDigits = inputDigits;
                    Restart();
                }
                ClearConsole();
            }
        }
    }

    private static bool IsBoardFull(int[,] board) {
        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1); j++) {
                if (board[i, j] == 0) return false;
            }
        }
        return true;
    }

    private static void Restart() {
        boardFull = SudokuHandler.GenerateSudoku();
        board = SudokuHandler.RemoveNumbers(boardFull, removedDigits);
        AsignSafePositionsBoard();
        Thread.Sleep(1000);
        ClearConsole();
        SudokuHandler.PrintBoard(board, ConsoleColor.Gray, GameAction.InputColumn);

        stopwatch.Reset();
        stopwatch.Start();

        while (true) {
            PrintBoardMain();
        }
    }

    private static void AsignSafePositionsBoard() {
        boardSafePosition = new bool[boardFull.GetLength(0), boardFull.GetLength(1)];
        for (int i = 0; i < boardFull.GetLength(0); i++) {
            for (int j = 0; j < boardFull.GetLength(1); j++) {
                if (boardFull[i, j] == 0) boardSafePosition[i, j] = false;
                else boardSafePosition[i, j] = true;
            }
        }
    }

    public static int InputToInt(string message, int fromIndex, int toIndex) {
        int inputToReturn = 0;
        bool isValidInput = false;

        while (!isValidInput) {
            Console.Write(message);
            var inputString = Console.ReadKey();
            if (toIndex > 10) {
                var inputString2 = Console.ReadKey();
                if (int.TryParse(inputString.KeyChar.ToString(), out inputToReturn)) {
                    if (int.TryParse(inputString2.KeyChar.ToString(), out int inputToReturnConcat)) {
                        int combinedInt = int.Parse(String.Concat(inputString.KeyChar.ToString(), inputString2.KeyChar.ToString()));
                        if (combinedInt >= fromIndex && combinedInt <= toIndex) {
                            return combinedInt;
                        }
                        continue;
                    }
                }
                else {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
                continue;
            }

            if (inputString.Key == ConsoleKey.Backspace) {
                if (sequence.TryPeek(out SudokuSequence previousMove)) {
                    board[previousMove.positionRow - 1, previousMove.positionCol - 1] = previousMove.numberBefore;
                    log.Add(new SudokuLog(previousMove.positionRow, previousMove.positionCol, previousMove.numberBefore, previousMove.numberAfter, Const.ACTION_DELETE));
                    ClearConsole();
                    SudokuHandler.PrintBoard(board, ConsoleColor.White, GameAction.InputColumn, previousMove.positionCol - 1, previousMove.positionRow - 1);
                    sequence.Pop();
                }
                else {
                    Console.WriteLine("Sequence is empty!");
                }
                continue;
            }
            else if (inputString.Key == ConsoleKey.Tab) {
                ClearConsole();
                for (int i = 0; i < log.Count; i++) {
                    Console.WriteLine(i + log[i].ToString());
                }
                Console.ReadKey();
                ClearConsole();
                SudokuHandler.PrintBoard(board, ConsoleColor.White);
                continue;
            }
            else if (inputString.Key == ConsoleKey.H) {// Position hint
                hint = SudokuHandler.GetHint(board);
                if (hint.numberAfter == 0) { Console.WriteLine("not found"); continue; }
                tookHint = true;
                ClearConsole();
                SudokuHandler.PrintBoard(board, ConsoleColor.Red, 0, hint.positionCol, hint.positionRow);
                return -1;
            }
            else if (inputString.Key == ConsoleKey.O) {// Opens number
                hint = SudokuHandler.GetHint(board);
                if (hint.numberAfter == 0) { Console.WriteLine("not found"); continue; }
                sequence.Push(new SudokuSequence(hint.positionRow+1, hint.positionCol+1, hint.numberBefore, hint.numberAfter));
                log.Add(new SudokuLog(hint.positionRow, hint.positionCol, hint.numberBefore, hint.numberAfter, Const.ACTION_HINT));

                board[hint.positionRow, hint.positionCol] = hint.numberAfter;

                ClearConsole();
                SudokuHandler.PrintBoard(board, ConsoleColor.Blue, GameAction.InputColumn, hint.positionCol, hint.positionRow);
                CheckingIfBoardFull();
                return -1;
            }

            if (int.TryParse(inputString.KeyChar.ToString(), out inputToReturn)) {
                if (inputToReturn >= fromIndex && inputToReturn <= toIndex) {
                    isValidInput = true;
                }
                else {
                    Console.WriteLine($"Please enter a number between {fromIndex} and {toIndex}.");
                }
            }
            else {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                Console.WriteLine("inputString is - " + inputString);
            }
        }

        return inputToReturn;
    }

    private static void ClearConsole() {
        Console.Clear();
        Console.WriteLine($"Time elapsed: {stopwatch.Elapsed:mm\\:ss}");
    }
}
