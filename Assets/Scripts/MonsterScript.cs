using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    List<Vector3> potentialPositions = new List<Vector3>();
    public bool needsNewPosition;
    // Start is called before the first frame update
    void Start()
    {
        needsNewPosition = false;
        potentialPositions.Add(new Vector3(51.98f,0.62f,36.81f));
        potentialPositions.Add(new Vector3(51.98f,0.62f,18.3f));
        potentialPositions.Add(new Vector3(84.04f,0.62f,41.6f));
        potentialPositions.Add(new Vector3(130.39f,0.62f,48.34f));
        potentialPositions.Add(new Vector3(139.34f,0.566f,48.34f));
        potentialPositions.Add(new Vector3(149.14f,0.566f,52.292f));
        potentialPositions.Add(new Vector3(138.65f,0.62f,88.88f));
        potentialPositions.Add(new Vector3(69.28f,0.566f,15.66f));
        potentialPositions.Add(new Vector3(66.09f,0.82f,18.91f));
        transform.position = potentialPositions[Random.Range(0, potentialPositions.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if(needsNewPosition) {

            transform.position = potentialPositions[Random.Range(0, potentialPositions.Count)];
            needsNewPosition = false;
        }
    }
}
