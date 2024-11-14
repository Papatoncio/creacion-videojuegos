using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnWinFlag : MonoBehaviour
{

    private GameObject player;
    private PlayerHealth playerScript;
    private GameObject messagesPanel;
    private ShowMessages messagesScript;
    private CircleCollider2D circleCollider;
    private bool isSpawned = false;

    void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerHealth>();
        messagesScript = GameObject.Find("MessagesPanel").GetComponent<ShowMessages>();
    }

    void Update()
    {
        CheckTimePassed();
    }

    private void CheckTimePassed()
    {
        float timePassed = playerScript.GetTime();
        if (timePassed > 60 && !isSpawned) {
            isSpawned = true;
            messagesScript.ShowAdviceMessage();
            circleCollider.enabled = true;
        }
    }
    private GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
