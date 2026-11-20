using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background1;
    public AudioClip background2;
    public AudioClip background3;
    public AudioClip background4;

    private AudioClip[] backgroundMusicSelection = new AudioClip[4];

    public AudioClip attack;

    
    void Awake()
    {
        // If not set in the Inspector, create the sources dynamically
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true; // music usually loops
        }

        if (SFXSource == null)
        {
            SFXSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {

        backgroundMusicSelection[0] = (background1);
        backgroundMusicSelection[1] = (background2);
        backgroundMusicSelection[2] = (background3);
        backgroundMusicSelection[3] = (background4);

        musicSource.clip = backgroundMusicSelection[Random.Range(0,4)];
        musicSource.Play();

    }

    void Update()
    {

        if(!musicSource.isPlaying && musicSource.time > 0) {

            musicSource.clip = backgroundMusicSelection[Random.Range(0,4)];
            musicSource.Play();
        }

    }

    public void PlaySFX(AudioClip clip) {

        SFXSource.PlayOneShot(clip);

    }
}