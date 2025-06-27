namespace StateMachine.Model.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTimeOffset SignupDate { get; set; }
}