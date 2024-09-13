using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class LaserSeguir : MonoBehaviour
{
    GameObject objSeguir;

    GameObject outroPlayer;

    //[SerializeField] VisualEffect vfxLaser;

    //Vector3 posicaoOlhar;
    //Vector3 distancia;

    //float size;

    private void Awake()
    {
        if (transform.parent.gameObject.CompareTag("Player1"))
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player1");
        }

        //vfxLaser = GetComponent<VisualEffect>();

        objSeguir = outroPlayer.transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (transform.parent.gameObject.CompareTag("Player1"))
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player2");
        }
        else
        {
            outroPlayer = GameObject.FindGameObjectWithTag("Player1");
        }

        if (objSeguir != null)
        {
            //posicaoOlhar = new Vector3(objSeguir.transform.position.x, objSeguir.transform.position.y, objSeguir.transform.position.z);

            //distancia = new Vector3(transform.position.x - objSeguir.transform.position.x, transform.position.y - objSeguir.transform.position.y, transform.position.z - objSeguir.transform.position.z);

            //vfxLaser.SetFloat("Size", size);

            transform.LookAt(objSeguir.transform.position);

        }
    }
}
