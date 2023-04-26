using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState m_CurrentState;
    private Vector3 m_SpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_SpawnPosition = transform.position;
        m_CurrentState = new RoamingPlayerState(this);
        EventManager.StartListening("OnMinigameEnd", EndMinigame);
        EventManager.StartListening("OnLoadGame", ResetPosition);
        EventManager.StartListening("Reset", ResetPosition);
    }

    public void ChangeState(PlayerState newState)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        m_CurrentState = newState;
    }

    private void EndMinigame()
    {
        ChangeState(new RoamingPlayerState(this));
    }

    private void ResetPosition()
    {
        transform.position = m_SpawnPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_CurrentState is PausedPlayerState)
            {
                EventManager.TriggerEvent("Unpause");
            }
            else if (m_CurrentState is RoamingPlayerState)
            {
                EventManager.TriggerEvent("Pause");
            }
        }

        m_CurrentState.ExecuteUpdate();
    }

    private void FixedUpdate()
    {
        m_CurrentState.ExecuteFixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_CurrentState.ExecuteOnCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        m_CurrentState.ExecuteOnCollisionExit(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_CurrentState.ExecuteOnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        m_CurrentState.ExecuteOnTriggerExit(other);
    }

    private void OnDestroy()
    {
        EventManager.StopListening("OnMinigameEnd", EndMinigame);
        EventManager.StopListening("OnLoadGame", ResetPosition);
        EventManager.StopListening("Reset", ResetPosition);
    }
}



