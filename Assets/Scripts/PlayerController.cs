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
        
        if(GameManager.GetInstance().Inicializado() && controlavel && !GameManager.GetInstance().Finalizado())
		{
            Move();
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                LanternaToggle();
            }
			if (Input.GetKeyDown(KeyCode.F))
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void LanternaToggle()
	{
        lanternaOnOff = !lanternaOnOff;

		if (lanternaOnOff)
		{
            lanterna.SetActive(true);
		}
		else
		{
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
                    if (item.porta != null) item.porta.SetActive(false);
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
                Destroy(other.gameObject);
			}
            else if(GameManager.GetInstance().qtdItens == 2)
			{
                AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scForest3);
                Destroy(other.gameObject);
            }
		}
        else if (other.gameObject.CompareTag("SoundscapeCabana"))
		{
            AudioHelper.GetInstance().PlayAudio(AudioHelper.GetInstance().scSource, AudioHelper.GetInstance().scCabana);
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
