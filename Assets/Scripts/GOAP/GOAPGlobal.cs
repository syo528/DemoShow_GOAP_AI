using Sirenix.OdinInspector;
using UnityEngine;
public class GOAPGlobal : SerializedMonoBehaviour
{
    public static GOAPGlobal instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private GOAPGlobalConfig config;
    public GOAPGlobalConfig Config => config;

    [SerializeField] private GOAPStates globalStates;
    public GOAPStates GlobalStates => globalStates;
}
