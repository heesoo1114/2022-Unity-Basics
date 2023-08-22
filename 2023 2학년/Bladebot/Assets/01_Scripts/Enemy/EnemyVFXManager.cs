using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFXManager : MonoBehaviour
{
    [SerializeField]
    private VisualEffect _footStep;

    public void PlayFootStep()
    {
        _footStep.Play();
    }


}
