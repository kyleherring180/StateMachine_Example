using MassTransit;
using StateMachine.Contracts.Commands;
using StateMachine.Contracts.Events;
using StateMachine.Model.Entities;

namespace StateMachine.Application.Sagas;

public class OnboardingSaga : MassTransitStateMachine<OnboardingSagaData>
{
    //Define the states of the saga
    public State Welcoming { get; set; }
    public State FollowingUp { get; set; }
    public State Onboarding { get; set; }
    
    //Define the events that trigger state transitions
    public Event<CustomerCreated> CustomerCreated { get; set; }
    public Event<WelcomeEmailSent> WelcomeEmailSent { get; set; }
    public Event<FollowUpEmailSent> FollowUpEmailSent { get; set; }
    
    public OnboardingSaga()
    {
        //Define the initial state
        InstanceState(x => x.CurrentState);
        
        //Define the saga data
        Event(() => CustomerCreated, x => x.CorrelateById(context => context.Message.CustomerId));
        Event(() => WelcomeEmailSent, x => x.CorrelateById(context => context.Message.CustomerId));
        Event(() => FollowUpEmailSent, x => x.CorrelateById(context => context.Message.CustomerId));
        
        //Define the state transitions
        //Initially refers to the state when the saga is first created
        Initially(
            When(CustomerCreated)
                            .Then(context =>
                            {
                                context.Saga.CustomerId = context.Message.CustomerId;
                                context.Saga.Email = context.Message.Email;
                            })
                            .TransitionTo(Welcoming)
                            .Publish(context => new SendWelcomeEmail(context.Message.CustomerId, context.Message.Email)));
        
        //During refers to the state when the saga is in progress
        During(Welcoming,
            When(WelcomeEmailSent)
                            .Then(context => new SendWelcomeEmail(context.Message.CustomerId, context.Message.Email))
                            .TransitionTo(FollowingUp)
                            .Publish(context => new SendFollowUpEmail(context.Message.CustomerId, context.Message.Email)));
        
        During(FollowingUp,
            When(FollowUpEmailSent)
                .Then(context =>
                {
                    context.Saga.FollowUpEmailSent = true;
                    context.Saga.OnboardingCompleted = true;
                })
                .TransitionTo(Onboarding)
                .Publish(context => new OnboardingCompleted
                {
                    CustomerId = context.Message.CustomerId,
                    Email = context.Message.Email,
                })
                .Finalize()); //Transitions the saga to the final state.
    }
}