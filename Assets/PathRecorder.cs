using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRecorder : MonoBehaviour
{
    public List<Vector3> pathPositions = new List<Vector3>();

    public float recordInterval = 0.1f;

    private float recordTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            pathPositions.Add(transform.position);
            recordTimer = 0.0f;
        }
    }
}
