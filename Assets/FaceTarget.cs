using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    public enum FacingTarget
    {
        Player,
        Enemy
    }

    public FacingTarget facingTarget;
    private GameObject target;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (facingTarget == FacingTarget.Player)
        {
            target = GameObject.FindWithTag("Player");
        }
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
            Vector3 dir = (target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
