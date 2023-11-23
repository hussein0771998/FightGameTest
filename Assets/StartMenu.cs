using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string UserName;
    int level ;
    public TMP_InputField enterNameInputField;
    public DOTweenAnimation tweenAnimation;
    bool playTweenOnce;
    float timer;
    public Image loadingBar;
    void Start()
    {
           timer = 0f;
           playTweenOnce = true;
           level = 1;
           GameAnalytics.Initialize();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        loadingBar.fillAmount = timer / 4f;
        if (timer >= 4)
        {
            
            tweenAnimation.DOPlay();
        }
       

    }
    // Update is called once per frame
    public void SetName_Play()
    {
        if (string.IsNullOrEmpty( enterNameInputField.text))
            return;
        
        UserName = enterNameInputField.text;
       // GameAnalytics.SetCustomId(UserName);
        GameAnalytics.NewDesignEvent("Level Reached", level);
        SceneManager.LoadScene("Game");

       
    }
}
