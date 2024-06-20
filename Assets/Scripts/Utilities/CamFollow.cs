using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class CamFollow : MonoBehaviour
{
    
    public float mouseSensitivity = 100.0f;

    public Vector3 positionOffSet;
    public Vector3 directionOffSet;
    private GameObject target;
    private Transform orientation;
    private Quaternion _targetRotation;
    private Vector3 _targetDirection;
    private RaycastHit _hitInfo;
    private float _rotationSpeed = 10.0f;
    private bool _isShaking = false;

    private float rotationX =0.0f, rotationY=0.0f;
    
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    void FollowPlayer()
    {
        if (!_isShaking)
        {
            transform.position = target.transform.position + positionOffSet;
            _targetDirection = 
                ((target.transform.position + directionOffSet) - transform.position).normalized; // 카메라 연출을 위한 OffSet 추가
        
            _targetRotation = Quaternion.LookRotation(_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed);

            int layerMask = 1 << LayerMask.NameToLayer("Wall"); // 벽만 감지하도록 LayerMask 정의
            if(Physics.Raycast(transform.position, _targetDirection, out _hitInfo, 10.0f, layerMask))
            {
                //Debug.Log("Wall Detected");
            }
        }
    }

    void FollowPlayer2()
    {
        Vector3 desiredPosition = target.transform.position + positionOffSet; // 목표 위치 설정
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f); // 부드러운 이동
        transform.position = smoothedPosition; // 카메라 위치 갱신

        transform.LookAt(target.transform);
    }

    void RotateCam()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY += mouseX * Time.deltaTime;
        rotationX -= mouseY * Time.deltaTime;
        //rotationY = Mathf.Clamp(rotationY, -120f, 120f);
        //rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0.0f);
        target.transform.eulerAngles = new Vector3(rotationX, rotationY, 0.0f);

    }

    public void CamShake(float amount, float time)
    {
        Debug.Log("CamShake");
        _isShaking = true;
        Vector3 originalOffSet = positionOffSet;
        StartCoroutine(CamShakeCoroutine(amount, time, originalOffSet));
    }

    IEnumerator CamShakeCoroutine(float amount, float time, Vector3 originalOffSet)
    {
        float timer = time;
        float _amount = amount;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            _amount -= Time.deltaTime;
            float _x = Random.Range(-1f, 1f) * amount;
            float _y = Random.Range(-1f, 1f) * amount;
            
            transform.localPosition += new Vector3(_x, _y);
            yield return null;
        }
        //Debug.Log("Shake End");
        _isShaking = false;
        if(timer <= 0) positionOffSet = originalOffSet;

    }

    public void CamZoomIn(float amount)
    {
        Camera.main.fieldOfView -= amount * Time.deltaTime;
    }

    public void CamZoomInTime(float amount, float time)
    {
        StartCoroutine(CamZoomInCoroutine(amount, time));
    }
    IEnumerator CamZoomInCoroutine(float amount, float time)
    {
        float timer = time;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            Camera.main.fieldOfView -= amount * Time.deltaTime;
            yield return null;

        }
    }

    public void CamZoomOut(float amount)
    {
        Camera.main.fieldOfView += amount * Time.deltaTime;
    }

    public void CamZoomOutTime(float amount, float time)
    {
        StartCoroutine(CamZoomOutCoroutine(amount, time));

    }
    IEnumerator CamZoomOutCoroutine(float amount, float time)
    {
        float timer = time;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            Camera.main.fieldOfView += amount * Time.deltaTime;
            yield return null;

        }
    }

    void Awake()
    {
        target = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        CamShake(0.1f, 0.2f);
        //CamZoomIn(100.0f, 0.2f);
    }

    private void LateUpdate()
    {
        FollowPlayer2();
        RotateCam();
        
    }
    

    // Update is called once per frame
    void Update()
    {


    }
}