using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class CoinScript : MonoBehaviour
{
    public float hSpeed, vSpeed;
    float hSpeedDir, vSpeedDir;
    float speedDecrement = 0f;
    float hSpeedMultiplier = 1f;
    float vSpeedMultiplier = 1f;
    float speedCap = 2f;
    float minSpeed = 1f;
    float speedCapIncrement = 0.05f;//0.015f;
    bool isPlayerTouchOnCooldown = false;
    float lastX, lastY;
    GameHandlerScript gHS;
    Rigidbody2D rb;
    public GameObject ps;
    CameraScript cs;
    public GameObject FloatingText;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CheckPosition"); //the first call of the position checking coroutine 
        gHS = GameObject.FindGameObjectWithTag("gameHandler").GetComponent<GameHandlerScript>(); //find the script attatched to the gamehandler object
        cs = GameObject.FindGameObjectWithTag("CameraHandler").GetComponent<CameraScript>(); //getting the instance of the camerahandler component
        hSpeedDir = Random.Range(0f, 1f); if (hSpeedDir <= 0.5) hSpeed = -hSpeed; //negate the speed direction through the result of a coin flip
        vSpeedDir = Random.Range(0f, 1f); if (vSpeedDir <= 0.5) vSpeed = -vSpeed; //negate the speed direction through the result of a coin flip
        rb = GetComponent<Rigidbody2D>(); //getting the instance of the rigidbody2d component
        speedDecrement = speedCap / 5f; //the decrement value of the ball (lose speedcap/5 speed after every wall collision)
        hSpeed = gHS.horizontalCoinSpeed; //the horizontal speed of the ball
        vSpeed = gHS.verticalCoinSpeed; //the vertical speed of the ball
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(hSpeed, vSpeed * vSpeedMultiplier); //the velocity of the ball
    }

    //coroutine for checking the position of the ball
    IEnumerator CheckPosition()
    {
        lastX = transform.position.x; //save the current location of the ball's x
        lastY = transform.position.y; //save the current location of the ball's y
        yield return new WaitForSeconds(0.1f); //the idle time required for ball to be flagged as stuck
        if (transform.position.x == lastX || transform.position.y == lastY) //if the ball has the same x or y coordiate after 1 second
            ResetPosition(); //call the reset position method
        StartCoroutine("CheckPosition"); //recall the coroutine
    }

    //the coroutine for player cooldowns
    IEnumerator PlayerCooldown()
    {
        yield return new WaitForSeconds(0.2f); //the cooldown timer
        isPlayerTouchOnCooldown = false; //reset the variable
    }

    //for collisions
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the colliding object has a wall tag
        if (other.gameObject.CompareTag("r.wall") || other.gameObject.CompareTag("l.wall"))
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact); //light impact
            cs.Shake("weak"); //shake the screen (weak)
            SoundManagerScript.PlaySound("bounce"); //play the bounce sound
            hSpeed = -hSpeed; //start moving in the other direction
            if (hSpeedMultiplier > minSpeed) hSpeedMultiplier -= speedDecrement; //slowly decrement the speed multiplier whenever it increases 
            if (vSpeedMultiplier > minSpeed) vSpeedMultiplier -= speedDecrement; //slowly decrement the speed multiplier whenever it increases 
            if (hSpeedMultiplier <= minSpeed) hSpeedMultiplier = minSpeed;  //if hspeed is lower than the min, set it to the min
            if (vSpeedMultiplier <= minSpeed) vSpeedMultiplier = minSpeed; //if vspeed is lower than the min, set it to the min
        }

        //if the collision is with the ceiling or the floor
        if (other.gameObject.CompareTag("ceiling") || other.gameObject.CompareTag("floor"))
        {
            MMVibrationManager.Haptic(HapticTypes.LightImpact); //light haptic
            cs.Shake("weak"); //shake the screen (weak)
            SoundManagerScript.PlaySound("bounce"); //play the bounce sound
            vSpeed = -vSpeed; //switch the vspeed 
            if (hSpeedMultiplier > minSpeed) hSpeedMultiplier -= speedDecrement; //slowly decrement the speed multiplier whenever it increases 
            if (vSpeedMultiplier > minSpeed) vSpeedMultiplier -= speedDecrement; //slowly decrement the speed multiplier whenever it increases 
        }

        //if the colliding object has the player tag
        if (other.gameObject.CompareTag("Player")) //if the collision is with the player
        {
            gHS.squash.PlayFeedbacks(); //play the squash feedback for the player
            bool verticallyAligned = false; if (transform.position.x > -0.3f && transform.position.x < 0.3f) verticallyAligned = true; //set vertically aligned to true if the conditions were true
            float xPos = transform.position.x; //save the x pos
            float yPos = transform.position.y; //save the y pos
            float playerXPos = other.transform.position.x; //save the player x pos
            float playerYPos = other.transform.position.y; //save the player y pos

            if (!isPlayerTouchOnCooldown) //if the player is on touch cooldown
            {
                isPlayerTouchOnCooldown = true; //set the cooldown to true
                StartCoroutine("PlayerCooldown"); //set
            }
            else return;
            
            gHS.addScore.PlayFeedbacks(); //show score effect

            if (ps.GetComponent<playerScript>().isPlayerGrounded == false && verticallyAligned && ps.GetComponent<playerScript>().hasPlayerChangedDirection == true) //if the hit is a power hit
            {
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact); //heavy haptic
                cs.Shake("strong"); //shake the screen (strong)
                gHS.ballHit.PlayFeedbacks(); //register the ball hit and play the feedback effect
                gHS.combo++; //add to the combo
                gHS.score+=gHS.combo; //increment the score
                if (yPos > playerYPos) ShowFloatingText("above"); else ShowFloatingText("below");
                
                ps.GetComponent<playerScript>().JumpButtonPressed(); //register a player gravity switch
                gHS.timer += 5f; //add 5 seconds to the timer
                SoundManagerScript.PlaySound("powerHit");
                if (yPos > playerYPos) vSpeed = Mathf.Abs(vSpeed); //if the coin is moving downwards, switch to upwards
                else if (yPos < playerYPos) vSpeed = -Mathf.Abs(vSpeed); //if the coin is moving upwards, switch to downwards
                vSpeedMultiplier = speedCap; //set the mutiplier to the speed cap
            }
            
            else //if the hit was a regular hit
            {
                MMVibrationManager.Haptic(HapticTypes.MediumImpact);
                cs.Shake("med"); //shake the screen (med)
                gHS.combo = 0; //reset the combo
                gHS.score ++; //increment the score
                gHS.timer += 3f; //add 3 seconds to the timer
                SoundManagerScript.PlaySound("hit"); //play the hit sound
                if (xPos > playerXPos) hSpeed = Mathf.Abs(hSpeed); //if the coin is moving downwards, switch to upwards
                else if (xPos < playerXPos) hSpeed = -Mathf.Abs(hSpeed); //if the coin is moving upwards, switch to downwards
                if (yPos > playerYPos) vSpeed = Mathf.Abs(vSpeed); //if the coin is moving downwards, switch to upwards
                else if (yPos < playerYPos) vSpeed = -Mathf.Abs(vSpeed); //if the coin is moving upwards, switch to downwards
                hSpeedMultiplier = speedCap;
            }

            if (gHS.maxTimer > 5f) gHS.maxTimer -= 0.04f; //remove 0.02 seconds from the timer's max cap
            speedCap += speedCapIncrement; //increase the speed cap of the ball
            minSpeed = speedCap / 2f; //set the new minspeed value
            speedDecrement = speedCap / 5f; //update the speed decrement
        }
    }

    void ShowFloatingText(string alignment) //method for floating text
    {
        var fT = Instantiate(FloatingText, ps.transform.position, Quaternion.identity); //save the instantiated text to a variable
        if (alignment == "below") fT.GetComponent<FloatingTextScript>().offset.y = -fT.GetComponent<FloatingTextScript>().offset.y; //set the combo to appear below player if ball is below
        fT.GetComponent<TextMesh>().text = "x" + gHS.combo.ToString(); //set the text of the 
    }

    //this method is called when the ball gets stuck
    void ResetPosition()
    {
        gHS.timer = gHS.maxTimer; //refill the timer
        gHS.score += 5; //add 5 to the score
        transform.position = new Vector2(0f, 0f); //reset to origin point
        gHS.explosion.PlayFeedbacks(); //show explosion
        gHS.addScore.PlayFeedbacks(); //show score effect
        vSpeedMultiplier = minSpeed; //set the vspeed to minspeed
        hSpeedMultiplier = minSpeed; //set the hspeed to minspeed
    }
}