using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorScript : MonoBehaviour
{
    public bool isOpen = false;
    public bool isOpeningOrClosing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator openTrapdoor() {

        Debug.Log(gameObject.name + " opening");    
        if(gameObject.name == "Bench") {

            float movementWidth = 5.65f;
            isOpeningOrClosing = true;
            float initZ = transform.localPosition.z;

            for (int i = 0; i < 100; i++) {

                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initZ - (movementWidth*(i/100f)));
                yield return new WaitForSeconds(0.01f);

            }
            isOpeningOrClosing = false;
            isOpen = true;

        } else {

            float width = transform.localScale.x;
            float initialX = transform.localPosition.x;
            isOpeningOrClosing = true;
            for (int i = 0; i < 100; i++) {

                transform.localPosition = new Vector3(
                    initialX + ((i / 100f)*width),
                    transform.localPosition.y,
                    transform.localPosition.z
                );
                yield return new WaitForSeconds(0.01f);
            }
            isOpeningOrClosing = false;
            isOpen = true;

        }
        
    }

    public IEnumerator closeTrapdoor() {
        
        if(gameObject.name == "Bench" && !PlayerState.Instance.isUnderground) {

            float movementWidth = 5.65f;
            isOpeningOrClosing = true;
            float initZ = transform.localPosition.z;

            for (int i = 0; i < 100; i++) {

                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initZ + (movementWidth*(i/100f)));
                yield return new WaitForSeconds(0.01f);

            }
            isOpeningOrClosing = false;
            isOpen = false;

        } else if(gameObject.name != "Bench") {

            float width = transform.localScale.x;
            float initialX = transform.localPosition.x;
            isOpeningOrClosing = true;

            for (int i = 0; i < 100; i++) {

                transform.localPosition = new Vector3(initialX - ((i/100f)*width), transform.localPosition.y, transform.localPosition.z);
                yield return new WaitForSeconds(0.01f);
            }
            isOpeningOrClosing = false;
            isOpen = false;


        }
        
    }


}
