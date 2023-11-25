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
    static int allCoin;
    void Start()
    {
        testMode = true;
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
           // GetAllCoin();

           // coin = PlayerPrefs.GetInt("AllCoin");
            coin = PlayerPrefs.GetInt("TotalCoin")+ PlayerPrefs.GetInt("AllCoin");
            coinText.text = coin.ToString();
             PlayerPrefs.SetInt("AllCoin",coin);
        }

        /* else
         {
             coin = 0;
         }*/


        timer = 0f;
           level = 1;
           GameAnalytics.Initialize();
    }

    public void GetAllCoin()
    {
        allCoin = PlayerPrefs.GetInt("AllCoin");
        allCoin += int.Parse(coinText.text);
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

    private void OnApplicationQuit()
    {
        if(PlayerPrefs.HasKey("TotalCoin"))
            PlayerPrefs.SetInt("TotalCoin", 0);

        Debug.Log("Application Quit");
    }

}
