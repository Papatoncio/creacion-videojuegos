using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessages : MonoBehaviour
{
    private TextMeshProUGUI missionMessage;
    private TextMeshProUGUI adviceMessage;
    private float messageTime = 4f;
    private float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (missionMessage == null)
        {
            missionMessage = GameObject.Find("MissionText").GetComponent<TextMeshProUGUI>();
        }

        if (adviceMessage == null)
        {
            adviceMessage = GameObject.Find("AdviceText").GetComponent<TextMeshProUGUI>();
        }

        ShowMissionMessage();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        CheckMessageTime();
    }

    public void ShowMissionMessage()
    {
        timePassed = 0;
        gameObject.SetActive(true);
        missionMessage.gameObject.SetActive(true);
        adviceMessage.gameObject.SetActive(false);
    }

    public void ShowAdviceMessage() {
        timePassed = 0;
        gameObject.SetActive(true);
        adviceMessage.gameObject.SetActive(true);
        missionMessage.gameObject.SetActive(false);
    }

    private void CheckMessageTime()
    {
        UnityEngine.Debug.Log(timePassed.ToString());

        if (timePassed >= messageTime)
        {
            gameObject.SetActive(false);

            if (missionMessage.IsActive())
            {
                missionMessage.gameObject.SetActive(false);
            }
            else if (adviceMessage.IsActive())
            {
                adviceMessage.gameObject.SetActive(false);
            }
        }
    }
}
