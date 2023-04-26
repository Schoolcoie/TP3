using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForagingPlayerState : PlayerState
{
    private List<float> m_Clicks = new List<float>();
    private float m_Elapsed = 0;
    private bool m_IsChopping = false;

    public ForagingPlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        EventManager.StartListening("OnMinigameEnd", StopForaging);
    }

    private void StopForaging()
    {
        AudioManager.GetInstance().StopLoopingSound(AudioManager.SoundEnum.TreeChopping);
        m_StateMachine.ChangeState(new RoamingPlayerState(m_StateMachine));
    }

    public override void ExecuteUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Clicks.Add(m_Elapsed);
            EventManager.TriggerEvent("OnForageClick");
        }
    }

    public override void ExecuteFixedUpdate()
    {
        m_Elapsed += Time.deltaTime;

        if (m_Clicks.Count > 0 && m_Elapsed - m_Clicks[m_Clicks.Count - 1] > 0.3f)
        {
            if (m_IsChopping == true)
            {
                AudioManager.GetInstance().StopLoopingSound(AudioManager.SoundEnum.TreeChopping);
                m_IsChopping = false;
            }
        }
        else
        {
            if (m_Clicks.Count > 0 && m_IsChopping == false)
            {
                AudioManager.GetInstance().PlayLoopingSound(AudioManager.SoundEnum.TreeChopping);
                m_IsChopping = true;
            }
        }

        if (m_Clicks.Count >= 60)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundEnum.TreeFalling);
            InventoryManager.GetInstance().AddInventoryItem(InventoryManager.Items.Wood);
            EventManager.TriggerEvent("OnMinigameEnd");
        }
        else if (m_Clicks.Count > 0 && m_Elapsed - m_Clicks[0] > 10)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundEnum.AxeBreaking);
            EventManager.TriggerEvent("OnMinigameEnd");
        }
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