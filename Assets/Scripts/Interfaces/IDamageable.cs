using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct DamageMessage
{
    public GameObject damager;
    public float amount;

    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public DamageMessage(GameObject _damager, float _amount)
    {
        this.damager = _damager;
        this.amount = _amount;
        this.hitPoint = new Vector3(0.0f,0.0f,0.0f);
        this.hitNormal = new Vector3(0.0f,0.0f,0.0f);
    }

    public DamageMessage(GameObject _damager, float _amount, Vector3 _hitPoint, Vector3 _hitNormal)
    {
        this.damager = _damager;
        this.amount = _amount;
        this.hitPoint = _hitPoint;
        this.hitNormal = _hitNormal;
    }
}
public interface IDamageable
{
    void GetDamage(DamageMessage damageMessage);
}
