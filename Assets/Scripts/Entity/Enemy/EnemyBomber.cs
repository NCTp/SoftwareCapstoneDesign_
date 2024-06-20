using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Enemy
{
    public GameObject explosionEffect;
    public float explosionRadius = 10.0f;
    public float explosionDelay = 2.0f;
    public float damage;
    void Explode()
    {
        GameObject _explosionEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                hitCollider.gameObject.GetComponent<Player>().GetDamage(new DamageMessage(gameObject, damage));
            }
            
        }
         Dead();
    }

    System.Collections.IEnumerator ExplosionCoroutine()
    {
        yield return new WaitForSeconds(explosionDelay);
        Explode();
    }
    void Awake()
    {
        _target = GameObject.FindWithTag("Player");
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
            if (Vector3.Distance(gameObject.transform.position, _target.transform.position) <= 10.0f)
            {
                Explode();
            }
        }
    }
}
