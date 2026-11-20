using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isOpen;
    public bool isClosingOrOpening;
    private float yPos;

    public static DoorScript Instance { get; set; }
    
    void Awake() {

        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {

            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
        isOpen = false;
        isClosingOrOpening = false;
    }

    void Update() {

        if(PlayerState.Instance.isRespawning && !isClosingOrOpening && isOpen) {

            StartCoroutine(closeDoor());
        }
    }

    public IEnumerator openDoor() {

        isClosingOrOpening = true;
        for (int i = 0; i < 100; i++) {

            float newPos = yPos + i*0.1f;
            transform.position = new Vector3(transform.position.x,newPos,transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        isClosingOrOpening = false;
        isOpen = true;

    }

    public IEnumerator closeDoor() {

        isClosingOrOpening = true;
        for (int i = 0; i < 100; i++) {

            float newPos = yPos + 10f - i*0.1f;
            transform.position = new Vector3(transform.position.x,newPos,transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        isClosingOrOpening = false;
        isOpen = false;

    }
}
