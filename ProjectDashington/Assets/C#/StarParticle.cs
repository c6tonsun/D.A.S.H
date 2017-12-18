using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarParticle : MonoBehaviour {

    public ParticleSystem star;

    private void OnEnable()
    {
        star.Play();
    }
}
