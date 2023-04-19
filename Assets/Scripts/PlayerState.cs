using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine m_StateMachine;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        m_StateMachine = stateMachine;
    }

    public abstract void ExecuteUpdate();
    public abstract void ExecuteOnCollisionEnter(Collision collision);
    public abstract void ExecuteOnTriggerEnter(Collider other);
}
