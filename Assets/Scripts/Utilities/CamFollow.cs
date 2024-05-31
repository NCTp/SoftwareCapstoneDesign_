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
    private GameObject Target;
    private Quaternion _targetRotation;
    private Vector3 _targetDirection;
    private RaycastHit _hitInfo;
    private float _rotationSpeed = 10.0f;

    private float rotationX =0.0f, rotationY=0.0f;

    void FollowPlayer()
    {
        transform.position = Target.transform.position + positionOffSet;
        _targetDirection = 
        ((Target.transform.position + directionOffSet) - transform.position).normalized; // 카메라 연출을 위한 OffSet 추가
        
        _targetRotation = Quaternion.LookRotation(_targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSpeed);

        int layerMask = 1 << LayerMask.NameToLayer("Wall"); // 벽만 감지하도록 LayerMask 정의
        if(Physics.Raycast(transform.position, _targetDirection, out _hitInfo, 10.0f, layerMask))
        {
            //Debug.Log("Wall Detected");
        }
    }

    void RotateCam()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY += mouseX * Time.deltaTime;
        rotationX -= mouseY * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -60f, 60f);
        rotationX = Mathf.Clamp(rotationX, -60f, 60f);
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0.0f);

    }

    void CamShake(float amount, float time)
    {
        Vector3 originalOffSet = directionOffSet;
        StartCoroutine(CamShakeCoroutine(amount, time, originalOffSet));
    }

    IEnumerator CamShakeCoroutine(float amount, float time, Vector3 originalOffSet)
    {
        float timer = time;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            Vector3 noise = 
            new Vector3(Random.Range(-amount, amount), Random.Range(-amount, amount), Random.Range(-amount, amount));
            directionOffSet += noise;
            yield return null;

        }
        if(timer <= 0) directionOffSet = originalOffSet;

    }

    public void CamZoomIn(float amount, float time)
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

    public void CamZoomOut(float amount, float time)
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
        Target = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        //CamShake(0.1f, 1.0f);
        //CamZoomIn(100.0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        RotateCam();
        //if(Input.GetKey(KeyCode.E)) CamZoomOut(5.0f);
        //if(Input.GetKey(KeyCode.Q)) CamZoomIn(50.0f, 5.0f);


    }
}
