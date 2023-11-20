using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI coinText,levelTimer,statisticTime,coinStatistic,loseCoin,LoseTime;
    public GameObject levelStatistic;
    private float elapsedTime;
    string formattedTime;
    public int coins = 0;
    public int totalCoin;
    public float totalTime;

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
       
        //Debug.Log(PlayerPrefs.GetString("LastTime"));
        UpdateTimerText();
 
    }
    void UpdateTimerText()
    {
       
        
        if (levelStatistic.activeSelf)
        {
           

            statisticTime.text = formattedTime;
            coinStatistic.text = coins.ToString();
            

            return;
        }
        else
        {
            elapsedTime += Time.deltaTime;
           // Debug.Log("UpdateTimerText() work ");
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

    public void SetCoinTimeToZero()
    {
        totalCoin += coins;
        totalTime += elapsedTime;
        PlayerPrefs.SetInt("TotalCoin", totalCoin);
        PlayerPrefs.SetFloat("TotalTime", totalTime);
        elapsedTime = 0;
        coins = 0;
    }

    public void Lose(GameObject _lose)
    {
        
        _lose.SetActive(true);
        loseCoin.text = PlayerPrefs.GetInt("TotalCoin").ToString();
        string loseTime= FormatTime(PlayerPrefs.GetFloat("TotalTime"));
        LoseTime.text = loseTime;

    }

    public void LoadSceneMode(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
}
