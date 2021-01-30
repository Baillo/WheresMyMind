using System.Collections;
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
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

    }

    // Update is called once per frame
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

		if (pauseToggle)
		{
            MenuManager.GetInstance().panelJogo.SetActive(false);
            MenuManager.GetInstance().panelPause.SetActive(true);
            Time.timeScale = 0;
		}
		else
		{
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
	}

    IEnumerator TimerCutsceneInicio()
	{
        yield return new WaitForSeconds(5);
        PularCutsceneInicio();
	}

    public void PularCutsceneInicio()
	{
		if (MenuManager.GetInstance().panelCutsceneInicio.activeInHierarchy)
		{
            inicializado = true;
            MenuManager.GetInstance().panelCutsceneInicio.SetActive(false);
            MenuManager.GetInstance().panelJogo.SetActive(true);
            PlayerController.GetInstance().SetControlavel(true);

            //iniciar audios do gameplay
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scForest1);
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().fogoSource, AudioHelper.GetInstance().fogoPerto);
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().plSource2, AudioHelper.GetInstance().plBreathing2);
        }
    }

    public void FimDeJogo()
	{
        AudioHelper.GetInstance().StopAudio(AudioHelper.GetInstance().scSource);
        PlayerController.GetInstance().SetControlavel(false);
        finalizado = true;
        MenuManager.GetInstance().panelJogo.SetActive(false);
        MenuManager.GetInstance().panelCutsceneFinal.SetActive(true);
        StartCoroutine(TimerCutsceneFinal());
	}

    IEnumerator TimerCutsceneFinal()
	{
        yield return new WaitForSeconds(5);
        PularCutsceneFinal();
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
