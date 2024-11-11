using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames.Image;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public int smallDamage;
    public int largeDamage;
    public float cokeEffect;

    private AudioSource deathSound;
    private PlayerMovent movementScript;
    private TextMeshProUGUI coinsLabel;
    private TextMeshProUGUI scoreLabel;
    private TextMeshProUGUI timerLabel;
    private int coins = 0;
    private int score = 0;
    private float timePassed = 0;

    void Start()
    {
        hearts = new Image[10];

        for (int i = 0; i < hearts.Length; i++)
        {
            // Buscar el GameObject por nombre
            string heartName = "Heart" + i; // Construye el nombre dinámicamente
            GameObject heartObject = GameObject.Find(heartName);

            if (heartObject != null)
            {
                // Obtener el componente Image y agregarlo al array
                Image heartImage = heartObject.GetComponent<Image>();

                if (heartImage != null)
                {
                    hearts[i] = heartImage;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        if (deathSound == null)
        {
            deathSound = GameObject.Find("DeathShot").GetComponent<AudioSource>();
        }

        if (movementScript == null)
        {
            movementScript = gameObject.GetComponent<PlayerMovent>();
        }

        if (coinsLabel == null)
        {
            coinsLabel = GameObject.Find("CoinsLabel").GetComponent<TextMeshProUGUI>();
            coinsLabel.text = coins.ToString();
        }

        if (scoreLabel == null)
        {
            scoreLabel = GameObject.Find("ScoreLabel").GetComponent<TextMeshProUGUI>();
            scoreLabel.text = score.ToString();
        }

        if (timerLabel == null)
        {
            timerLabel = GameObject.Find("TimerLabel").GetComponent<TextMeshProUGUI>();
            timerLabel.text = timePassed.ToString();
        }
    }

    void Update()
    {
        UpdateHealth();
        updateTimer();
        
    }

    // Detecta las colisiones con los objetos usando OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si colisiona con una "Apple"
        if (other.gameObject.CompareTag("Apple"))
        {
            // Recupera 1 punto de vida
            health = Mathf.Min(health + 1, numOfHearts); // Asegura que no se exceda el máximo de vida
            Destroy(other.gameObject); // Destruye la manzana
        }

        // Detecta si colisiona con una "GoldenApple"
        else if (other.gameObject.CompareTag("GoldenApple"))
        {
            // Aumenta el número de corazones en 1
            numOfHearts++;
            health = numOfHearts; // Restaura la vida al máximo
            Destroy(other.gameObject); // Destruye la Golden Apple
        }

        // Detecta si colisiona con una "Coke"
        else if (other.gameObject.CompareTag("Coke"))
        {
            // Reduce el cooldown de los disparos
            movementScript.attackDelay -= cokeEffect;
            movementScript.attackDelay = Mathf.Max(0, movementScript.attackDelay); // Asegura que attackDelay no sea negativo
            Destroy(other.gameObject); // Destruye la bala
        }

        // Detecta si colisiona con una "Coin"
        else if (other.gameObject.CompareTag("Coin"))
        {
            // Recoje una moneda y la suma a la cantidad de monedas
            coins += 1;
            coinsLabel.text = coins.ToString();
            Destroy(other.gameObject); // Destruye la moneda
        }

        // Detecta si colisiona con una "LargeEnemyBullet"
        else if (other.gameObject.CompareTag("LargeEnemyBullet"))
        {
            // Hace la cantidad de daño de largeDamage
            health -= largeDamage;
            health = Mathf.Max(0, health); // Asegura que la salud no sea negativa
            Destroy(other.gameObject); // Destruye la bala
        }

        // Detecta si colisiona con una "SmallEnemyBullet"
        else if (other.gameObject.CompareTag("SmallEnemyBullet"))
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
            UnityEngine.Debug.Log("Jugador ha muerto");
            UpdateHealth();
            PlayerDied();
        }
    }

    private void UpdateHealth()
    {
        // Limita la salud al número máximo de corazones
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        // Actualiza la UI de corazones
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void PlayerDied()
    {
        deathSound.Play();
        LevelManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    private void updateTimer()
    {
        timePassed += Time.deltaTime;
        float minutes = Mathf.FloorToInt(timePassed / 60);
        float seconds = Mathf.FloorToInt(timePassed % 60);

        timerLabel.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void addToScore(int maxPoints)
    {
        // Ajuste por tiempo
        float timePenaltyMultiplier = 1f;
        if (timePassed >= 60f && timePassed < 150f) // Entre 1 y 2.5 minutos
        {
            timePenaltyMultiplier = 0.8f; // Reduce los puntos en un 20%
        }
        else if (timePassed >= 150f) // Más de 2.5 minutos
        {
            timePenaltyMultiplier = 0.5f; // Reduce los puntos en un 50%
        }

        // Cálculo de puntos con penalización por tiempo
        float adjustedPoints = maxPoints * timePenaltyMultiplier;

        // Bonificación por monedas
        float coinMultiplier = 1 + (coins * 0.01f); // Aumenta puntos en un 1% por cada moneda
        adjustedPoints *= coinMultiplier;

        // Sumar puntos al puntaje total
        score += Mathf.RoundToInt(adjustedPoints);
        scoreLabel.text = score.ToString();
    }
}
