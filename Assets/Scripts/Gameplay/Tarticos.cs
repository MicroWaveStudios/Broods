using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarticos : MonoBehaviour
{
    LaserPosition laser;

    private void Awake()
    {
        laser = this.gameObject.transform.GetChild(0).GetComponent<LaserPosition>();
    }

    public IEnumerator Laser()
    {
        laser.AtirarLaser();

        yield return new WaitForSeconds(1f);

        laser.ResetPosition(this.gameObject);


    }
}
