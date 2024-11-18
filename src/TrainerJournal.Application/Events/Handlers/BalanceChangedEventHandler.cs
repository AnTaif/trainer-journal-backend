using MediatR;
using TrainerJournal.Application.Services.Finance;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Application.Events.Handlers;

public class BalanceChangedEventHandler(
    IBalanceChangeRepository balanceChangeRepository) : INotificationHandler<BalanceChangedEvent>
{
    public async Task Handle(BalanceChangedEvent domainEvent, CancellationToken cancellationToken)
    {
        var newBalanceChange = new BalanceChange(domainEvent.StudentId, domainEvent.Amount, domainEvent.PreviousBalance,
            domainEvent.BalanceChangeReason, domainEvent.Date);

        balanceChangeRepository.Add(newBalanceChange);
        await balanceChangeRepository.SaveChangesAsync();
    }
}