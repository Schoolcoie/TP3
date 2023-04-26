using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    private List<AudioSource> AudioSourcePool = new List<AudioSource>();
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            AudioSource Source = new GameObject("AudioSource").AddComponent<AudioSource>();
            Source.transform.parent = this.transform;
            AudioSourcePool.Add(Source);
        }
    }

    public AudioSource GetValidAudioSource()
    {
        for (int i = 0; i < AudioSourcePool.Count; i++)
        {
            if (!AudioSourcePool[i].isPlaying)
            {
                return AudioSourcePool[i];
            }
        }

        AudioSource source = new GameObject("AudioSource").AddComponent<AudioSource>();
        source.transform.parent = this.transform;
        AudioSourcePool.Add(source);
        return source;
    }
}
