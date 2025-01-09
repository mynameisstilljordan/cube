using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MoreMountains;

public class EndGameScript : MonoBehaviour
{
    [SerializeField] Canvas endgameCanvas;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    SettingsManagerScript sMS;
    MoreMountains.Feedbacks.MMTimeManager tM;

    int score;
    int highScore;
    int scoreCounter;
    // Start is called before the first frame update
    void Start()
    {
        sMS = GameObject.FindGameObjectWithTag("SettingsManager").GetComponent<SettingsManagerScript>();
        tM = GameObject.FindGameObjectWithTag("gameHandler").GetComponent<MoreMountains.Feedbacks.MMTimeManager>();
        endgameCanvas.enabled = false; //disable canvas by default
    }

    public void GameOver(int s)
    {
        tM.NormalTimescale  = 0f;
        endgameCanvas.enabled = true;
        score = s;
        scoreText.text = "Score: "+score.ToString();
        highScore = sMS.GetHighscore(score);
        highScoreText.text = "Best: " + highScore.ToString();
    }

    public void OnNextButtonClicked()
    {
        SoundManagerScript.PlaySound("menuSelection");
        SceneManager.LoadScene(sceneName: "menu");
    }
}
