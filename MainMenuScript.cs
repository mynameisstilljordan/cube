using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour{

    [SerializeField] TMP_Text subheader;
    string[] messages = new string[]
    {
        "You can't disprove it's 3d if you can only see one side of it!",
        "The new mobile adaptation!",
        "Unlock more colors as you progress!",
        "You can change gravity in mid-air by tapping the screen once airbourne!",
        "Heavily inspired by the original CUBE released in 2019!"
    };

    string displayMessage; //the variable for holding which message is to be displayed

    private void Start()
    {
        displayMessage = messages[Random.Range(0,messages.Length)]; //select a random message to display
        subheader.text = displayMessage; //give the value of the string to the subheader 
    }

    //this method is called when the play button is pressed
    public void OnPlayButtonPressed(){
        SoundManagerScript.PlaySound("menuSelection");
        SceneManager.LoadScene(sceneName: "ingame");
    }

    //this method is called when the settings button is pressed
    public void OnHelpButtonPressed(){
        SoundManagerScript.PlaySound("menuSelection");
        SceneManager.LoadScene(sceneName: "help");
    }

    //this method is called when the quit button is pressed
    public void OnQuitButtonPressed(){
        Application.Quit();
    }

    public void OnDebug()
    {
        Handheld.Vibrate(); //so android prompts user for vibration permission
    }
}
