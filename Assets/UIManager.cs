using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI coinText,levelTimer,statisticTime,coinStatistic;
    public GameObject levelStatistic;
    private float elapsedTime;
    string formattedTime;
    public int coins = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        elapsedTime = 0f;
    }

    private void Update()
    {
        coinText.text = coins.ToString();

        if (!levelTimer.gameObject.activeSelf )
            return;
        elapsedTime += Time.deltaTime;
        //Debug.Log(PlayerPrefs.GetString("LastTime"));
        UpdateTimerText();
 
    }
    void UpdateTimerText()
    {
       
        
        if (levelStatistic.activeSelf)
        {
            PlayerPrefs.SetString("LastTime", formattedTime);

            statisticTime.text = formattedTime;
            coinStatistic.text = coins.ToString();
            elapsedTime = 0;
            coinText.text = "";
            return;
        }
        else
        {
            //Debug.Log("UpdateTimerText() work ");
            // Format the time as minutes:seconds
            formattedTime = FormatTime(elapsedTime);

            // Update the TextMeshProUGUI
            levelTimer.text = "Time: " + formattedTime;
        }
    }

    string FormatTime(float timeInSeconds)
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Format as MM:SS
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        return formattedTime;
    }
}
