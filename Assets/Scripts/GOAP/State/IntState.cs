﻿public class IntState : GOAPStateBase<IntState, int, IntStateComparer>
{
    public override void ApplyEffect(IntStateComparer comparer)
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

    public override bool CompreForPrecondition(IntStateComparer comparer)
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

    public override bool CompreForEffect(IntStateComparer comparer)
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
                return false;
            case NumberCompareSymbol.下降即可:
                return false;
            case NumberCompareSymbol.等于:
                return value == comparer.value;
        }
        return false;
    }

    public override bool EqualsValue(IntState other)
    {
        return this.value == other.value;
    }
}

public class IntStateComparer : GOAPStateComparer<IntState, IntStateComparer>
{
    public NumberCompareSymbol symbol;
    public int value;
    public override bool EqualsComparer(IntStateComparer other)
    {
        return symbol == other.symbol;
    }
}