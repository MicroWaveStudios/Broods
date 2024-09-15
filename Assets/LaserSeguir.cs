using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class LaserSeguir : MonoBehaviour
{
    public GameObject objSeguir;
    Vector3 newPosition;
    GameObject outroPlayer;


    //[SerializeField] VisualEffect vfxLaser;

    //Vector3 posicaoOlhar;
    //Vector3 distancia;

    //float size;

    private void Awake()
    {

        //vfxLaser = GetComponent<VisualEffect>();
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
        if (outroPlayer != null)
        {

            objSeguir = outroPlayer.transform.GetChild(2).gameObject;
        }
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

            newPosition = new Vector3(-1f, 1f, 1f);
            transform.LookAt(objSeguir.transform.position);

        }
    }
}
