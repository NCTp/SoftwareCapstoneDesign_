using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 5.0f;
    public float score = 1.0f;
    private GameObject _target;
    private NavMeshAgent _navMeshAgent;

    public void GetDamage(DamageMessage damageMessage)
    {
        health -= damageMessage.amount;
        if (health <= 0)
        {
            if (damageMessage.damager.gameObject.CompareTag("Player"))
            {
                damageMessage.damager.gameObject.GetComponent<Player>().ResetCoolTime();
            }
            Dead();
            
        }
    }
    public float GetHealth()
    {
        return health;
    }
    private void Dead()
    {
        GameManager.instance.AddScore(score);
        GetComponent<MeshDestroy>().DestroyMesh();
        Destroy(gameObject, 3.0f);
    }

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
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }

    }
}
