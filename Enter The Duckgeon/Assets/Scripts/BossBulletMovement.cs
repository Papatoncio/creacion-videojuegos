using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletMovement : MonoBehaviour
{
    public float bulletSpeed;
    public float liveTime;
    public float passedTime;
    public Transform[] bulletSpawnPositions;
    public float angle = 0f;
    public GameObject smallBullet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckBulletLiveTime();
    }

    public void ShotBullet(Transform firePoint, float lookAngle)
    {
        transform.position = firePoint.position;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        transform.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
    }

    void CheckBulletLiveTime()
    {
        if (passedTime >= liveTime)
        {
            DivideBullet();
            passedTime = 0;
        }

        if (passedTime < liveTime)
        {
            passedTime += Time.deltaTime;
        }
    }

    void DestroyBullet()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si colisiona con "Border"
        if (other.gameObject.CompareTag("Border"))
        {
            DivideBullet();
        }
    }

    public void DivideBullet()
    {
        foreach (Transform pos in bulletSpawnPositions)
        {
            GameObject bulletClone = Instantiate(smallBullet);
            EnemyBulletMovement bulletScript = bulletClone.GetComponent<EnemyBulletMovement>();

            if (bulletScript != null)
            {
                bulletScript.ShotBullet(pos, angle);
                angle += 90;
            }
        }

        DestroyBullet();
    }
}
