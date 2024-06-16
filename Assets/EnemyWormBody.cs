using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyWormBody : MonoBehaviour, IDamageable
{
    public float health = 5.0f;
    private float CurrentHealth { get; set; }
    public float repairInterval = 10.0f;
    private float _repairTimer = 0.0f;
    public GameObject bodyMesh;
    private GameObject _bodyMesh;
    private MeshDestroy _meshDestroy;
    private SphereCollider _sphereCollider;
    private bool _isDestroyed = false;
    public void GetDamage(DamageMessage damageMessage)
    {
        Debug.Log("Body Get Damage");
        CurrentHealth -= damageMessage.amount;
        if (CurrentHealth <= 0)
        {
            if (damageMessage.damager.gameObject.CompareTag("Player"))
            {
                damageMessage.damager.gameObject.GetComponent<Player>().ResetCoolTime();
            }
        }
    }

    public void Repair()
    {
        CurrentHealth = health;
        _isDestroyed = false;
        _sphereCollider.enabled = true;
        // 새로운 매쉬 장착
        GameObject _bodyMesh = Instantiate(bodyMesh, transform.position, Quaternion.identity);
        _bodyMesh.transform.parent = transform;
        _meshDestroy = _bodyMesh.GetComponent<MeshDestroy>();
    }
    
    void Awake()
    {
        GameObject _bodyMesh = Instantiate(bodyMesh, transform.position, Quaternion.identity);
        _bodyMesh.transform.parent = transform;
        _meshDestroy = _bodyMesh.GetComponent<MeshDestroy>();
        
        _sphereCollider = GetComponent<SphereCollider>();
        CurrentHealth = health;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDestroyed && CurrentHealth <= 0)
        {
            _isDestroyed = true;
            _meshDestroy.DestroyMesh();
            _sphereCollider.enabled = false;
        }

        if (_isDestroyed)
        {
            _repairTimer += Time.deltaTime;
            if (_repairTimer >= repairInterval)
            {
                Repair();
                _repairTimer = 0.0f;

            }
        }
    }
    
    
}
