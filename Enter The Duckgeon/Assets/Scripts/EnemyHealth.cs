using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int smallDamage;
    public int largeDamage;
    public GameObject[] objects;
    public int maxPoints;

    private GameObject player;
    private PlayerHealth playerScript;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si colisiona con una "LargeEnemyBullet"
        if (other.gameObject.CompareTag("LargePlayerBullet"))
        {
            // Hace la cantidad de daño de largeDamage
            health -= largeDamage;
            health = Mathf.Max(0, health); // Asegura que la salud no sea negativa
            Destroy(other.gameObject); // Destruye la bala
        }

        // Detecta si colisiona con una "SmallEnemyBullet"
        else if (other.gameObject.CompareTag("SmallPlayerBullet"))
        {
            // Hace la cantidad de daño de smallDamage
            health -= smallDamage;
            health = Mathf.Max(0, health); // Asegura que la salud no sea negativa
            Destroy(other.gameObject); // Destruye la bala
        }

        // Si la salud llega a cero, actualiza puntuación y destruye al enemigo.
        if (health <= 0)
        {
            UnityEngine.Debug.Log("Enemigo ha muerto");
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        addToScore();
        SpawnReward();
        Destroy(gameObject);
    }

    private void SpawnReward()
    {
        GameObject reward = Instantiate(objects[0]);
        reward.transform.position = gameObject.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void addToScore()
    {
        playerScript.addToScore(maxPoints);
    }
}
