using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
        HealthPack,
        EnergyPack
    }
    public Type itemType;
    public float amount;
    public float rotationSpeed = 5.0f;

    void GetItem(Player player)
    {
        switch(itemType)
        {
            case Type.EnergyPack:
                player.AddEnergy(amount);
                Debug.Log("Get Energy");
                break;
            case Type.HealthPack:
                player.AddHealth(amount);
                Debug.Log("Get Health");
                break;
            default:
                Debug.Log("Get Item");
                break;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null) 
        {
            GetItem(player);
            Destroy(gameObject);
        }
    }
}
