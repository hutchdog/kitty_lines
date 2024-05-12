namespace Code
{
    public enum GameplayState : uint
    {
        Undefined = 0,
        Init, // Setup game rules and settings
        Drop, // Create new tetramino
        UpdateInput, // Loop with vertical offsets and rotations
        UpdateDrop, // Loop with horizontal offset
        CheckLines, // When movement is not possible, calculate lines and scores
        Win,
        Lose
    }
}