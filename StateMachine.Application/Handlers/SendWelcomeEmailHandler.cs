using MassTransit;
using StateMachine.Application.Abstraction.Services;
using StateMachine.Contracts.Commands;
using StateMachine.Contracts.Events;
using StateMachine.Data;

namespace StateMachine.Application.Handlers;

public class SendWelcomeEmailHandler(IEmailService emailService) : IConsumer<SendWelcomeEmail>
{
    public async Task Consume(ConsumeContext<SendWelcomeEmail> context)
    {
        await emailService.SendWelcomeEmailAsync(context.Message.Email);

        await context.Publish(new WelcomeEmailSent
        {
            CustomerId = context.Message.CustomerId,
            Email = context.Message.Email,
        });
    }
}