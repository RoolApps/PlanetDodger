using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongsPlayer : MonoBehaviour {

    public AudioClip[] Clips;

    private static SongsPlayer instance = null;
    private AudioSource source = null;
    private System.Random random = null;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            source = GetComponent<AudioSource>();
            random = new System.Random();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void LateUpdate()
    {
        if(!source.isPlaying)
        {
            source.clip = Clips[random.Next(Clips.Length)];
            source.Play();
        }
    }
}
