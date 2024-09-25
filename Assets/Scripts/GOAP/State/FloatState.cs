public class FloatState : GOAPStateBase<FloatState, float, FloastStateComparer>
{
    public override void ApplyEffect(FloastStateComparer comparer)
    {
        switch (comparer.symbol)
        {
            case NumberCompareSymbol.等于:
                value = comparer.value;
                break;
            default:
                value += comparer.value;
                break;
        }
    }

    public override bool Compre(FloastStateComparer comparer)
    {
        switch (comparer.symbol)
        {
            case NumberCompareSymbol.大于:
                return value > comparer.value;
            case NumberCompareSymbol.小于:
                return value < comparer.value;
            case NumberCompareSymbol.大于等于:
                return value >= comparer.value;
            case NumberCompareSymbol.小于等于:
                return value <= comparer.value;
            case NumberCompareSymbol.提升即可:
                return value > 0;
            case NumberCompareSymbol.下降即可:
                return value < 0;
            case NumberCompareSymbol.等于:
                return value == comparer.value;
        }
        return false;
    }

    public override bool EqualsValue(FloatState other)
    {
        return this.value == other.value;
    }

}

public class FloastStateComparer : GOAPStateComparer<FloatState, FloastStateComparer>
{
    public NumberCompareSymbol symbol;
    public float value;
    public override bool EqualsComparer(FloastStateComparer other)
    {
        return symbol == other.symbol;
    }
}

public enum NumberCompareSymbol
{
    大于,
    小于,
    大于等于,
    小于等于,
    提升即可,
    下降即可,
    等于
}