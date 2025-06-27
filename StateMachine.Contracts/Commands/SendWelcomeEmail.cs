namespace StateMachine.Contracts.Commands;

public record SendWelcomeEmail(Guid CustomerId, string Email);
