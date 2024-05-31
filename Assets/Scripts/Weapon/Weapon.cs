using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float shootRate = 0.1f;
    public float distance = 10.0f;
    public Transform muzzle;
    public GameObject target;
    private RaycastHit _hitInfo;
    private Camera _mainCamera;

    public void ShootRayCast()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("NoDetectionFromPlayer"));
        if(Physics.Raycast(
        muzzle.position, 
        transform.forward, 
        out _hitInfo, 
        distance,
        layerMask))
        {
            GameObject hitObject = _hitInfo.collider.gameObject;
            if (hitObject != null)
            {
                if(hitObject.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy");
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
