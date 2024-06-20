using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnGameObject;
    public Transform[] spawnPoints;

    public float spawnRate = 5.0f;

    private float spawnTimer = 0.0f;

    public bool spawnStart = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnStart)
        {
            if (spawnTimer >= spawnRate)
            {
                int temp = Random.Range(0,spawnPoints.Length);
                GameObject _spawnGameObject = Instantiate(spawnGameObject, spawnPoints[temp].position, transform.rotation);
                spawnTimer = 0.0f;
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }
        }
        
    }
}
