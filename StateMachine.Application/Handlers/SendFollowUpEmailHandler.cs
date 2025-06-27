using MassTransit;
using StateMachine.Application.Abstraction.Services;
using StateMachine.Contracts.Commands;
using StateMachine.Contracts.Events;

namespace StateMachine.Application.Handlers;

public class SendFollowUpEmailHandler(IEmailService emailService) : IConsumer<SendFollowUpEmail>
{
    public async Task Consume(ConsumeContext<SendFollowUpEmail> context)
    {
        await emailService.SendFollowUpEmailAsync(context.Message.Email);

        await context.Publish(new FollowUpEmailSent
        {
            CustomerId = context.Message.CustomerId,
            Email = context.Message.Email,
        });
    }
}