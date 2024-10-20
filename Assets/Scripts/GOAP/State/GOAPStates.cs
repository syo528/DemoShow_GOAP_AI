using System.Collections.Generic;

public class GOAPStates
{
    public Dictionary<string, GOAPStateBase> stateDic = new Dictionary<string, GOAPStateBase>();
    public bool TryAddState(GOAPStateType type, GOAPStateBase state)
    {
        return stateDic.TryAdd(type, state);
    }

    public bool TryRemove(GOAPStateType type)
    {
        return stateDic.Remove(type);
    }

    public T GetState<T>(GOAPStateType type) where T : GOAPStateBase
    {
        return (T)stateDic[type];
    }

    public bool TryGetState(GOAPStateType type, out GOAPStateBase state)
    {
        state = default;
        if (stateDic == null || type.name == null) return false;
        return stateDic.TryGetValue(type, out state);
    }

    public bool TryGetState<T>(GOAPStateType type, out T state) where T : GOAPStateBase
    {
        state = default;
        if (stateDic == null) return false;

        if (stateDic.TryGetValue(type, out GOAPStateBase tempState))
        {
            state = (T)tempState;
            return true;
        }
        else return false;
    }
    public bool CheckStateForPrecondition(GOAPStateType type, GOAPStateComparer comparer)
    {
        if (TryGetState(type, out GOAPStateBase state))
        {
            return state.CompreForPrecondition(comparer);
        }
        return false;
    }
    public bool CheckStateForEffect(GOAPStateType type, GOAPStateComparer comparer)
    {
        if (TryGetState(type, out GOAPStateBase state))
        {
            return state.CompreForEffect(comparer);
        }
        return false;
    }
    public void ApplyEffect(GOAPTypeAndComparer effect)
    {
        if (stateDic.TryGetValue(effect.stateType, out GOAPStateBase value))
        {
            value.ApplyEffect(effect.stateComparer);
        }
    }
}
