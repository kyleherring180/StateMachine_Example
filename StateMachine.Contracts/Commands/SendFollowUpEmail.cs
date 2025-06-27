namespace StateMachine.Contracts.Commands;

public record SendFollowUpEmail(Guid CustomerId, string Email);