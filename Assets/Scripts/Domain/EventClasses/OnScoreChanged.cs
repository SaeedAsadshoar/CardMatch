namespace Domain.EventClasses
{
    public class OnScoreChanged
    {
        public int Score { get; private set; }
        public int Combo { get; private set; }

        public OnScoreChanged(int score, int combo)
        {
            Score = score;
            Combo = combo;
        }
    }
}