using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum ProjectileType
    {
        PlayerProjecitle,
        EnemyProjectile
    }
    public float speed = 1.0f;
    public float damage = 0.5f;
    public ProjectileType projectileType;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (projectileType == ProjectileType.PlayerProjecitle)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("EnemyDamaged");
                    enemy.GetDamage(new DamageMessage(gameObject, damage));
                    Destroy(gameObject);
                }
            }
        }
        else if (projectileType == ProjectileType.EnemyProjectile)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    Debug.Log("PlayerDamaged");
                    player.GetDamage(new DamageMessage(gameObject, damage));
                    Destroy(gameObject);
                }
            }
        }
        //Destroy(gameObject);

    }
}
