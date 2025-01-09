using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryScript : MonoBehaviour
{
    SpriteRenderer sr;
    Color col;
    public float alpha;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = new Color(255, 255, 255, 0);
        sr.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= 2.5f * Time.deltaTime;
        col.a = alpha;
        sr.color = col;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "coin")
        {
            alpha = .5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "coin")
        {
            alpha = .5f;
        }
    }
}
