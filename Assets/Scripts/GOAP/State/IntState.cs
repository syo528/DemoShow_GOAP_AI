public class IntState : GOAPStateBase<IntState, int>
{
    public override bool EqualsValue(IntState other)
    {
        return this.value == other.value;
    }
}