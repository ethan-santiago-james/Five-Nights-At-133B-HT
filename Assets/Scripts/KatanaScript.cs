using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaScript : MonoBehaviour
{

    public Transform player;
    public bool isPickedUp;
    public bool isSwinging;

    int durability = 15;
    List<Vector3> potentialPositions = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        isPickedUp = false;
        transform.rotation = Quaternion.Euler(90f,180f,0f);
        potentialPositions.Add(new Vector3(52.694f,2.499f,34.71f));
        potentialPositions.Add(new Vector3(66.225f,0.548f,21.54f));
        potentialPositions.Add(new Vector3(101.19f,0.01f,41.96f));
        potentialPositions.Add(new Vector3(128.84f,0.091f,12.343f));
        transform.position = potentialPositions[Random.Range(0, potentialPositions.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && isPickedUp && !isSwinging && !PlayerState.Instance.isInCamMenu) {

            StartCoroutine(swing());
        }
    }

    public void pickUp() {

        gameObject.layer = LayerMask.NameToLayer("KATANA");
        isPickedUp = true;
        transform.SetParent(player);
        transform.localPosition = new Vector3(0.227f,2.182f,0.453f);
        transform.localRotation = Quaternion.Euler(184f,76f,-200f);
    }

    public IEnumerator swing() {

        isSwinging = true;
        PlayerState.Instance.isAttacking = true;

        for (int i = 0; i < 60; i++) {

            transform.Rotate(0f,0f,-1f);
            yield return new WaitForSeconds(0.005f);
        }
        
         for (int i = 0; i < 60; i++) {

            transform.Rotate(0f,0f,1f);
            yield return new WaitForSeconds(0.005f);
        }
        isSwinging = false;
        PlayerState.Instance.isAttacking = false;
        durability -= 1;

        if(durability == 0) {

            Destroy(gameObject);
        }
    }
}
