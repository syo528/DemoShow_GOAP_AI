using System;

public abstract class GOAPStateBase
{
    public abstract bool EqualsValue(GOAPStateBase other);
    public abstract void SetValue(GOAPStateBase other);
    public abstract GOAPStateBase Copy();
    public abstract GOAPStateComparer GetComparer();
    public abstract Type GetGetComparerType();
    public abstract bool CompreForPrecondition(GOAPStateComparer comparer);
    public abstract bool CompreForEffect(GOAPStateComparer comparer);
    public abstract void ApplyEffect(GOAPStateComparer comparer);
}

public abstract class GOAPStateBase<T, V, C> : GOAPStateBase where T : GOAPStateBase<T, V, C>, new() where C : GOAPStateComparer, new()
{
    public V value;
    public abstract bool EqualsValue(T other);
    public override bool EqualsValue(GOAPStateBase other)
    {
        return EqualsValue((T)other);
    }
    public virtual void SetValue(T other)
    {
        this.value = other.value;
    }
    public override void SetValue(GOAPStateBase other)
    {
        SetValue((T)other);
    }
    public virtual void SetValue(V value)
    {
        this.value = value;
    }
    public override GOAPStateBase Copy()
    {
        return new T() { value = value };
    }

    public virtual C GetStateComparer()
    {
        return new C();
    }
    public override GOAPStateComparer GetComparer()
    {
        return GetStateComparer();
    }

    public abstract bool CompreForPrecondition(C comparer);
    public override bool CompreForPrecondition(GOAPStateComparer comparer)
    {
        return CompreForPrecondition((C)comparer);
    }
    public abstract bool CompreForEffect(C comparer);
    public override bool CompreForEffect(GOAPStateComparer comparer)
    {
        return CompreForEffect((C)comparer);
    }

    public abstract void ApplyEffect(C comparer);
    public override void ApplyEffect(GOAPStateComparer comparer)
    {
        ApplyEffect((C)comparer);
    }
    public override Type GetGetComparerType()
    {
        return typeof(C);
    }
}