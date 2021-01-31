﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool pauseToggle;
    private bool inicializado;
    private bool finalizado;
    public Itens[] itensColetados;
    public int qtdItens;
    public UnityEvent AoIniciar;
    public GameObject luz;

    public static GameManager GetInstance()
	{
        return instance;
	}

    public bool Inicializado()
	{
        return inicializado;
	}

    public bool Finalizado()
	{
        return finalizado;
	}

    void Awake()
	{
        instance = this;
        Application.targetFrameRate = 60;
	}
    
    void Start()
    {
        Time.timeScale = 1;

    }
    void Update()
    {
		if (inicializado && !finalizado)
		{
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseToggle();
            }
        }
    }

    public void PauseToggle()
	{
        pauseToggle = !pauseToggle;

		if (pauseToggle && !MenuManager.GetInstance().outraTelaAberta)
		{
            AudioHelper.GetInstance().PauseAll();
            MenuManager.GetInstance().panelJogo.SetActive(false);
            MenuManager.GetInstance().panelPause.SetActive(true);
            Time.timeScale = 0;
		}
		else if(!pauseToggle && !MenuManager.GetInstance().outraTelaAberta)
		{
            AudioHelper.GetInstance().UnpauseAll();
            MenuManager.GetInstance().panelPause.SetActive(false);
            MenuManager.GetInstance().panelJogo.SetActive(true);
            Time.timeScale = 1;
        }
	}

    public void Iniciar()
	{
        AoIniciar.Invoke();
        PlayerController.GetInstance().SetControlavel(false);
        MenuManager.GetInstance().panelMenu.SetActive(false);
        MenuManager.GetInstance().panelCutsceneInicio.SetActive(true);
        StartCoroutine(TimerCutsceneInicio());
        luz.GetComponent<Light>().intensity = 0.2f;
	}

    IEnumerator TimerCutsceneInicio()
	{
        yield return new WaitForSeconds(8);
        PularCutsceneInicio();
	}

    public void PularCutsceneInicio()
	{
		if (MenuManager.GetInstance().panelCutsceneInicio.activeInHierarchy)
		{
            /*inicializado = true;
            MenuManager.GetInstance().panelCutsceneInicio.SetActive(false);
            MenuManager.GetInstance().panelJogo.SetActive(true);
            PlayerController.GetInstance().SetControlavel(true);

            //iniciar audios do gameplay
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scForest1);
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().fogoSource, AudioHelper.GetInstance().fogoPerto);
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().plSource2, AudioHelper.GetInstance().plBreathing2);*/
            MenuManager.GetInstance().panelCutsceneInicio.SetActive(false);
            MenuManager.GetInstance().panelControles.SetActive(true);
        }
    }

    public void FecharPanelControles()
	{
        inicializado = true;
        MenuManager.GetInstance().panelCutsceneInicio.SetActive(false);
        MenuManager.GetInstance().panelJogo.SetActive(true);
        PlayerController.GetInstance().SetControlavel(true);

        //iniciar audios do gameplay
        AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scForest1);
        AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().fogoSource, AudioHelper.GetInstance().fogoPerto);
        AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().plSource2, AudioHelper.GetInstance().plBreathing2);
        AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasInicio, 0);

        MenuManager.GetInstance().panelControles.SetActive(false);
    }

    public void FimDeJogo()
	{
        AudioHelper.GetInstance().PauseAll();
        AudioHelper.GetInstance().StopAudio(AudioHelper.GetInstance().scSource);
        PlayerController.GetInstance().SetControlavel(false);
        finalizado = true;
        MenuManager.GetInstance().panelJogo.SetActive(false);
        MenuManager.GetInstance().panelCutsceneFinal.SetActive(true);
        StartCoroutine(TimerCutsceneFinal());
	}

    IEnumerator TimerCutsceneFinal()
	{
        MenuManager.GetInstance().CreditosFinais();
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PularCutsceneFinal()
	{
		if (MenuManager.GetInstance().panelCutsceneFinal.activeInHierarchy)
		{
            MenuManager.GetInstance().panelCutsceneFinal.SetActive(false);
            MenuManager.GetInstance().panelCreditos.SetActive(true);
		}
	}

    public void VoltarMenu()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
