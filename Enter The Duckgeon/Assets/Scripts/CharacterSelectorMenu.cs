using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

public class CharacterSelectorMenu : MonoBehaviour
{
    private int index;

    [SerializeField] private Image characterImage;

    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI healthQuantity;
    [SerializeField] private TextMeshProUGUI shotSpeed;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        index = PlayerPrefs.GetInt("PlayerIndex");

        if (index > gameManager.characters.Count - 1)
        {
            index = 0;
        }

        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("PlayerIndex", index);
        characterImage.sprite = gameManager.characters[index].characterImage;
        characterName.text = gameManager.characters[index].characterName;
        healthQuantity.text = gameManager.characters[index].healthQuantity;
        shotSpeed.text = gameManager.characters[index].shotSpeed;
    }

    public void NextCharacter()
    {
        if (index == gameManager.characters.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }

        CambiarPantalla();
    }

    public void PreviousCharacter()
    {
        if (index == 0)
        {
            index = gameManager.characters.Count - 1;
        }
        else
        {
            index -= 1;
        }

        CambiarPantalla();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
