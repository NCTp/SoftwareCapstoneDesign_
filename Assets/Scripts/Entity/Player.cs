using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float scrabs;
    public float speed = 5.0f;
    public float jumpForce = 100.0f;

    private Rigidbody _rb;
    private SphereCollider _sphereCollider;
    private float _health;
    private float _energy;

    private int _level;

    private bool isGrounded = true;
    private RaycastHit _hitInfo;

    public void GetDamage(DamageMessage damageMessage)
    {
        _health -= damageMessage.amount;
    }
    public float GetHealth()
    {
        return _health;
    }
    public void AddHealth(float amount)
    {
        _health += amount;
        if(_health >= maxHealth) _health = maxHealth;
    }
    public float GetEnergy()
    {
        return _energy;
    }
    public void AddEnergy(float amount)
    {
        _energy += amount;
        if(_energy >= 100)
        {
            _level += 1;
            _energy -= 100;
        }
        
    }
    public void RigidbodyReset()
    {
        _rb.velocity = Vector3.zero;
    }
    public void RigidbodyAddForce()
    {
        _rb.AddForce(0.0f, jumpForce, 0.0f);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);

        _rb.AddForce(direction * speed);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.AddForce(0.0f, jumpForce, 0.0f);
        }
        if(Physics.Raycast(transform.position, Vector3.down, out _hitInfo, transform.localScale.x/2 + 0.2f))
        {
            isGrounded = true;
            //Debug.Log(transform.localScale.x);
        }
        else
        {
            isGrounded = false;
            //Debug.Log("Not Grounded!");
        }

    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _health = maxHealth;
        _level = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.E)) _health -= 10.0f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.GetDamage(new DamageMessage(gameObject, 3.0f));
        }
    }
    

    
}
