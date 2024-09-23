public class UnityObjectState : GOAPStateBase<UnityObjectState, UnityEngine.Object>
{
    public override bool EqualsValue(UnityObjectState other)
    {
        return this.value == other.value;
    }
}