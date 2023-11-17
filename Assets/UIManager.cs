using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI coinText;

    public int coins = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        coinText.text = "coins : " + coins.ToString();
    }
}
