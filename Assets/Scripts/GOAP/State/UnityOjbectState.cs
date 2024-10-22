public class UnityObjectState : GOAPStateBase<UnityObjectState, UnityEngine.Object, UnityObjectStateComparer>
{
    public override void ApplyEffect(UnityObjectStateComparer comparer)
    {
        switch (comparer.symbol)
        {
            case UnityObjectSymbol.是:
                this.value = comparer.value;
                break;
            case UnityObjectSymbol.为空:
                this.value = null;
                break;
        }
    }

    public override bool CompreForPrecondition(UnityObjectStateComparer comparer)
    {
        switch (comparer.symbol)
        {
            case UnityObjectSymbol.是:
                return this.value == comparer.value;
            case UnityObjectSymbol.否:
                return this.value != comparer.value;
            case UnityObjectSymbol.为空:
                return this.value == null;
            case UnityObjectSymbol.不为空:
                return this.value != null;
        }
        return this.value = comparer.value;
    }

    public override bool CompreForEffect(UnityObjectStateComparer comparer)
    {
        return CompreForPrecondition(comparer);
    }

    public override bool EqualsValue(UnityObjectState other)
    {
        return this.value == other.value;
    }
}

public class UnityObjectStateComparer : GOAPStateComparer<UnityObjectState, UnityObjectStateComparer>
{
    public UnityObjectSymbol symbol;
    public UnityEngine.Object value;
    public override bool EqualsComparer(UnityObjectStateComparer other)
    {
        if (other.symbol != symbol) return false;

        switch (symbol)
        {
            case UnityObjectSymbol.是:
            case UnityObjectSymbol.否:
                return this.value == other.value;
            case UnityObjectSymbol.不为空:
                break;
        }
        return true;
    }
}

public enum UnityObjectSymbol
{
    是,
    否,
    为空,
    不为空,
}