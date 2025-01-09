using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class playerScript : MonoBehaviour
{
    Rigidbody2D rb; //box collider 2d
    SpriteRenderer sp;
    [SerializeField] Color col;
    float gravityScale = 2f;
    float jumpForce = 0.06f;
    public char gravityDirection = 'd'; //this is the current direction of gravity
    public bool isPlayerGrounded = true; //this variable determines whether or not the player is grounded
    public bool hasPlayerChangedDirection = false; //this variable states whether the player has changed direction (atleast once) during the current jump
    public iTween.EaseType easeType;
    public iTween.LoopType loopType;
    CameraScript cs;
    GameHandlerScript gHS;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>(); //getting the box collider
        sp = GetComponent<SpriteRenderer>(); //getting the spriterenderer
        cs = GameObject.FindGameObjectWithTag("CameraHandler").GetComponent<CameraScript>();
        gHS = GameObject.FindGameObjectWithTag("gameHandler").GetComponent<GameHandlerScript>(); //find the script attatched to the gamehandler object
    }

    // Update is called once per frame
    void Update(){
        if (gameObject.transform.rotation.eulerAngles.z == 180) gameObject.transform.rotation = Quaternion.identity;

        if (gravityDirection == 'd') //if the gravity is going downwards
            rb.gravityScale = gravityScale; //set the gravity to the positive gravityscale value
        else if (gravityDirection == 'u') //if the gravity is going upwards
            rb.gravityScale = -gravityScale; //set the gravity to the negative gravityscale value

        var count = Input.touchCount;

        if (count == 1)
        {
            for (int i = 0; i < count; i++)
            {
                var touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    JumpButtonPressed();
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {

            JumpButtonPressed();
        }
        col.a = 0.1f * gHS.timer;
        sp.material.color = col;
    }

    private void OnCollisionEnter2D(Collision2D other)
    { //on collision enter method
        if (other.gameObject.tag == "ceiling" || other.gameObject.tag == "floor")
        {
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            transform.rotation = Quaternion.identity;
            MMVibrationManager.Haptic(HapticTypes.MediumImpact); 
            //gHS.squash.PlayFeedbacks();
            cs.Shake("med");
            SoundManagerScript.PlaySound("step");
            isPlayerGrounded = true; //allow the player to jump when touching
            hasPlayerChangedDirection = false;
            
            if (other.gameObject.tag == "ceiling") gravityDirection = 'u'; //if touching the ceiling, switch the gravity direction to the ceiling
            if (other.gameObject.tag == "floor") gravityDirection = 'd'; //if touching the floor, switch the gravity direction to the floor
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "ceiling" || other.gameObject.tag == "floor") transform.rotation = Quaternion.identity;
    }

    private void OnCollisionExit2D(Collision2D other){ //on collision exit method
        if (other.gameObject.tag == "ceiling" || other.gameObject.tag == "floor")
        { //if the colliding object is the ceiling or the floor
            isPlayerGrounded = false; //set the boolean back to false when player leaves the ground or ceiling
        }
    }

    public void JumpButtonPressed()
    {
        if (!isPlayerGrounded)
        { //if the player isnt grounded
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            if (gravityDirection == 'd') //if the gravity direction is down
                gravityDirection = 'u'; //switch it to up
            else gravityDirection = 'd'; //otherwise, switch the direction to down
            spin(gravityDirection);
            hasPlayerChangedDirection = true;
            SoundManagerScript.PlaySound("swoosh");
            gHS.squash.PlayFeedbacks();
        }
        else
        { //if the player is grounded
            if (gravityDirection == 'd') //if the gravity direction is down
                rb.AddForce(Vector2.up * jumpForce);  //jump upwards
            else rb.AddForce(-Vector2.up * jumpForce); //other wise, if the gravity direction is up, jump downwards
        }
    }

    private void spin(char gravityDir){ //this method spins the cube 180 deg when called
        if (gravityDir == 'u') iTween.RotateTo(this.gameObject, iTween.Hash("z", -180, "time", 0.3f, "easetype", easeType, "looptype", loopType));
        else if (gravityDir == 'd') iTween.RotateTo(this.gameObject, iTween.Hash("z", 180, "time", 0.3f, "easetype", easeType, "looptype", loopType));
    }
}
