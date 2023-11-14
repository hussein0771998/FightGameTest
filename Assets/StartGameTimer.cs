using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image timerIcon;
    public float timer = 3f;
    void Start()
    {
        
    }

    private void Update()
    {
        
        timer -= Time.deltaTime;
        timerIcon.fillAmount = timer / 3f;
        //timer = (int)timer;
        int roundedTimer = Mathf.RoundToInt(timer);
        timerText.text = roundedTimer.ToString();
        
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

}
