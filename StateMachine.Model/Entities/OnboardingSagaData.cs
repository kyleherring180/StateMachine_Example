using MassTransit;

namespace StateMachine.Model.Entities;

public class OnboardingSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }

    //Supporting Data
    public Guid CustomerId { get; set; }
    public string Email { get; set; }
    public bool WelcomeEmailSent { get; set; }
    public bool FollowUpEmailSent { get; set; }
    public bool OnboardingCompleted { get; set; }
    
}