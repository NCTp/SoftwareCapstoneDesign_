using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class EnemyWormBody : MonoBehaviour, IDamageable
{
    public GameObject projectile;
    
    public float health = 5.0f;
    private float CurrentHealth { get; set; }
    public float repairInterval = 10.0f;
    private float _repairTimer = 0.0f;
    public float fireRate = 2.0f;
    private float _fireTimer = 0.0f;
    public GameObject bodyMesh;
    public GameObject bodyMesh2;
    public GameObject destroyEffect;
    
    private Player _player;
    private GameObject _bodyMesh;
    private MeshDestroy _meshDestroy;
    private SphereCollider _sphereCollider;
    private bool _isDestroyed = false;
    private Vector3 _dir_L;
    private Vector3 _dir_R;

    public Transform muzzle_L;
    public Transform muzzle_R;
    
    public Enemy Boss;
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

            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Boss.GetDamage(new DamageMessage(gameObject, health));
        }
    }
    void Fire()
    {
        GameObject _projectile_L = Instantiate(projectile, muzzle_L.transform.position, muzzle_L.transform.rotation);
        GameObject _projectile_R = Instantiate(projectile, muzzle_R.transform.position, muzzle_R.transform.rotation);
        //_dir_L = (_player.transform.position - muzzle_L.position).normalized;
        //_dir_R = (_player.transform.position - muzzle_R.position).normalized;
        
        _projectile_L.GetComponent<Projectile>().SetDirection(Vector3.left);
        _projectile_R.GetComponent<Projectile>().SetDirection(Vector3.right);
        
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
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player.transform.position);
        if (!_isDestroyed)
        {
            if (CurrentHealth <= 0)
            {
                _isDestroyed = true;
                //GameObject _bodyMesh2 = Instantiate(bodyMesh2, transform.position, Quaternion.identity);
                _meshDestroy.DestroyMesh();
                _sphereCollider.enabled = false;
            }

            if (_fireTimer >= fireRate)
            {
                //Fire();
                _fireTimer = 0.0f;
            }
            else
            {
                _fireTimer += Time.deltaTime;
            }
        }
        else
        {
            _repairTimer += Time.deltaTime;
            if (_repairTimer >= repairInterval)
            {
                Repair();
                _repairTimer = 0.0f;

            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GetDamage(new DamageMessage(gameObject, 5.0f));
        }
    }

}
