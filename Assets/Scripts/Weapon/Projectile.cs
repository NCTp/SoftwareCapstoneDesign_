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
    public GameObject hitEffect;

    public void SetDirection(Vector3 dir)
    {
        gameObject.GetComponent<Rigidbody>().velocity = dir * speed;
    }
    
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
                IDamageable enemy = other.GetComponent<IDamageable>();
                if (enemy != null)
                {
                    Debug.Log("EnemyDamaged");
                    enemy.GetDamage(new DamageMessage(gameObject, damage));
                    GameObject _hitEffect = Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                //Debug.Log("Wall Detected");
                GameObject _hitEffect = Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else if (projectileType == ProjectileType.EnemyProjectile)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                   // Debug.Log("PlayerDamaged");
                    player.GetDamage(new DamageMessage(gameObject, damage));
                    GameObject _hitEffect = Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                //Debug.Log("Wall Detected");
                GameObject _hitEffect = Instantiate(hitEffect, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        //Destroy(gameObject);
    }
}
