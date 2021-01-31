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
    public AudioClip falasInicio;
    public AudioClip falasPrimeiroItem;
    public AudioClip falasSegundoItem;
    public AudioClip falasTerceiroItem;
    public AudioClip falasCabana;
    public AudioClip falasChuva;
    public AudioClip falasChoro;
    public AudioClip falasAmbulancia;
    public AudioClip falasFogueira;
    public AudioClip falasAleatorio;

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
    public AudioClip sfxChuva;
    public AudioClip sfxChoroBebe;

    [Header("Cutscene")]
    public AudioClip cutChuvaAbafada;
    public AudioClip cutChuvaVento;


    public AudioSource[] allSources;

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

    public void PlayFala(AudioSource source, AudioClip clip, int text)
	{
        MenuManager.GetInstance().legenda.text = MenuManager.GetInstance().textFalas[text];
        source.clip = clip;
        source.Play();
        StartCoroutine(TempoLegenda(source));
	}

    IEnumerator TempoLegenda(AudioSource source)
	{
		while (source.isPlaying)
		{
            MenuManager.GetInstance().legenda.gameObject.SetActive(true);
            yield return null;
        }

        MenuManager.GetInstance().legenda.gameObject.SetActive(false);
    }

    public void PlaySusto(int delay)
	{
        StartCoroutine(PlaySustoCoroutine(delay));
	}

    public IEnumerator PlaySustoCoroutine(int delay)
	{

        yield return new WaitForSeconds(delay);
        int random = Random.Range(0, 10);

        if(random > 3 && random <= 5)
		{
            PlayAudio(plSource2, plSusto1);
		}
        else if(random > 6 && random <= 8)
		{
            PlayAudio(plSource2, plSusto2);
        }
		else
		{
            PlayFala(falasSource, falasAleatorio, 8);
		}
	}

    public void PauseAll()
	{
		for (int i = 0; i < allSources.Length; i++)
		{
            allSources[i].Pause();
		}
	}
    public void UnpauseAll()
	{
        for (int i = 0; i < allSources.Length; i++)
        {
            allSources[i].UnPause();
        }
    }

}
