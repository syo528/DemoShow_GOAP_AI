public class BoolState : GOAPStateBase<BoolState, bool>
{
    public override bool EqualsValue(BoolState other)
    {
        return this.value == other.value;
    }
}