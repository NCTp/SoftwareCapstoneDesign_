using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Markable : MonoBehaviour
{
    public GameObject mark;

    public void SetActiveMark()
    {
        mark.SetActive(true);
    }
    public void SetInActiveMark()
    {
        mark.SetActive(false);
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
