namespace Sudoku {
    public struct SudokuSequence {//need to create sequence for backspace -> to UNDO move
        public int positionRow;
        public int positionCol;
        public int numberBefore;
        public int numberAfter;

        public SudokuSequence(int positionRow, int positionCol, int numberBefore, int numberAfter)
        {
            this.positionRow = positionRow;
            this.positionCol = positionCol;
            this.numberBefore = numberBefore;
            this.numberAfter = numberAfter;
        }
    }
}
