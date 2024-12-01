using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIEnemyChase : MonoBehaviour
{
    private GameObject player;
    public GameObject bullet;
    public Transform firePoint;
    public GameObject gun;
    public float speed;
    public float distanceBetween;
    public float attackDistance;
    public float attackDelay;
    public float passedTime;

    private float distance;
    private bool facingRight = true;
    private Animator animator;
    private Vector2 lookDirection;
    private float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndAttack();
    }

    void MoveAndAttack()
    {
        if (player != null)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            RotateFirePoint();

            if (distance <= distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                FlipSprite(angle);

                if (distance <= attackDistance)
                {
                    if (passedTime >= attackDelay)
                    {
                        ShotBullet();
                        passedTime = 0;
                    }
                }
            }

            animator.SetBool("IsMoving", distance <= distanceBetween);

            if (passedTime < attackDelay)
            {
                passedTime += Time.deltaTime;
            }
        }
    }

    void FlipSprite(float angle)
    {
        // Si el personaje está moviéndose hacia la izquierda y está mirando a la derecha, se voltea
        if (((angle < 180 && angle > 90.01) || (angle > -180 && angle < -90.01)) && facingRight)
        {
            Flip();
        }
        // Si el personaje está moviéndose hacia la derecha y está mirando a la izquierda, se voltea
        else if (((angle > 0 && angle < 89.99) || (angle < 0 && angle > -89.99)) && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Cambia el estado de la dirección
        facingRight = !facingRight;

        // Invertir la rotación en el eje Y para voltear el sprite
        Vector3 rotation = transform.localEulerAngles;
        rotation.y += 180f;
        transform.localEulerAngles = rotation;
    }

    void RotateFirePoint()
    {
        lookDirection = new Vector3(player.transform.position.x, player.transform.position.y) - new Vector3(transform.position.x, transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        Vector3 gunRotation = gun.transform.localEulerAngles;
        gunRotation.z = lookAngle * -1;
        if (!facingRight) gunRotation.y = 180f; else gunRotation.y = 0;
        gun.transform.localEulerAngles = gunRotation;
    }

    void ShotBullet()
    {
        GameObject bulletClone = Instantiate(bullet);
        if (gameObject.CompareTag("Boss"))
        {
            BossBulletMovement bulletScript = bulletClone.GetComponent<BossBulletMovement>();
            if (bulletScript != null)
            {
                bulletScript.ShotBullet(firePoint, lookAngle);
            }
        } else
        {
            EnemyBulletMovement bulletScript = bulletClone.GetComponent<EnemyBulletMovement>();
            if (bulletScript != null)
            {
                bulletScript.ShotBullet(firePoint, lookAngle);
            }
        }        
    }
}
