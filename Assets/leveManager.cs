using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leveManager : MonoBehaviour
{
    GameObject Currentwall, _nextLevel;
    public GameObject[] Allwall ;
    public GameObject[] nextLevel;

    private void Update()
    {
        if (PlayerPrefs.GetInt("levelNumber") == 0)
        {
            Currentwall = Allwall[0];
            _nextLevel = nextLevel[0];
        }
        if (PlayerPrefs.GetInt("levelNumber") == 1)
        {
            Currentwall = Allwall[1];
            _nextLevel = nextLevel[1];
        }

    }
    public void PlayNext()
    {
        Currentwall.GetComponent<Animator>().SetBool("down", true);
        _nextLevel.SetActive(true);
    }
}
