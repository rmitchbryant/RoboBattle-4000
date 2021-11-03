using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 5;

    void OnTriggerEnter(Collider other)
    {
        PlayerCombat player = other.GetComponent<PlayerCombat>();
        if (player != null)
        {
            player.PlayerDamaged(damage);
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
