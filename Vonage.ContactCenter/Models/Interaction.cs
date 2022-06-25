namespace Vonage.ContactCenter.Models
{
    public class Interaction
    {
        public InteractionTypeEnum Type { get; init; }
        public Task Task { get; set; }

        
        public Interaction(InteractionTypeEnum type, TimeSpan executeAfter, Action<Task> afterCompilation)
        {
            Type = type;
            Task.Delay(executeAfter).ContinueWith(afterCompilation);
        }
    }
}
