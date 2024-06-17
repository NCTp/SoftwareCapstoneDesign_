using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public GameObject healthPack;
    public GameObject energyPack;
    
    public float health = 5.0f;
    public float score = 1.0f;
    public bool isNavMeshAgentActive = true;
    public GameObject _target;
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

    private void DropItem()
    {
        int item = Random.Range(0, 2);
        if (item == 0)
        {
            GameObject _healthPack = Instantiate(healthPack, transform.position, Quaternion.identity);
        }
        else if (item == 1)
        {
            GameObject _energyPack = Instantiate(energyPack, transform.position, Quaternion.identity);
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
        DropItem();
        _meshRenderer.material.color = originalColor;
        GetComponent<MeshDestroy>().DestroyMesh();
        Destroy(gameObject, 3.0f);
    }

    void Awake()
    {
        _target = GameObject.FindWithTag("Player");
        if(isNavMeshAgentActive) _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null && isNavMeshAgentActive)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }

    }
}
