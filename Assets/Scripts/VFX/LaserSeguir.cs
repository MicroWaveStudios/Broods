using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.VFX;

public class LaserSeguir : MonoBehaviour
{
    public GameObject objSeguir;
    [SerializeField] bool treinamento;
    GameObject outroPlayer;

    Vector3 positionSeguir;
    Vector3 originalLocalScale;

    private void Start()
    {
        originalLocalScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
            if (treinamento)
            {
                objSeguir = outroPlayer.transform.gameObject;
            }
            else
            {                   
                objSeguir = outroPlayer.transform.GetChild(2).gameObject;
            }

            transform.localScale = new Vector3(originalLocalScale.x, originalLocalScale.y, originalLocalScale.z * transform.parent.transform.localScale.z);

            //positionSeguir = new Vector3(objSeguir.transform.position.x * transform.parent.transform.localScale.z,
            //                             objSeguir.transform.position.y, 
            //                             objSeguir.transform.position.z);

            if(outroPlayer.GetComponent<PlayerStats>().GetXimas() == true)
            {
                objSeguir.transform.position = new Vector3(objSeguir.transform.position.x, objSeguir.transform.position.y + 1.3f, objSeguir.transform.position.z);
            }

            transform.LookAt(objSeguir.transform.position);
        }
    }
}
