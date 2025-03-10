using System;

/// <summary>
/// 高度抽象的基类
/// </summary>
public abstract class GOAPStateBase
{
    //比较当前状态和另一个GOAPStateBase状态是否相等
    public abstract bool EqualsValue(GOAPStateBase other);
    //将另一个状态的数值设置到当前状态中
    public abstract void SetValue(GOAPStateBase other);
    //创建并且返回一个当前状态的副本
    public abstract GOAPStateBase Copy();
    //获取比较器的对象
    public abstract GOAPStateComparer GetComparer();
    //获取比较器类型
    public abstract Type GetGetComparerType();
    //使用给定的比较器判断当前状态是否满足某个前置条件。
    public abstract bool CompreForPrecondition(GOAPStateComparer comparer);
    //使用给定的比较器判断当前状态是否满足某个效果。
    public abstract bool CompreForEffect(GOAPStateComparer comparer);
    //使用给定的比较器应用效果。
    public abstract void ApplyEffect(GOAPStateComparer comparer);
}

/// <summary>
/// 泛型版本的GOAPStateBase
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="V">代表一个具体的数值</typeparam>
/// <typeparam name="C"></typeparam>
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