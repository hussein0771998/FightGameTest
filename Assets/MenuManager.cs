using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject firstTimePlay,uiManager;
    public TextMeshProUGUI coinText,playerName;
    public string UserName;
    int level ;
    public TMP_InputField enterNameInputField;
    public DOTweenAnimation tweenAnimation;
    float timer;
    public Image loadingBar;
    static int coin;
    public bool testMode;
   
    void Start()
    {
        testMode = false;
        if (testMode)
            PlayerPrefs.DeleteAll();

        if (!PlayerPrefs.HasKey("FirstTimePlayer"))
        {
            
            PlayerPrefs.SetInt("FirstTimePlayer", 1);
            PlayerPrefs.Save();
            firstTimePlay.SetActive(true);
        }
        else
        {
            uiManager.SetActive(true);
            playerName.text = PlayerPrefs.GetString("userName");
        }

        if (PlayerPrefs.HasKey("TotalCoin"))
        {
            coin += PlayerPrefs.GetInt("TotalCoin");
            coinText.text = coin.ToString();
        }

        /* else
         {
             coin = 0;
         }*/


        timer = 0f;
           level = 1;
           GameAnalytics.Initialize();
    }
    private void Update()
    {
        

        if (firstTimePlay.activeSelf)
        {
            timer += Time.deltaTime;
            loadingBar.fillAmount = timer / 4f;
            if (timer >= 4)
            {

                tweenAnimation.DOPlay();
            }

        }


    }
    // Update is called once per frame
    public void Play()
    {
       /**/
       // GameAnalytics.SetCustomId(UserName);
       // GameAnalytics.NewDesignEvent("Level Reached", level);
        SceneManager.LoadScene("Game");
    }
    public void SetName()
    {
        if (string.IsNullOrEmpty(enterNameInputField.text))
            return;

        UserName = enterNameInputField.text;
        PlayerPrefs.SetString("userName", UserName);
       
        playerName.text = UserName;
        uiManager.SetActive(true);
        firstTimePlay.SetActive(false);
    }

    public void ExitGame()
    {

        Application.Quit();
    }

}
