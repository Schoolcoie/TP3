using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlayerState : PlayerState
{

    public FishingPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        EventManager.StartListening("OnMinigameEnd", StopFishing);
    }

    private void StopFishing()
    {
        AudioManager.GetInstance().StopLoopingSound(AudioManager.SoundEnum.FishEscaping);
        AudioManager.GetInstance().StopLoopingSound(AudioManager.SoundEnum.FishReeling);
        EventManager.StopListening("OnMinigameEnd", StopFishing);
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
