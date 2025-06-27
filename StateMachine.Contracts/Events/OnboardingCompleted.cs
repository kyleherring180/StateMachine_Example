namespace StateMachine.Contracts.Events;

public class OnboardingCompleted
{
    public Guid CustomerId { get; set; }
    public string Email { get; set; }
}