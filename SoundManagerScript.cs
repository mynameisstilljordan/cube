using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip step, hit, powerHit, swoosh, bounce, menuSelection;
    static AudioSource audioSrc;
    public static GameObject instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); //dont destroy the gameobject on load
        if (instance == null) instance = this.gameObject; //if there isnt an existing instance of the gameobject
        else Destroy(this.gameObject); //destroy the gameobject
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        hit = Resources.Load<AudioClip>("hit");
        powerHit = Resources.Load<AudioClip>("powerHit");
        swoosh = Resources.Load<AudioClip>("swoosh");
        bounce = Resources.Load<AudioClip>("bouce");
        step = Resources.Load<AudioClip>("step");
        menuSelection = Resources.Load<AudioClip>("menuSelection");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "hit":
                audioSrc.PlayOneShot(hit);
                break;
            case "powerHit":
                audioSrc.PlayOneShot(powerHit);
                break;
            case "swoosh":
                audioSrc.PlayOneShot(swoosh);
                break;
            case "bounce":
                audioSrc.PlayOneShot(bounce);
                break;
            case "menuSelection":
                audioSrc.PlayOneShot(menuSelection);
                break;
            case "step":
                audioSrc.PlayOneShot(step);
                break;
        }
    }
}
