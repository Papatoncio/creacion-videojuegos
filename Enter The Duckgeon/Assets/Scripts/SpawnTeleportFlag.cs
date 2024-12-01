using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTeleportFlag : MonoBehaviour
{
    public int killsNeeded;
    public int killCount;
    public Transform teleportPoint;
    private ShowMessages messagesScript;

    // Start is called before the first frame update
    void Start()
    {        
        messagesScript = GameObject.Find("MessagesPanel").GetComponent<ShowMessages>();
        Invoke("DisableTeleportFlag", 5);
    }

    void DisableTeleportFlag()
    {
        gameObject.SetActive(false);
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableTeleportFlag()
    {
        gameObject.SetActive(true);
    }

    public void addKillToCount()
    {
        killCount++;

        if (killCount >= killsNeeded)
        {
            EnableTeleportFlag();
            messagesScript.ShowTeleportFlagMessage();
        }
    }
}
