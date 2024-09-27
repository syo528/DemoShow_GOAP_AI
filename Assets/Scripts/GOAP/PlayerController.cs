using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour, IGOAPOwner
{
    public GOAPAgent agent;
    private void Start()
    {
        agent.Init(this);
    }
    private void Update()
    {
        agent.OnUpdate();
    }
}
