using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnGameObject;
    public GameObject dangerIndicator;
    public Transform[] spawnPoints;

    public float spawnRate = 5.0f;

    private float spawnTimer = 0.0f;
    private Vector3 _dir;

    public bool spawnStart = false;

    void Awake()
    {
        _dir = transform.position;
    }
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
                GameObject _dangerIndicator =
                    Instantiate(dangerIndicator, spawnPoints[temp].position, transform.rotation);
                _dangerIndicator.transform.rotation = Quaternion.Euler(0,90,0);
                spawnTimer = 0.0f;
            }
            else
            {
                spawnTimer += Time.deltaTime;
            }
        }
        
    }
}
