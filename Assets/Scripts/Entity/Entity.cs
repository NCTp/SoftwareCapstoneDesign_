using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject posMark;
    private RaycastHit _hitinfo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Managers")); 
        if (Physics.Raycast(transform.position, Vector3.down, out _hitinfo, 100.0f, layerMask))
        {
            Debug.Log(_hitinfo.collider.gameObject.name);
            if (_hitinfo.collider.gameObject.CompareTag("Wall"))
            {
                posMark.transform.position = _hitinfo.point;
            }
        }
    }
}
