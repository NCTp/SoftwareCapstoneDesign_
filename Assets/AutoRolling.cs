using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRolling : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(rotationSpeed * Time.time,0.0f, 0.0f);
    }
}