namespace SharedKernel.Base;

public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    // add Domain Events, Auditing, Notification, Outbox، Logging , ... to All AggregateRoots
}