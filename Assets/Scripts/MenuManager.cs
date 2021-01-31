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
    public GameObject panelItem;
    public GameObject panelControles;
    public GameObject imgControles1;
    public GameObject imgControles2;
    public GameObject imgInteragir;
    public string[] textFalas;
    public Text legenda;
    public GameObject iconLanternaOn;
    public GameObject iconLanternaOff;
    public GameObject creditosText; 
    public GameObject creditosFInaisText;
    public ScrollRect viewer;
    

    [Header("InfoItens")]
    public Image imgItem;
    public Sprite[] spritesItem;
    public Text textItem;

    [Header("FadeSplash")]
    public float delayFadeIn = 1;
    public float velocidadeFadeIn = 1;

  //  public List<Image> cutsceneCenas = new List<Image>();

    private float contador;
    public bool outraTelaAberta;
    public static MenuManager GetInstance()
	{
        return instance;
	}

    void Awake()
	{
        instance = this;

        panelSplash.SetActive(true);
	}
    
    void Start()
    {
        StartCoroutine(SplashAbertura());
        panelMenu.SetActive(true);
    }

     void Update()
     {

      if(panelItem && Input.GetKeyDown(KeyCode.Escape))
        {
            panelItem.SetActive(false);
            outraTelaAberta = false;
        }
     }
    void LateUpdate()
    {
        if (creditos && Input.GetKeyDown(KeyCode.Escape) && !outraTelaAberta)
        {
            if (GameManager.GetInstance().Finalizado())
            {               
                GameManager.GetInstance().VoltarMenu();           
            }

            else
            {
                creditos = !creditos;
                viewer.verticalNormalizedPosition = 1;
                panelCreditos.SetActive(creditos);
                panelMenu.SetActive(!creditos);
                StartCoroutine(creditosMove());

            }
        }
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

        {
            if (GameManager.GetInstance().Finalizado())
            {               
                GameManager.GetInstance().VoltarMenu();          
            }
            else
            {
                creditos = !creditos;
                panelMenu.SetActive(!creditos);
                panelCreditos.SetActive(creditos);
                StartCoroutine(creditosMove());

            }
        }
    }

    public void FecharPanelItem()
	{
        panelItem.SetActive(false);
        if(GameManager.GetInstance().qtdItens <= 2)
		{
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().plSource2, AudioHelper.GetInstance().doorOpen);
        }
	}

    public void InfoPanelItem(string nome, Sprite image)
	{
        textItem.text = nome;
        imgItem.overrideSprite = image;
        outraTelaAberta = true;
    }

    public void AvancarControles()
	{
        imgControles1.SetActive(false);
        imgControles2.SetActive(true);
	}
    public void VoltarControles()
	{
        imgControles2.SetActive(false);
        imgControles1.SetActive(true);
	}

    public void Quit()
	{
        Application.Quit();
	}

    IEnumerator creditosMove()
    {
        while (creditos)
        {
            creditosText.transform.position += Vector3.up;
            yield return new WaitForSeconds(0.01f);    
        }
    }

    public void CreditosFinais()
    {
        StartCoroutine(creditosFinaisMove());
    }

    IEnumerator creditosFinaisMove()
    {
        while (creditosFInaisText)
        {
            creditosFInaisText.transform.position += Vector3.up;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
