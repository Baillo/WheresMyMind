using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;

    public bool creditos;

    [Header("Paineis")]
    public GameObject panelSplash;
    public GameObject panelMenu;
    public GameObject panelJogo;
    public GameObject panelPause;
    public GameObject panelGameOver;
    public GameObject panelCreditos;
    public GameObject panelCutsceneInicio;
    public GameObject panelCutsceneFinal;
    public GameObject imgInteragir;

    [Header("FadeSplash")]
    public float delayFadeIn = 1;
    public float velocidadeFadeIn = 1;

    public static MenuManager GetInstance()
	{
        return instance;
	}

    void Awake()
	{
        instance = this;

        panelSplash.SetActive(true);
	}
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashAbertura());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SplashAbertura()
	{
        float alfa = 0;
        yield return new WaitForSeconds(delayFadeIn);

        while(alfa < 1)
		{
            panelSplash.GetComponent<Image>().color = new Color(0, 0, 0, 1 - alfa);
            alfa += Time.deltaTime * velocidadeFadeIn;
            yield return new WaitForEndOfFrame();
		}
        panelSplash.SetActive(false);
	}

    public void Creditos()
	{
		if (GameManager.GetInstance().Finalizado())
		{
            GameManager.GetInstance().VoltarMenu();
		}
		else
		{
            creditos = !creditos;
            panelCreditos.SetActive(creditos);
        }
	}

    public void Quit()
	{
        Application.Quit();
	}
}
