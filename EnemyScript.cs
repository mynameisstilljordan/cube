using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    float speed;
    float health;
    // Start is called before the first frame update
    void Start(){
        speed = 5f;//5.0f - 0.03f * GameHandlerScript.score; //set the speed
        //health = 1 + (int)(GameHandlerScript.score % 10); //set health to increase by 1 every 10 points 
        Debug.Log(health);
    }

    private void Update()
    {
        if (health <= 0) Destroy(this.gameObject);
    }

    void FixedUpdate(){
        transform.Translate(speed * Time.deltaTime, 0f, 0f); //constantly move in the chosen direction
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D other){
        //if the colliding object has a wall tag
        if (other.gameObject.tag == "r.wall" || other.gameObject.CompareTag("l.wall"))
        {
            health--;
            speed = -speed;
        }
        //if the colliding object has the player tag
        if (other.gameObject.tag == "Player"){
            transform.GetChild(0).parent = null; //detatch the particle from self
            Destroy(this.gameObject);
        }
    }
}
