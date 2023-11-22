using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider musicSlider, sfxSlider;
    public Image ofSound, ofMusic;
    public TextMeshProUGUI coinText,levelTimer,statisticTime,coinStatistic,loseCoin,LoseTime,winCoin,winTime;
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
        AudioManager.instance.PlayMusic("enemy1");
        elapsedTime = 0f;
    }
    public void ToggleMusic()
    {
        ofMusic.enabled = !ofMusic.enabled;
        AudioManager.instance.ToggleMusic();
        if (ofMusic.enabled)
        {
            musicSlider.value = 0f;
        }
        else
        {
            musicSlider.value = 0.1f;
        }
        
    }
    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
        ofSound.enabled = !ofSound.enabled;
        if (ofSound.enabled)
        {
            sfxSlider.value = 0f;
        }
        else
        {
            sfxSlider.value = 0.1f;
        }
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }
    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(sfxSlider.value);
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
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlaySFX("lose");
        _lose.SetActive(true);
        SetCoinTimeToZero();
        loseCoin.text = PlayerPrefs.GetInt("TotalCoin").ToString();
        string loseTime= FormatTime(PlayerPrefs.GetFloat("TotalTime"));
        LoseTime.text = loseTime;

    }
     public void Win(GameObject _win)
    {
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlaySFX("win");
        _win.SetActive(true);
        SetCoinTimeToZero();
        winCoin.text = PlayerPrefs.GetInt("TotalCoin").ToString();
        string wintime = FormatTime(PlayerPrefs.GetFloat("TotalTime"));
        winTime.text = wintime;

    }

    public void LoadSceneMode(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
}
