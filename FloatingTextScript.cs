using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextScript : MonoBehaviour
{
    float destroyTimer = 2f;
    public Vector3 offset = new Vector3(0, 0.6f);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTimer);
        transform.localPosition += offset;
    }
}
