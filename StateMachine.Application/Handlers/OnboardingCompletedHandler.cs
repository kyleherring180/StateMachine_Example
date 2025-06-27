using MassTransit;
using Microsoft.Extensions.Logging;
using StateMachine.Contracts.Commands;
using StateMachine.Contracts.Events;

namespace StateMachine.Application.Handlers;

public class OnboardingCompletedHandler(ILogger<OnboardingCompletedHandler> logger) : IConsumer<OnboardingCompleted>
{
    public Task Consume(ConsumeContext<OnboardingCompleted> context)
    {
        logger.LogInformation("Onboarding completed");
        
        return Task.CompletedTask;
    }
}