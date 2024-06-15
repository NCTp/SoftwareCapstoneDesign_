using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float length = 1.0f;

    private float yPos = 0.0f;
    private float mathStandard = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mathStandard += Time.deltaTime * speed;
        yPos = Mathf.Sin(mathStandard) * length;
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
