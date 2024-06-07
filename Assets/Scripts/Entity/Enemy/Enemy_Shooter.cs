using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : Enemy
{
    void Awake()
    {
        _target = GameObject.FindWithTag("Player");
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
