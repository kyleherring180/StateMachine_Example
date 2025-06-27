using MassTransit;
using StateMachine.Contracts.Commands;
using StateMachine.Contracts.Events;
using StateMachine.Data;

namespace StateMachine.Application.Handlers;

//Basically looks like a Service method to me. but following the pattern of the tutorial.
public class SignupHandler(StateMachineDbContext dbContext) : IConsumer<SignUp>
{
    public async Task Consume(ConsumeContext<SignUp> context)
    {
        //FYI: I know this is not the norm and should be separated into it's own repository or service, but this is a simple example.
        var newCustomer = dbContext.Customers.Add(new Model.Entities.Customer
        {
            Id = Guid.NewGuid(),
            Email = context.Message.Email,
            SignupDate = context.Message.SignUpDate
        });
        
        await dbContext.SaveChangesAsync();
        
        await context.Publish(new CustomerCreated
        {
            CustomerId = newCustomer.Entity.Id,
            Email = newCustomer.Entity.Email,
            SignUpDate = newCustomer.Entity.SignupDate
        });
    }
}