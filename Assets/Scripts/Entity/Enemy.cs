using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    float health = 5.0f;

    public void GetDamage(DamageMessage damageMessage)
    {
        health -= damageMessage.amount;
        damageMessage.damager.GetComponent<Player>().RigidbodyReset();
        damageMessage.damager.GetComponent<Player>().RigidbodyAddForce();
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) Dead();
    }
}
