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
    public AudioClip plBreathing1;
    public AudioClip plBreathing2;
    public AudioClip plBreathing3;
    public AudioClip plChoro1;
    public AudioClip plChoro2;
    public AudioClip plSusto1;
    public AudioClip plSusto2;
    public AudioClip plPassosFloresta;
    public AudioClip plPassosMadeira;
    public AudioClip plHeartbeat;
    public AudioClip plCorrendo;

    [Header("Fogueira")]
    public AudioSource fogoSource;
    public AudioClip fogoLonge;
    public AudioClip fogoPerto;

    [Header("FalasPersonagem")]
    public AudioSource falasSource;

    [Header("Itens")]
    public AudioSource itemSource;
    public AudioClip itemChamado;
    public AudioClip itemColetado;
    public AudioClip doorOpen;
    public AudioClip gateOpen;

    [Header("SFX")]
    public AudioSource sfxSource;
    public AudioSource[] sfxsSources;
    public AudioClip[] sfxs;
    public AudioClip sfxAmbulancia1;
    public AudioClip sfxAmbulancia2;
    public AudioClip sfxCorujas;
    public AudioClip sfxFolhas;
    public AudioClip sfxMato1;
    public AudioClip sfxMato2;
    public AudioClip sfxTrovao;

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

    public void PlayRandomAudio()
	{
        int randomSfx = Random.Range(0, sfxs.Length);
        int randomSource = Random.Range(0, sfxsSources.Length);
		if (!sfxsSources[randomSource].isPlaying)
		{
            PlayAudio(sfxsSources[randomSource], sfxs[randomSfx]);
        }
    }

}
