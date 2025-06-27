using Microsoft.Extensions.Logging;
using StateMachine.Application.Abstraction.Services;

namespace StateMachine.Application.Services;

public class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public Task SendWelcomeEmailAsync(string email)
    {
        // Simulate sending an email
        //Will propbably dispatch message to Post office or similar service
        logger.LogInformation($"Send welcome email to {email}.");
        
        return Task.CompletedTask;
    }

    public Task SendFollowUpEmailAsync(string email)
    {
        logger.LogInformation($"Send Follow up email to {email}.");
        
        return Task.CompletedTask;
    }
}