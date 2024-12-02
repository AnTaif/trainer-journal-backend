using MediatR;
using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Application.Events.Handlers;

public class BalanceChangedEventHandler(
    IBalanceChangeRepository balanceChangeRepository) : INotificationHandler<BalanceChangedEvent>
{
    public Task Handle(BalanceChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        var newBalanceChange = new BalanceChange(domainEvent.StudentId, domainEvent.Amount, domainEvent.PreviousBalance,
            domainEvent.BalanceChangeReason, domainEvent.Date);

        balanceChangeRepository.Add(newBalanceChange);
        return Task.CompletedTask;
    }
}