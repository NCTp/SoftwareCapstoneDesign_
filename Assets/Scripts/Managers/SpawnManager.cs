using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public GameObject spawnObject;
    public SpawnPoint[] spawnPoints;
    public float spawnRate = 1.0f;
    private float _spawnTimer = 0.0f;
    private BoxCollider _boxCollider;

    Vector3 GetRandomPosition()
    {
        Vector3 originPosition = transform.position;
        float range_X = _boxCollider.bounds.size.x;
        float range_Z = _boxCollider.bounds.size.z;
        range_X = Random.Range( (range_X / 2) * -1, range_X / 2);
        range_Z = Random.Range( (range_Z / 2) * -1, range_Z / 2);
        Vector3 randomPostion = new Vector3(range_X, 0f, range_Z);
        Vector3 spawnPosition = originPosition + randomPostion;
        return spawnPosition;
    }

    void Awake()
    {
        // 싱글톤 설정
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _boxCollider = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnTimer >= spawnRate)
        {
            GameObject _spawnObject = Instantiate(spawnObject, GetRandomPosition(), Quaternion.identity);
            _spawnTimer = 0.0f;
        }
        else
        {
            _spawnTimer += Time.deltaTime;
        }
    }
}
