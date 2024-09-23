public class FloatState : GOAPStateBase<FloatState, float>
{
    public override bool EqualsValue(FloatState other)
    {
        return this.value == other.value;
    }
}