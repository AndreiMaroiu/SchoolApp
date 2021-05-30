namespace DatabaseWPF.Models.DatabaseEntities
{
    public class Professor : DatabaseObject
    {
        public Professor() { }

        public int Person_ID { get; set; }

        public int Subject_ID { get; set; }

        public int? Class_ID { get; set; }

        public override string ToString()
            => $"{Person_ID} {Class_ID} {Subject_ID} {Class_ID}";

    }
}
