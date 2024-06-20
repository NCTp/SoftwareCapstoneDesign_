using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMeshDestroy : MonoBehaviour
{
    private MeshDestroy _meshDestroy;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshDestroy>().DestroyMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
