
/// <summary>
/// 比较器的普通版本 
/// 作用场景1.作为行为的前提 2.作为目标的倾向
/// </summary>
public abstract class GOAPStateComparer
{
    //和其他比较器作比较
    public abstract bool EqualsComparer(GOAPStateComparer other);
}

public abstract class GOAPStateComparer<S, C> : GOAPStateComparer where S : GOAPStateBase where C : GOAPStateComparer
{
    public abstract bool EqualsComparer(C other);
    public override bool EqualsComparer(GOAPStateComparer other)
    {
        return EqualsComparer((C)other);
    }
}
