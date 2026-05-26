namespace BookRight.Domain.ValueObjects
{
    public sealed record Authorization //Represents a therapist authorization with type and authorization number
    {
        public string Type { get; }
        public string Number { get; }

        public Authorization(string type, string number)
        {
            Type = type;
            Number = number;
        }
    }
}