using MassTransit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StateMachine.Application.Abstraction.Services;
using StateMachine.Application.Sagas;
using StateMachine.Application.Services;
using StateMachine.Contracts.Commands;
using StateMachine.Contracts.Events;
using StateMachine.Data;
using StateMachine.Model.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection - add your services here
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddDbContext<StateMachineDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StateMachine")));

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumers(typeof(Program).Assembly);
    busConfigurator.AddSagaStateMachine<OnboardingSaga, OnboardingSagaData>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<StateMachineDbContext>();
            r.UseSqlServer();
        });
    busConfigurator.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        cfg.UseInMemoryOutbox(context);
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/onboarding", async ([FromBody] string email, DateTimeOffset signUpDate , IBus bus) =>
{
    await bus.Publish(new SignUp(email, signUpDate));
    return Results.Accepted();
});

app.UseHttpsRedirection();


app.Run();
