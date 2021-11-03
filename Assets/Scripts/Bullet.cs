using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.x > 100 || transform.position.x < -100 || transform.position.z > 100 || transform.position.z < -100)
        {
            Destroy(gameObject);
        }
    }
}
