using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwearScript : MonoBehaviour
{
    List<Vector3> potentialPositions = new List<Vector3>();
    public bool collected;

    [SerializeField] private float hoverHeight = 0.5f;  // how far up/down it moves
    [SerializeField] private float hoverSpeed = 2f;     // how fast it moves

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        hoverSpeed = Random.Range(1.5f,2f);
        potentialPositions.Add(new Vector3(38.07f,-1.9f,15.85f));
        potentialPositions.Add(new Vector3(70.94f,-8.7f,14.81f));
        potentialPositions.Add(new Vector3(150.64f,-1.14f,77.31f));
        potentialPositions.Add(new Vector3(147.792f,-1.14f,64.278f));
        potentialPositions.Add(new Vector3(78.59f,-1.14f,22.88f));
        transform.position = potentialPositions[Random.Range(0, potentialPositions.Count)];
        startPos = transform.position;
        collected = false;  
        StartCoroutine(Hover());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Hover()
    {
        while (!collected)
        {
            // Ping-pong between -1 and 1, multiply by hoverHeight
            float newY = startPos.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;

            transform.position = new Vector3(startPos.x, newY, startPos.z);

            yield return null; // wait for next frame
        }
        Destroy(gameObject);
    }
}
