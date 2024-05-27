using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float speed = 5.0f;
    public float jumpForce = 100.0f;

    private Rigidbody _rb;
    private float _health;
    private int _level;

    private bool isGrounded = true;
    private RaycastHit _hitInfo;

    public void GetDamage(DamageMessage damageMessage)
    {
        Debug.Log("!");
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);

        _rb.AddForce(direction * speed);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.AddForce(0.0f, jumpForce, 0.0f);
        }
        if(Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 0.6f))
        {
            isGrounded = true;
            Debug.Log("Grounded!");
        }
        else
        {
            isGrounded = false;
            Debug.Log("Not Grounded!");
        }

    }
}
