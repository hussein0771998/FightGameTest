using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using TMPro;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string UserName;
    int level ;
    public TMP_InputField enterNameInputField;
   
    void Start()
    {
        level = 1;
       GameAnalytics.Initialize();
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
