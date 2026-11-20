using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isOpen;
    public bool isClosingOrOpening;
    void Start()
    {
        isOpen = false;
        transform.localPosition = new Vector3(-0.301f,0f,0.277f);
        transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }

    public IEnumerator openDoor() {

        isClosingOrOpening = true;
        for (int i = 0; i > -91; i--) {

            transform.localRotation = Quaternion.Euler(0f,i,0f); 
            yield return new WaitForSeconds(0.01f);
        }
        isClosingOrOpening = false;
        isOpen = true;

    }

    public IEnumerator closeDoor() {

        isClosingOrOpening = true;
        for (int i = -90; i < 1; i++) {

            transform.localRotation = Quaternion.Euler(0f,i,0f);
            yield return new WaitForSeconds(0.01f);    
        }
        isClosingOrOpening = false;
        isOpen = false;

    }
}
