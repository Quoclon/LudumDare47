using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //AudioSource audioSource;
    AudioSource[] audioSources;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSources = Resources.FindObjectsOfTypeAll<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playAudio(string audioSourceName)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            //Debug.Log("AudioSource in Array of Audiosources: " + audioSource);

            if (audioSource.name == audioSourceName)
            {
                //Debug.Log(audioSourceName);
                audioSource.Play();
            }
        }
    }
}
