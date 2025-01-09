using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using MoreMountains.Feedbacks;

public class GameHandlerScript : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText; //the score text
    public GameObject coin; //the coin gameobject
    public float timer = 10f;
    public float maxTimer = 10f;
    public int combo;
    int xSpawn, ySpawn;
    public float horizontalCoinSpeed = 2.5f;
    public float verticalCoinSpeed = 2.5f;
    [SerializeField] Slider slider;
    public MMFeedbacks ballHit;
    public MMFeedbacks explosion;
    public MMFeedbacks squash;
    public MMFeedbacks addScore;
    EndGameScript eGS;

    private void Awake()
    {
        eGS = this.gameObject.GetComponent<EndGameScript>();


    }

    // Update is called once per frame
    void Update()
    {
        if (score > 9) scoreText.text = score.ToString(); //if score is less than 10
        else scoreText.text = "0" + score.ToString(); //add 0 before the number

        slider.maxValue = maxTimer;
        if (timer > maxTimer) timer = maxTimer; //cap the timer

        if (timer > 0) timer -= 1f * Time.deltaTime; //decrement the timer 
        slider.value = timer; //set the slider value to the timer 

        if (timer < 0) eGS.GameOver(score);
    }

    //this method spawns a coin at a random location
    public void SpawnCoin()
    {
        Instantiate(coin, new Vector2(xSpawn, ySpawn), Quaternion.identity); //spawn the coin
    }
}