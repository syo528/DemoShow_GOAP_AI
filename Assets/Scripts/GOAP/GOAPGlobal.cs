using Sirenix.OdinInspector;
using UnityEngine;
public class GOAPGlobal : SerializedMonoBehaviour
{
    public static GOAPGlobal instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GOAPStates globalStates;
    public GOAPStates GlobalStates => globalStates;

    public bool TryGetGlobalState(string targetState, out GOAPStateBase state)
    {
        state = default;
        if (globalStates == null || globalStates.stateDic == null) return false;
        return globalStates.TryGetState(targetState, out state);
    }
}
