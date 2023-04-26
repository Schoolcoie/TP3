using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMinigame : MonoBehaviour
{
    //Objects
    [SerializeField] private Fish m_FishScriptableObject;
    [SerializeField] private Rigidbody2D m_FishBody;
    [SerializeField] private Rigidbody2D m_Bobber;
    [SerializeField] private GameObject m_ProgressBar;

    //Minigame Stats
    private List<Fish.Fishes> m_PossibleFish;
    private Fish.Fishes m_CurrentFish;
    private Vector3 m_FishInitialPosition;
    private Vector3 m_BobberInitialPosition;
    private float m_FishCatchProgress = 100;
    private float m_Ceiling;
    private float m_Floor;
    private bool m_IsReeling;

    //Fish Stats
    private Texture2D m_FishIcon;
    private string m_FishName;
    private int m_FishDifficulty;
    private float m_FishMovementInterval;

    private void Awake()
    {
        m_FishInitialPosition = m_Bobber.transform.localPosition;
        m_BobberInitialPosition = m_FishBody.transform.localPosition;
        m_PossibleFish = m_FishScriptableObject.FishList;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {  
        m_CurrentFish = m_PossibleFish[Random.Range(0, m_PossibleFish.Count)];
        Debug.Log($"Current fish is: {m_CurrentFish.ItemDrop.ToString()}");
        m_Bobber.transform.localPosition = m_BobberInitialPosition;
        m_FishBody.transform.localPosition = m_FishInitialPosition;
        m_Ceiling = gameObject.transform.Find("TopEdge").transform.localPosition.y;
        m_Floor = gameObject.transform.Find("BottomEdge").transform.localPosition.y;
        m_FishCatchProgress = 50;
        m_FishDifficulty = m_CurrentFish.Difficulty;
        m_FishMovementInterval = m_CurrentFish.MovementInterval;
        m_IsReeling = false;
        StartCoroutine(FishActionRoutine());
    }

    void Update()
    {
        m_ProgressBar.transform.localScale = new Vector3(m_ProgressBar.transform.localScale.x, (m_FishCatchProgress / 100), m_ProgressBar.transform.localScale.z);

        if (m_FishCatchProgress > 100)
        {
            InventoryManager.GetInstance().AddInventoryItem(m_CurrentFish.ItemDrop);
            AudioManager.GetInstance().PlaySound(AudioManager.SoundEnum.FishCaught);
            EventManager.TriggerEvent("OnMinigameEnd");
        }
        else if (m_FishCatchProgress < 0)
        {
            AudioManager.GetInstance().PlaySound(AudioManager.SoundEnum.FishLost);
            EventManager.TriggerEvent("OnMinigameEnd");
        }

        if (Mathf.Abs(m_Bobber.transform.localPosition.y - m_FishBody.transform.localPosition.y) <= 12) //add variable for bobber range
        {
            if (m_IsReeling == false)
            {
                AudioManager.GetInstance().StopLoopingSound(AudioManager.SoundEnum.FishEscaping);
                AudioManager.GetInstance().PlayLoopingSound(AudioManager.SoundEnum.FishReeling);
            }
            m_FishCatchProgress += 10 * Time.deltaTime; //add variables for progress decay and progress gain
            m_IsReeling = true;
        }
        else
        {
            if (m_IsReeling == true)
            {
                AudioManager.GetInstance().StopLoopingSound(AudioManager.SoundEnum.FishReeling);
                AudioManager.GetInstance().PlayLoopingSound(AudioManager.SoundEnum.FishEscaping);
            }
            m_FishCatchProgress -= 15 * Time.deltaTime;
            m_IsReeling = false;
        }


        if (m_Bobber.transform.localPosition.y > m_Ceiling)
        {
            m_Bobber.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Ceiling, m_Bobber.transform.localPosition.z);
            m_Bobber.velocity = Vector2.zero;
        }
        else if (m_Bobber.transform.localPosition.y < m_Floor)
        {
            m_Bobber.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Floor, m_Bobber.transform.localPosition.z);
            m_Bobber.velocity = Vector2.zero;
        }

        if (m_FishBody.transform.localPosition.y > m_Ceiling)
        {
            m_FishBody.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Ceiling, m_Bobber.transform.localPosition.z);
            m_FishBody.velocity = Vector2.zero;
        }
        else if (m_FishBody.transform.localPosition.y < m_Floor)
        {
            m_FishBody.transform.localPosition = new Vector3(m_Bobber.transform.localPosition.x, m_Floor, m_Bobber.transform.localPosition.z);
            m_FishBody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            m_Bobber.AddForce(Vector2.up * 3.5f); //Add bobber speed variable
        }
    }
    private IEnumerator FishActionRoutine()
    {
        while (m_FishCatchProgress < 100 && m_FishCatchProgress > 0)
        {
            yield return new WaitForSeconds(m_FishMovementInterval);

            if (m_FishBody.transform.localPosition.y >= 50)
            {
                m_FishBody.AddForce(new Vector2(0, Random.Range(m_FishDifficulty / 2, m_FishDifficulty) * -1));
            }
            else if (m_FishBody.transform.localPosition.y <= -50)
            {
                m_FishBody.AddForce(new Vector2(0, Random.Range(m_FishDifficulty / 2, m_FishDifficulty)));
            }
            else
            {
                m_FishBody.AddForce(new Vector2(0, Random.Range(m_FishDifficulty / 2, m_FishDifficulty) * (Random.Range(0, 2) * 2 - 1)));
            }
        }
    }
}
