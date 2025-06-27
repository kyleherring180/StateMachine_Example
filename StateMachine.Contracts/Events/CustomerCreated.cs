namespace StateMachine.Contracts.Events;

public class CustomerCreated
{
    public Guid CustomerId { get; set; }
    public string Email { get; set; }
    public DateTimeOffset SignUpDate { get; set; }
}