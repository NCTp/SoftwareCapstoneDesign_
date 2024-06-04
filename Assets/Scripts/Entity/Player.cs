using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    
    public float maxHealth;
    public float scrabs = 0;
    public float speed = 5.0f;
    public float accelValue = 1.0f;
    public float chargeSpeed = 50.0f;
    public float jumpForce = 100.0f;
    public float collisionDamage = 3.0f;
    public float fireRate = 1.0f;
    public Vector3 direction;

    public Weapon weapon;
    public GameObject chargeTarget;
    public GameObject Wheel;

    private Rigidbody _rb;
    private SphereCollider _sphereCollider;
    private CharacterController _controller;
    private float _health;
    private float _speed;
    private float _energy = 50.0f;
    private float _gravityValue = -9.81f;
    private int _level;
    private float _nextFireTime = 0.0f;

    private bool _isGrounded = true;
    private bool isCharging = false;
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
        //if(horizontal == vertical) RigidbodyReset();

        direction = new Vector3(horizontal, 0.0f, vertical);

        _rb.AddForce(direction * speed);
        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(0.0f, jumpForce, 0.0f);
        }
        if(Physics.Raycast(transform.position, Vector3.down, out _hitInfo, transform.localScale.x/2 + 0.2f))
        {
            _isGrounded = true;
            //Debug.Log(transform.localScale.x);
        }
        else
        {
            _isGrounded = false;
            //Debug.Log("Not Grounded!");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _rb.AddForce(direction * chargeSpeed / 2);
        }
        
    }
    private void Move2()
    {
        _isGrounded = _controller.isGrounded;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //if(horizontal == vertical) RigidbodyReset();
        if (horizontal == 0 && vertical == 0)
            _speed = speed;
        if (_isGrounded)
        {
            direction = new Vector3(horizontal, 0.0f, vertical);
            direction = transform.TransformDirection(direction) * _speed;
            _speed += accelValue * Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Jump");
                direction.y += Mathf.Sqrt(jumpForce * -10.0f * _gravityValue);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                direction += new Vector3(horizontal, 0.0f, vertical) * 5.0f;
            }
        }
        direction.y += _gravityValue * 50.0f * Time.deltaTime;
        Vector3 rotationDirection = new Vector3(vertical, 0.0f, -horizontal);
        Wheel.transform.Rotate(rotationDirection * 500.0f * Time.deltaTime);
        _controller.Move(direction * Time.deltaTime);
    }
    
    public void SetChargeTarget(GameObject gameObject)
    {
        chargeTarget = gameObject;
        chargeTarget.GetComponent<Markable>().SetActiveMark();
    }

    public void DeleteTarget()
    {
        chargeTarget.GetComponent<Markable>().SetInActiveMark();
        chargeTarget = null;
    }

    private void Charge(GameObject chargeTarget)
    {
        // 특정된 타겟을 향해 돌진
        if (chargeTarget != null)
        {
            Vector3 _direction = chargeTarget.transform.position - transform.position;
            direction.Normalize();
            transform.position += _direction * chargeSpeed * Time.deltaTime;

        }
        
    }

    private void Fire()
    {
        weapon.Fire();
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sphereCollider = GetComponent<SphereCollider>();
        _controller = GetComponent<CharacterController>();
        _health = maxHealth;
        _level = 1;
        _speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + fireRate;
            Fire();
        }
        Move2();
        if (chargeTarget != null)
        {
            chargeTarget.GetComponent<Markable>().SetActiveMark();
        }
        if(Input.GetKeyDown(KeyCode.E)) _health -= 10.0f;
        /*
        if (Input.GetMouseButton(0))
        {
            //RigidbodyReset();
            if (chargeTarget != null)
            {
                RigidbodyReset();
                Charge(chargeTarget);
            }
        }
        */
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Debug.Log("DD");
            enemy.GetDamage(new DamageMessage(gameObject, collisionDamage));

        }
    }
    

    
}
