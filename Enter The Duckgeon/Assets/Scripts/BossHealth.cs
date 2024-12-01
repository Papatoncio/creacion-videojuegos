using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int health;
    public int smallDamage;
    public int largeDamage;
    public int maxPoints;
    public float minDistance;

    private float distance;
    private bool isPlayerSeen = false;
    private GameObject player;
    private PlayerHealth playerScript;
    private ShowMessages messagesScript;
    private Slider bossHealthBar;
    private GameObject winFlag;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerHealth>();
        messagesScript = GameObject.Find("MessagesPanel").GetComponent<ShowMessages>();
        bossHealthBar = GameObject.Find("BossHealthBar").GetComponent<Slider>();
        winFlag = GameObject.Find("WinFlag");
        winFlag.SetActive(false);
        bossHealthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        ToggleBossHealthBar();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si colisiona con una "LargeEnemyBullet"
        if (other.gameObject.CompareTag("LargePlayerBullet"))
        {
            // Hace la cantidad de daño de largeDamage
            health -= largeDamage;
            health = Mathf.Max(0, health); // Asegura que la salud no sea negativa
            UpdateBossHealthBar();
            Destroy(other.gameObject); // Destruye la bala
        }

        // Detecta si colisiona con una "SmallEnemyBullet"
        else if (other.gameObject.CompareTag("SmallPlayerBullet"))
        {
            // Hace la cantidad de daño de smallDamage
            health -= smallDamage;
            health = Mathf.Max(0, health); // Asegura que la salud no sea negativa
            UpdateBossHealthBar();
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
        messagesScript.ShowBossKilledMessage();
        bossHealthBar.gameObject.SetActive(false);
        winFlag.SetActive(true);
        Destroy(gameObject);
    }

    private void addToScore()
    {
        playerScript.addToScore(maxPoints);
    }

    private void UpdateBossHealthBar()
    {
        if (bossHealthBar != null)
        {
            bossHealthBar.value = health;
        }
    }

    private void ToggleBossHealthBar()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= minDistance && !isPlayerSeen)
        {
            bossHealthBar.gameObject.SetActive(!bossHealthBar.IsActive());
            isPlayerSeen = true;
        }
    }
}
