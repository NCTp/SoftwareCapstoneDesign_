using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : Enemy
{
    private RaycastHit _hitInfo;
    private Vector3 _targetDir;
    private bool _isLockedOn = false;
    private LineRenderer _lineRenderer;
    public float rayRange = 10.0f;
    public float fireRate = 5.0f;
    public GameObject muzzle;
    public GameObject rayEffects;
    void Fire()
    {


    }
    void Awake()
    {
        _target = GameObject.FindWithTag("Player");
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.enabled = false;
        rayEffects.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _targetDir = (_target.transform.position - muzzle.transform.position).normalized;
        if(Physics.Raycast(muzzle.transform.position, _targetDir, out _hitInfo, rayRange))
        {
            if(_hitInfo.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player!");
                _lineRenderer.enabled = true;
                rayEffects.transform.position = _hitInfo.point;
                //rayEffects.transform.rotation = Quaternion.LookRotation(_hitInfo.normal);
                rayEffects.SetActive(true);
                _lineRenderer.SetPosition(0, muzzle.transform.position);
                _lineRenderer.SetPosition(1, rayEffects.transform.position);
            }
            else
            {
                _lineRenderer.enabled = false;
                rayEffects.SetActive(false);
            }

        }
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }

    }
}
