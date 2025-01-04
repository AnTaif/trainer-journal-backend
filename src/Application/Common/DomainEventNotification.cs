using MediatR;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Common;

public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : INotification
    where TDomainEvent : DomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}