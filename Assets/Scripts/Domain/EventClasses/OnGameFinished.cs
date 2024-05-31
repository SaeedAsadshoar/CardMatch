namespace Domain.EventClasses
{
    public class OnGameFinished
    {
        public bool IsWin { get; private set; }
        public int WinCount { get; private set; }
        public int ExtraTime { get; private set; }
        public bool IsNewRecord { get; private set; }
        public int Score { get; private set; }

        public OnGameFinished(bool isWin, int winCount, int extraTime, bool isNewRecord, int score)
        {
            IsWin = isWin;
            WinCount = winCount;
            ExtraTime = extraTime;
            IsNewRecord = isNewRecord;
            Score = score;
        }
    }
}