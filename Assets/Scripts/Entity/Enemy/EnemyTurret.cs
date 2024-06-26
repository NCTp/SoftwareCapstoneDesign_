using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public GameObject muzzle;
    public GameObject projectile;
    public GameObject aimMarker;
    public float distance = 100.0f;
    public float fireRate = 2.0f;
    public float attackCoolTime = 2.0f;
    public AudioClip[] fireSounds;
    public GameObject deadEffect;

    private Player _player;
    private LineRenderer _lineRenderer;
    private AudioSource _audioSource;
    private float _fireTime = 0.0f;
    private Vector3 _dir;

    private RaycastHit _hit;

    void Fire()
    {
        GameObject _projectile = Instantiate(projectile, muzzle.transform.position, muzzle.transform.rotation);
        //_projectile.GetComponent<Rigidbody>().velocity = _dir * _projectile.GetComponent<Projectile>().speed;
        _projectile.GetComponent<Projectile>().SetDirection(_dir);
        int temp = Random.Range(0, 3);
        _audioSource.PlayOneShot(fireSounds[temp]);
    }

    void Awake()
    {
        _lineRenderer = muzzle.GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        muzzle.transform.LookAt(_player.transform.position);
        _dir = (_player.transform.position - muzzle.transform.position).normalized;
        // 카메라와 적들은 Ray의 감지대상에서 제외
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy")) - (1 << LayerMask.NameToLayer("Camera")); 
        if (Physics.Raycast(muzzle.transform.position, _dir, out _hit, distance, layerMask))
        {
            if (_hit.collider.gameObject.CompareTag("Player"))
            {
                aimMarker.SetActive(true);
                aimMarker.transform.position = _hit.point;
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, muzzle.transform.position);
                _lineRenderer.SetPosition(1, _hit.point);
                _lineRenderer.startWidth = _fireTime * 0.5f;
                //Debug.Log("Player Detected!");
                if (_fireTime >= fireRate)
                {
                    aimMarker.SetActive(false);
                    //Debug.Log("shoot");
                    Fire();
                    //_lineRenderer.enabled = false;
                    _fireTime = 0.0f;
                }
                else
                {
                    //aimMarker.SetActive(true);
                    _fireTime += Time.deltaTime;
                }
            }
        }
        else
        {
            // 플레이어가 Ray에 걸리지 않으면 모두 초기화
            aimMarker.SetActive(false);
            _lineRenderer.enabled = false;
            _fireTime = 0.0f;
        }
        
    }
}
