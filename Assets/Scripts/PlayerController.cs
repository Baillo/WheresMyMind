using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    [Header("PlayerMovement")]
    public CharacterController controller;
    public float playerSpeed = 2.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    Vector3 oldPos;
    bool chegouCabana;
    bool chegouCamping;
    bool chegouAmbulancia;

    [Header("Lanterna")]
    public bool lanternaOnOff;
    public GameObject lanterna;
    private bool temLanterna;
    private bool podeInteragir;
    private GameObject itemInteracao;

    private bool controlavel;

    public static PlayerController GetInstance()
	{
        return instance;
	}

    public void SetControlavel(bool vl)
	{
        controlavel = vl;
        oldPos = transform.position;
	}

    public bool GetControlavel()
	{
        return controlavel;
	}

    void Awake()
	{
        instance = this;
	}
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(GameManager.GetInstance().Inicializado() && controlavel && !GameManager.GetInstance().Finalizado() && !MenuManager.GetInstance().panelItem.activeInHierarchy)
		{
            Move();
            if (Input.GetKeyDown(KeyCode.F))
            {
                LanternaToggle();
            }
			if (Input.GetKeyDown(KeyCode.E))
			{
                Interagir();
			}
        }
    }

	public void Move()
	{
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //this.transform.position += move * playerSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log("isGrounded " + isGrounded);
        if(isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
		}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * playerSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
		{
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
        Vector3 newPos = transform.position;

        if(oldPos != newPos)
		{
			if (!AudioHelper.GetInstance().plSource1.isPlaying)
			{
                AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().plSource1, AudioHelper.GetInstance().plPassosFloresta);
            }
        }
        else if(oldPos == newPos)
		{
            AudioHelper.GetInstance().StopAudio(AudioHelper.GetInstance().plSource1);
        }
        oldPos = transform.position;
    }

    public void LanternaToggle()
	{
        lanternaOnOff = !lanternaOnOff;

		if (lanternaOnOff)
		{
            MenuManager.GetInstance().iconLanternaOff.SetActive(false);
            MenuManager.GetInstance().iconLanternaOn.SetActive(true);
            lanterna.SetActive(true);
		}
		else
		{
            MenuManager.GetInstance().iconLanternaOff.SetActive(true);
            MenuManager.GetInstance().iconLanternaOn.SetActive(false);
            lanterna.SetActive(false);
		}
	}

    public void Interagir()
	{
		if (podeInteragir)
		{
            Itens item = itemInteracao.GetComponent<Itens>();

			for (int i = 0; i < GameManager.GetInstance().itensColetados.Length; i++)
			{
                if(GameManager.GetInstance().itensColetados[i] == null)
				{
                    GameManager.GetInstance().itensColetados[i] = item;
                    GameManager.GetInstance().qtdItens += 1;
                    if (item.portaFechada != null && item.portaAberta != null) item.portaFechada.SetActive(false); item.portaAberta.SetActive(true);
                    AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().plSource2, AudioHelper.GetInstance().itemColetado);
                    MenuManager.GetInstance().panelItem.SetActive(true);

                    if(GameManager.GetInstance().qtdItens == 1)
					{
                        AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasPrimeiroItem, 1);
                        MenuManager.GetInstance().InfoPanelItem(item.nome, MenuManager.GetInstance().spritesItem[0]);
                    }
                    else if(GameManager.GetInstance().qtdItens == 2)
					{
                        AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasSegundoItem, 2);
                        MenuManager.GetInstance().InfoPanelItem(item.nome, MenuManager.GetInstance().spritesItem[1]);
                    }
                    else if(GameManager.GetInstance().qtdItens == 3)
					{
                        AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasTerceiroItem, 3);
                        MenuManager.GetInstance().InfoPanelItem(item.nome, MenuManager.GetInstance().spritesItem[2]);
                    }
                    break;
				}
			}

            Debug.Log("itensColetados " + GameManager.GetInstance().itensColetados);
            //Destroy(itemInteracao);
            itemInteracao.SetActive(false);
            podeInteragir = false;
            MenuManager.GetInstance().imgInteragir.SetActive(false);
            itemInteracao = null;
        }
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Item"))
		{
            podeInteragir = true;
            MenuManager.GetInstance().imgInteragir.SetActive(true);
            itemInteracao = other.gameObject;
		}
        else if (other.gameObject.CompareTag("FimDeJogo"))
		{
            GameManager.GetInstance().FimDeJogo();
		}
        else if (other.gameObject.CompareTag("TrocaSoundscape"))
		{
            if(GameManager.GetInstance().qtdItens == 1)
			{
                AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scForest2);
                AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().sfxsSources[1], AudioHelper.GetInstance().plChoro2);
                AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasChoro, 6);
                Destroy(other.gameObject);
			}
            else if(GameManager.GetInstance().qtdItens == 2)
			{
                AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scForest3);
                AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().sfxsSources[2], AudioHelper.GetInstance().sfxTrovao);
                AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasChuva, 5);
                Destroy(other.gameObject);
            }
            //AudioHelper.GetInstance().PlayRandomAudio();
		}
        else if (other.gameObject.CompareTag("SoundscapeCabana"))
		{
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scCabana);

			if (!chegouCabana)
			{
                chegouCabana = true;
                AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasCabana, 4);
            }
		}
        else if (other.gameObject.CompareTag("TriggerAudio"))
		{
            AudioHelper.GetInstance().PlayRandomAudio();
            AudioHelper.GetInstance().PlaySusto();
            other.gameObject.SetActive(false);          
		}
        else if (other.gameObject.CompareTag("TriggerCamping"))
		{
			if (!chegouCamping)
			{
                chegouCamping = true;
                AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasFogueira, 9);
            }
		}
        else if (other.gameObject.CompareTag("TriggerAmbulancia"))
		{
			if (!chegouAmbulancia)
			{
                chegouAmbulancia = true;
                AudioHelper.GetInstance().PlayFala(AudioHelper.GetInstance().falasSource, AudioHelper.GetInstance().falasAmbulancia, 7);
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Item"))
		{
            podeInteragir = false;
            MenuManager.GetInstance().imgInteragir.SetActive(false);
            itemInteracao = null;
		}
        else if (other.gameObject.CompareTag("SoundscapeCabana"))
		{
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().ultimoSoundscape);
        }
	}
}
