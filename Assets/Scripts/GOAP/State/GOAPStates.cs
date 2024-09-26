using Sirenix.OdinInspector;
using System.Collections.Generic;

public class GOAPStates
{
    public Dictionary<GOAPStateType, GOAPStateBase> stateDic = new Dictionary<GOAPStateType, GOAPStateBase>();
    public bool TryAddState(GOAPStateType type, GOAPStateBase state)
    {
        return stateDic.TryAdd(type, state);
    }

    public bool TryRemove(GOAPStateType type)
    {
        return stateDic.Remove(type);
    }

    public T GetStateBase<T>(GOAPStateType type) where T : GOAPStateBase
    {
        return (T)stateDic[type];
    }
    public bool TryGetState<T>(GOAPStateType type, out T state) where T : GOAPStateBase
    {
        if (stateDic.TryGetValue(type, out GOAPStateBase tempState))
        {
            state = (T)tempState;
        }
        state = default;
        return false;
    }
    public bool CheckState(GOAPStateType type, GOAPStateComparer comparer)
    {
        if (TryGetState(type, out GOAPStateBase state))
        {
            return state.Compre(comparer);
        }
        return false;
    }

#if UNITY_EDITOR
    [Button]
    private void CheckStates()
    {
        List<GOAPStateType> createTypeList = new List<GOAPStateType>();
        foreach (KeyValuePair<GOAPStateType, GOAPStateBase> item in stateDic)
        {
            // 类型错误
            if (item.Value == null || item.Value.GetType() != GOAPGlobalConfig.GetStateValueType(item.Key))
            {
                createTypeList.Add(item.Key);
            }
        }
        foreach (GOAPStateType item in createTypeList)
        {
            stateDic[item] = GOAPGlobalConfig.CopyState(item);
        }
    }
#endif
}
