﻿namespace SharedKernel.Base;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; } = default!;

    public override bool Equals(object obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override int GetHashCode() => Id?.GetHashCode() ?? 0;
}