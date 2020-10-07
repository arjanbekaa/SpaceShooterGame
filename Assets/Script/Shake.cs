using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator shakeAnim;

    public void shake()
    {
        shakeAnim.SetTrigger("CameraShake");
    }
}
