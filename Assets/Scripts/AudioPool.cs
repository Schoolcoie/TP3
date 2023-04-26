using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    private List<AudioSource> m_AudioSourcePool = new List<AudioSource>();
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            AudioSource Source = new GameObject("AudioSource").AddComponent<AudioSource>();
            Source.transform.parent = this.transform;
            m_AudioSourcePool.Add(Source);
        }
    }

    public AudioSource GetValidAudioSource()
    {
        for (int i = 0; i < m_AudioSourcePool.Count; i++)
        {
            if (!m_AudioSourcePool[i].isPlaying)
            {
                return m_AudioSourcePool[i];
            }
        }

        AudioSource source = new GameObject("AudioSource").AddComponent<AudioSource>();
        source.transform.parent = this.transform;
        m_AudioSourcePool.Add(source);
        return source;
    }
}
