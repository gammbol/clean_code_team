namespace MediatorPatternExample
{
    // Базовый класс участника
    public abstract class Participant
    {
        protected readonly IThesisMediator Mediator;

        public string Name { get; }

        protected Participant(
            string name,
            IThesisMediator mediator)
        {
            Name = name;

            Mediator = mediator;
        }
    }
}