namespace Vonage.ContactCenter.Models
{
    public class Interaction
    {
        public Guid Id { get; init; }
        public InteractionTypeEnum Type { get; init; }
        public int CompletesAfterMilliseconds { get; init; }
        public Interaction(InteractionTypeEnum type, int completesAfterMilliseconds)
        {
            Type = type;
            CompletesAfterMilliseconds = completesAfterMilliseconds;
            Id = Guid.NewGuid();
        }
    }
}
