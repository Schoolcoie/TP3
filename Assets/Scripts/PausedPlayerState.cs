using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPlayerState : PlayerState
{

    public PausedPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        EventManager.StartListening("Unpause", EndPause);
    }

    private void EndPause()
    {
        m_StateMachine.ChangeState(new RoamingPlayerState(m_StateMachine));
    }

    public override void ExecuteUpdate()
    {
    }

    public override void ExecuteFixedUpdate()
    {
    }

    public override void ExecuteOnCollisionEnter(Collision collision)
    {
    }
    public override void ExecuteOnCollisionExit(Collision collision)
    {
    }

    public override void ExecuteOnTriggerEnter(Collider other)
    {
    }

    public override void ExecuteOnTriggerExit(Collider other)
    {
    }

}
