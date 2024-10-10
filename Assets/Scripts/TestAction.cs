using UnityEngine;

public class TestAction : GOAPActionBase
{
    public float timer;
    public override void OnStart()
    {
        timer = 0;
    }

    public override GOAPRunState OnUpdate()
    {
        timer += Time.deltaTime;
        if (timer > 1)
        {
            ApplyEffect();
            return GOAPRunState.Succeed;
        }
        else
        {
            return GOAPRunState.Runing;
        }
    }

}
