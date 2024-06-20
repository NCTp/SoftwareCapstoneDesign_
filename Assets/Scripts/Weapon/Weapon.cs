using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootRate = 0.1f;
    public float damage = 0.1f;
    public GameObject projectile;
    public GameObject target;
    public Transform muzzle;
    public GameObject muzzleFlash;
    public Player player;
    public AudioClip[] fireSounds;

    private float rotationX, rotationY;
    private AudioSource _audioSource;
  

    public void Fire()
    {
        GameObject _projectile = Instantiate(projectile, transform.position, transform.rotation);
        int temp = Random.Range(0,3);
        _audioSource.PlayOneShot(fireSounds[temp]);
        _projectile.GetComponent<Projectile>().damage = damage + (player.GetLevel() * 0.3f);
        _projectile.GetComponent<Rigidbody>().velocity =
            transform.forward * _projectile.GetComponent<Projectile>().speed;
    }

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
