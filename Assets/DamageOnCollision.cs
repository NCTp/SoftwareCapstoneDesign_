using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public float damageAmount = 10f;
    public float speed = 10.0f;

    void Update()
    {
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 충돌한 오브젝트에서 IDamageable 인터페이스를 찾습니다.
            Player player = collision.gameObject.GetComponent<Player>();

            // IDamageable 인터페이스를 구현한 오브젝트가 있다면 데미지를 줍니다.
            if (player != null)
            {
                player.GetDamage(new DamageMessage(gameObject, damageAmount));
            }
            // 자신을 파괴합니다.
            Destroy(gameObject);
        }
    }
}
