using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathRecorder targetPathRecorder;

    public float followSpeed = 2.0f;
    public float stopDistance = 0.5f;

    private int currentPathIndex = 0;

    void Awake()
    {
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPathRecorder.transform.position);
        if (targetPathRecorder != null 
            && currentPathIndex < targetPathRecorder.pathPositions.Count 
            && distance > stopDistance)
        {
            Vector3 targetPosition = targetPathRecorder.pathPositions[currentPathIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
    }
}
