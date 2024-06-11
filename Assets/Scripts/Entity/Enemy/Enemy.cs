using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 5.0f;
    public float score = 1.0f;
    protected GameObject _target;
    protected NavMeshAgent _navMeshAgent;
    protected Color originalColor;
    protected MeshRenderer _meshRenderer;

    public void GetDamage(DamageMessage damageMessage)
    {
        StartCoroutine(DamageEffectCoroutine());
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

    System.Collections.IEnumerator DamageEffectCoroutine()
    {
        _meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.05f); // 0.1초동안 대기하기
        _meshRenderer.material.color = originalColor;
    }
    public float GetHealth()
    {
        return health;
    }
    protected void Dead()
    {
        GameManager.instance.AddScore(score);
        _meshRenderer.material.color = originalColor;
        GetComponent<MeshDestroy>().DestroyMesh();
        Destroy(gameObject, 3.0f);
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
        }

    }
}
