using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarticos : MonoBehaviour
{
    public ParticleSystem laser;

    private void Awake()
    {
        laser = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    public void Laser()
    {
        laser.Play();
    }
}
