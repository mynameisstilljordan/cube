using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManagerScript : MonoBehaviour
{
    int highScore;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);
    }

    // Update is called once per frame
    public void SetNewHighscore(int hs)
    {
        highScore = hs;
        PlayerPrefs.SetInt("highscore", highScore);
    }

    public int GetHighscore(int current)
    {
        int newHighscore = 0;
        if (current > highScore) newHighscore = current;
        else newHighscore = highScore;
        PlayerPrefs.SetInt("highscore", newHighscore);
        return newHighscore;
    }
}
