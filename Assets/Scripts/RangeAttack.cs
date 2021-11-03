using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    public int burstSize = 3;
    public float rateOfFire = 500f;

    float bulletDelay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void Shoot()
    {
        StartCoroutine(Burst());
    }

    IEnumerator Burst()
    {
        bulletDelay = 60 / rateOfFire;

        for (int i = 0; i < burstSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            yield return new WaitForSeconds(bulletDelay);

        }

    }
}
