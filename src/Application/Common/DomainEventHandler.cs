using MediatR;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Common;

public abstract class DomainEventHandler<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>>
    where TDomainEvent : DomainEvent
{
    public abstract Task Handle(DomainEventNotification<TDomainEvent> notification,
        CancellationToken cancellationToken);
}