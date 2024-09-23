using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GOAPStateBase
{
    public abstract bool EqualsValue(GOAPStateBase other);
    public abstract void SetValue(GOAPStateBase other);
    public abstract GOAPStateBase Copy();
}

public abstract class GOAPStateBase<T, V> : GOAPStateBase where T : GOAPStateBase<T, V>, new()
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
}