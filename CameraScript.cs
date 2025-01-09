using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    CameraShake.CameraShaker cs;
    [SerializeField] CameraShake.BounceShake.Params weakHit, medHit, strongHit;
    // Start is called before the first frame update

    public void Shake(string hit)
    {
        CameraShake.BounceShake.Params bsP;
        switch (hit)
        {
            case "weak":
                bsP = weakHit;
                break;
            case "med":
                bsP = medHit;
                break;
            case "strong":
                bsP = strongHit;
                break;
            default:
                bsP = medHit;
                break;
        }
        CameraShake.CameraShaker.Shake(new CameraShake.BounceShake(bsP));
    }
}
