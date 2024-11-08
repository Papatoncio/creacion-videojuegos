using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    private void Awake()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");

        Instantiate(GameManager.Instance.characters[playerIndex].playableCharacter, transform.position, Quaternion.Euler(0, -180, 0));
    }
}
