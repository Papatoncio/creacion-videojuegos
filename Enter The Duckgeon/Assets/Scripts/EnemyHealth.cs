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
    private SpawnTeleportFlag spawnTeleportFlag;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerHealth>();
        spawnTeleportFlag = GameObject.Find("TeleportFlag").GetComponent<SpawnTeleportFlag>();
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
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        addToScore();
        addToKillCount();
        SpawnReward();
        Destroy(gameObject);
    }

    private void SpawnReward()
    {
        float[] probabilities = { 91f, 3f, 3f, 3f };

        float randomValue = Random.Range(0f, 100f);

        float cumulativeProbability = 0f;
        int selectedIndex = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (randomValue <= cumulativeProbability)
            {
                selectedIndex = i;
                break;
            }
        }

        GameObject reward = Instantiate(objects[selectedIndex]);
        reward.transform.position = gameObject.transform.position;
        reward.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void addToScore()
    {
        playerScript.addToScore(maxPoints);
    }

    private void addToKillCount()
    {
        if (spawnTeleportFlag != null)
        {
            spawnTeleportFlag.addKillToCount();
        }
    }
}
