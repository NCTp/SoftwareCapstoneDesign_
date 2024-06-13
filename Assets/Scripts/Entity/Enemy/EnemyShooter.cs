using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooter : Enemy
{
    private RaycastHit _hitInfo;
    private Vector3 _targetDir;
    private bool _canAttack = true;
    private bool _isLockedOn = false;
    private float _lockedOnTime = 0.0f;
    private float _attackReadyTime = 0.0f;
    private LineRenderer _lineRenderer;
    public float rayRange = 10.0f;
    public float damage = 10.0f;
    public float fireRate = 5.0f;
    public GameObject muzzle;
    public GameObject rayEffects;
    public GameObject projectile;
    void Fire()
    {
        _targetDir = (_target.transform.position - gameObject.transform.position).normalized;
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy")) - (1 << LayerMask.NameToLayer("Camera"));
        if(Physics.Raycast(gameObject.transform.position, _targetDir, out _hitInfo, rayRange))
        {
            if(_hitInfo.collider.gameObject.CompareTag("Player"))
            {
                //Debug.Log(_lockedOnTime);
                _lineRenderer.enabled = true;
                //rayEffects.transform.position = _hitInfo.point;
                //rayEffects.transform.rotation = Quaternion.LookRotation(_hitInfo.normal);
                _lineRenderer.startWidth = _lockedOnTime * 0.1f;
                _lockedOnTime += Time.deltaTime;
                if(_lockedOnTime >= fireRate) 
                {
                    //_hitInfo.collider.gameObject.GetComponent<Player>().GetDamage(new DamageMessage(gameObject,damage));
                    GameObject _projectile = Instantiate(projectile, muzzle.transform.position,
                        Quaternion.Lerp(transform.rotation, 
                            Quaternion.LookRotation(_target.transform.position), 
                            5 * Time.deltaTime)); // 투사체 발사
                    _projectile.GetComponent<Rigidbody>().velocity =
                        transform.forward * _projectile.GetComponent<Projectile>().speed;
                    _lockedOnTime = 0.0f; // 락온 시간 초기화
                    _canAttack = false; // 공격 불가능한 상태로.
                    _lineRenderer.enabled = false; // 레이저 제거
                    rayEffects.SetActive(false); // ray 닿은 시점 이펙트 제거
                }
                else
                {
                    rayEffects.SetActive(true);
                    rayEffects.transform.position = _hitInfo.point;
                    _lineRenderer.SetPosition(0, muzzle.transform.position);
                    _lineRenderer.SetPosition(1, rayEffects.transform.position);
                }
            }
            else
            {
                _lockedOnTime = 0.0f;
                _lineRenderer.enabled = false;
                rayEffects.SetActive(false);
            }
        }
    }
    void Awake()
    {
        _target = GameObject.FindWithTag("Player");
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _meshRenderer = GetComponent<MeshRenderer>();
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
        _target = GameObject.FindWithTag("Player");
        if(_canAttack) Fire();
        else
        {
            _attackReadyTime += Time.deltaTime;
            if (_attackReadyTime >= fireRate)
            {
                _canAttack = true;
                _attackReadyTime = 0.0f;
            }
        }
        
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.transform.position);
        }

    }
}
