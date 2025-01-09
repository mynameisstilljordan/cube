using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionScript : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    float destroyDelay;
    float opacity;
    float drainRate;
    // Start is called before the first frame update
    void Start(){
        //drainRate = 0.02f * GameHandlerScript.score; //the rate at which the caution fades at
        destroyDelay = 3.0f - drainRate; //set the spawn delay of the caution
        opacity = destroyDelay; //set the opacity to the difference
        StartCoroutine("SpawnEnemy"); //start the coroutine to kill this gameobject
    }

    // Update is called once per frame
    void Update(){
        if (opacity > 0) opacity -= drainRate * Time.deltaTime; //drain the timer at the drainrate * the delta time
    }

    IEnumerator SpawnEnemy(){
        yield return new WaitForSeconds(destroyDelay); //wait for the spawn delay
        Instantiate(enemy, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        Destroy(this.gameObject); //destroy this gameobject
    }
}
