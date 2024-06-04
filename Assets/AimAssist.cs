using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AimAssist : MonoBehaviour
{
    public Collider target;
    private GameObject player;
    
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Collider GetTarget()
    {
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(enemy.health <= player.GetComponent<Player>().collisionDamage) 
                player.GetComponent<Player>().SetTarget(other.gameObject);
            else
            {
                player.GetComponent<Player>().SetTarget(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            player.GetComponent<Player>().DeleteTarget();
            
        }
    }
}
