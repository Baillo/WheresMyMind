using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    /*public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2}
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public float minimumX = -360f;
    public float maximumX = 360f;

    public float minimumY = -60f;
    public float maximumY = 60f;

    float rotationY = 0f;*/

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public Transform lanterna;
    float xRotation = 0f;

	// Update is called once per frame

	private void Start()
	{
        //Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
        /*if(axes == RotationAxes.MouseXAndY)
		{
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
        else if(axes == RotationAxes.MouseX)
		{
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}*/

        
        if(GameManager.GetInstance().Inicializado() && PlayerController.GetInstance().GetControlavel() 
            && !GameManager.GetInstance().Finalizado() && !MenuManager.GetInstance().panelItem.activeInHierarchy)
		{
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 30f);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            lanterna.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
