using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Animatronic")) {

            //Debug.Log(other.gameObject.name + " is touching a wall.");
            other.gameObject.GetComponent<Animatronic>().isAgainstWall = true;
            
        }

    }

    private void OnTriggerExit(Collider other) {

        if(other.CompareTag("Animatronic")) {

           other.gameObject.GetComponent<Animatronic>().isAgainstWall = false;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
