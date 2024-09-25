public class UnityObjectState : GOAPStateBase<UnityObjectState, UnityEngine.Object, UnityObjectStateComparer>
{
    public override void ApplyEffect(UnityObjectStateComparer comparer)
    {
        if (comparer.symbol == BoolValue.是)
        {
            this.value = comparer.value;
        }
    }

    public override bool Compre(UnityObjectStateComparer comparer)
    {
        switch (comparer.symbol)
        {
            case BoolValue.是:
                return value == comparer.value;
            case BoolValue.否:
                return value != comparer.value;
        }

        return this.value = comparer.value;
    }

    public override bool EqualsValue(UnityObjectState other)
    {
        return this.value == other.value;
    }
}

public class UnityObjectStateComparer : GOAPStateComparer<UnityObjectState, UnityObjectStateComparer>
{
    public BoolValue symbol;
    public UnityEngine.Object value;
    public override bool EqualsComparer(UnityObjectStateComparer other)
    {
        switch (other.symbol)
        {
            case BoolValue.是:
                return this.value == other.value;
            case BoolValue.否:
                return this.value != other.value;
        }
        return false;
    }
}