using Sirenix.OdinInspector;
using System.Collections.Generic;

[HideLabel]
public struct GOAPStateType
{
    [HideLabel, ValueDropdown("GetAllState")] public string name;
    public static implicit operator GOAPStateType(string state)
    {
        return new GOAPStateType { name = state };
    }
    public static implicit operator string(GOAPStateType state)
    {
        return state.name;
    }
    #region Editor
#if UNITY_EDITOR
    private List<string> GetAllState()
    {
        return new List<string> { "AB", "CD" };
    }
#endif
    #endregion

}
