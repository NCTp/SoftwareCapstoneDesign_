using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootRate = 0.1f;
    public GameObject projectile;
    public GameObject target;
    public Transform muzzle;

    private float rotationX, rotationY;
  

    public void Fire()
    {
        GameObject _projectile = Instantiate(projectile, transform.position, transform.rotation);
        _projectile.GetComponent<Rigidbody>().velocity =
            transform.forward * _projectile.GetComponent<Projectile>().speed;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
