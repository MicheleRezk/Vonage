using System.Collections.Concurrent;
using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Common
{
    public class Employee : IEmployee
    {
        public string Name { get; init; }
        public EmployeeTypeEnum Type { get; init; }
        public ICollection<Interaction> RunningInteractions { get; init; }
        public bool IsFree => RunningInteractions.Count == 0;
        public bool CanHandleOnlyNonVoice
        {
            get
            {
                return
                    RunningInteractions.Count < MaximumInteractionAtATime
                    &&
                    RunningInteractions.All(i => i.Type == InteractionTypeEnum.NonVoice);
            }
        }
        public int MaximumInteractionAtATime { get; init; }
        private object lockObj = new object();

        public Employee(string name, EmployeeTypeEnum type, int maxInteractionsAtTime)
        {
            Name = name;
            Type = type;
            MaximumInteractionAtATime = maxInteractionsAtTime;
            RunningInteractions = new List<Interaction>(MaximumInteractionAtATime);
        }

        public async Task HandleInteraction(Interaction interaction)
        {
            lock(lockObj)
            {
                if(RunningInteractions.Count < MaximumInteractionAtATime)
                {
                    //ToDO: replace with logger
                    Console.WriteLine($"{Name} Started handling {interaction.Type} interaction at {DateTime.UtcNow}");
                    RunningInteractions.Add(interaction);
                }
            }
            //execute it
            if (RunningInteractions.Any(i => i.Id == interaction.Id))
            {
                await Task.Delay(interaction.CompletesAfterMilliseconds);
                OnInteractionCompleted(interaction);
            }

        }
        public void OnInteractionCompleted(Interaction interaction)
        {
            lock (lockObj)
            {
                if (RunningInteractions.Any(i => i.Id == interaction.Id))
                {
                    Console.WriteLine($"{Name} finished handling {interaction.Type} interaction at {DateTime.UtcNow}");
                    RunningInteractions.Remove(interaction);
                }
            }
        }

        public int CompareTo(IEmployee? other)
        {
            return this.Type.CompareTo(other?.Type);
        }
    }
}
