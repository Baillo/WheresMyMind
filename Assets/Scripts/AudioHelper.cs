using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    private static AudioHelper instance;

    [Header("Soundscapes")]
    public AudioSource scSource;
    public AudioClip scForest1;
    public AudioClip scForest2;
    public AudioClip scForest3;
    public AudioClip scCabana;
    public AudioClip ultimoSoundscape;

    [Header("Personagem")]
    public AudioSource plSource1;
    public AudioSource plSource2;
    public AudioClip plPassos;
    public AudioClip plBreathing1;
    public AudioClip plBreathing2;
    public AudioClip plBreathing3;
    public AudioClip plChoro1;
    public AudioClip plChoro2;
    public AudioClip plSusto1;
    public AudioClip plSusto2;

    [Header("Fogueira")]
    public AudioSource fogoSource;
    public AudioClip fogoLonge;
    public AudioClip fogoPerto;

    [Header("FalasPersonagem")]
    public AudioSource falasSource;

    public static AudioHelper GetInstance()
	{
        return instance;
	}

	private void Awake()
	{
        instance = this;
	}

    public void PlayAudio(AudioSource source, AudioClip clip)
	{
        ultimoSoundscape = scSource.clip;
        source.clip = clip;
        source.Play();
	}

    public void StopAudio(AudioSource source)
	{
        source.Stop();
	}

}
