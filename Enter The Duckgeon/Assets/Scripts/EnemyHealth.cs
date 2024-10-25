using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public int smallDamage;
    public int largeDamage;

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

        // Si la salud llega a cero, puedes hacer algo como mostrar la pantalla de Game Over, etc.
        if (health <= 0)
        {
            // Lógica de muerte del jugador (reiniciar nivel, mostrar pantalla de Game Over, etc.)
            UnityEngine.Debug.Log("Enemigo ha muerto");
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        Destroy(gameObject);
    }
}
