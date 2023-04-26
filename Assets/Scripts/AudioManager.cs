using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;
    public AudioPool m_Pool;

    [System.Serializable]
    private struct SoundStruct
    {
        public SoundEnum Name;
        public AudioClip Clip;
    }

    [SerializeField] private List<SoundStruct> SoundAssociations;

    public enum SoundEnum
    {
        EndGame,
        FishReeling,
        FishEscaping,
        FishCaught,
        FishLost,
        TreeChopping,
        TreeFalling,
        AxeBreaking,
        BGM,
        BossMusic
    }

    void Start()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }

        PlayLoopingSound(SoundEnum.BGM);
    }

    public static AudioManager GetInstance()
    {
        return m_Instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(SoundEnum sound)
    {
        AudioSource source = m_Pool.GetValidAudioSource();

        for (int i = 0; i < SoundAssociations.Count; i++)
        {
            if (SoundAssociations[i].Name == sound)
            {
                source.clip = SoundAssociations[i].Clip;
            }
        }
        source.gameObject.transform.position = FindObjectOfType<PlayerStateMachine>().transform.position;
        source.Play();
    }

    public void PlayLoopingSound(SoundEnum sound)
    {
        AudioSource source = m_Pool.GetValidAudioSource();

        for (int i = 0; i < SoundAssociations.Count; i++)
        {
            if (SoundAssociations[i].Name == sound)
            {
                source.clip = SoundAssociations[i].Clip;
                source.loop = true;
            }
        }
        source.gameObject.transform.position = FindObjectOfType<PlayerStateMachine>().transform.position;
        source.Play();
    }

    public void StopLoopingSound(SoundEnum sound)
    {
        for (int i = 0; i < m_Pool.transform.childCount; i++)
        {
            AudioSource source = m_Pool.transform.GetChild(i).GetComponent<AudioSource>();
            if (source.isPlaying)
            {
                for (int y = 0; y < SoundAssociations.Count; y++)
                {
                    if (SoundAssociations[y].Clip == source.clip && SoundAssociations[y].Name == sound)
                    {
                        source.Stop();
                        source.loop = false;
                    }
                }
            }
        }
    }

    public bool IsSoundAlreadyPlaying(SoundEnum sound)
    {
        for (int i = 0; i < m_Pool.transform.childCount; i++)
        {
            AudioSource source = m_Pool.transform.GetChild(i).GetComponent<AudioSource>();
            if (source.isPlaying)
            {
                for (int y = 0; y < SoundAssociations.Count; y++)
                {
                    if (SoundAssociations[y].Clip == source.clip && SoundAssociations[y].Name == sound)
                    {
                        return true;
                    }
                }
                
            }
        }
        return false;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (FindObjectOfType<AudioPool>() != null)
            m_Pool = FindObjectOfType<AudioPool>();
    }
}
