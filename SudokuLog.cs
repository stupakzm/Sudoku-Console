using Sudoku;

public struct SudokuLog {//Recording every action made by player. Can be viewed by pressing TAB
    private readonly int left;
    private readonly int top;
    private readonly int numberBefore;
    private readonly int numberAfter;
    private readonly string action;


    public SudokuLog(int left, int top, int numberBefore, int numberAfter, string action)
    {
        this.left = left;
        this.top = top;
        this.numberBefore = numberBefore;
        this.numberAfter = numberAfter;
        this.action = action;
    }

    public override string ToString() {
        switch (action) {
            case Const.ACTION_WON_GAME: return $" Log: {action}.";
            case Const.ACTION_INSERT: return $" Log: number [{numberAfter}] was {action} in {left} row, {top} column.";
            case Const.ACTION_REPLACE: return $" Log: number [{numberBefore}] was {action} with {numberAfter} in {left} row, {top} column.";
            case Const.ACTION_DELETE: return $" Log: number [{numberAfter}] was {action} and placed previous {numberBefore} in {left} row, {top} column.";
            case Const.ACTION_HINT: return $" Log: number [{numberAfter}] was {action} in {left} row, {top} column.";
        }
       // string aa = action == Const.ACTION_WON_GAME ? $" Log: {action}." : $" Log: number [{number}] was {action} in {left} row, {top} column.";
        return "Problem with Log";
    }
}