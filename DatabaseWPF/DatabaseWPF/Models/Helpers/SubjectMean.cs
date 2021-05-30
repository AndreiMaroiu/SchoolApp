namespace DatabaseWPF.Models.Helpers
{
    public class SubjectMean
    {
        public SubjectMean(float score, string subject)
            => (Score, Subject) = (score, subject);

        public float Score { get; set; }

        public string Subject { get; set; }

        public override string ToString()
            => $"{Subject}: {Score}";

        public static bool operator <(SubjectMean left, SubjectMean right)
            => left.Score < right.Score;

        public static bool operator >(SubjectMean left, SubjectMean right)
            => left.Score > right.Score;
    }
}
