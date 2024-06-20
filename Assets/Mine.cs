using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public GameObject explodeEffect;
    public AudioClip explosionSound;

    private AudioSource _audioSource;


    void Explode()
    {
        _audioSource.PlayOneShot(explosionSound);
        GameObject _explodeEffect = Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().GetDamage(new DamageMessage(gameObject, 10.0f));
            Explode();
            Debug.Log("Player Detected");
        }
    }

    
}
