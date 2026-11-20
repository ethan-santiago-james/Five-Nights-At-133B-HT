using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity = 400f;

    float xRotation = 0f;
    float yRotation = 0f;

    int deathCount = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        transform.rotation = Quaternion.Euler(0f,0f,0f);

    }

    // Update is called once per frame
    void Update()
    {

        if(MenuScript.Instance.gameStarted == true && !PlayerState.Instance.isInCamMenu) {

            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

                xRotation -= mouseY;

                if(xRotation < -90f) {

                    xRotation = -90f;
                } else if(xRotation > 90f) {

                    xRotation = 90f;
                }

                yRotation += mouseX;

                transform.rotation = Quaternion.Euler(xRotation,yRotation,0f);


            Cursor.lockState = CursorLockMode.Locked;

        } else {

            Cursor.lockState = CursorLockMode.None;
        }

        
    }

}
