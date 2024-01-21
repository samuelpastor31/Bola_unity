using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform esferaObjetivo;
    public float velocidadSeguimiento = 5f;

    void Update()
    {
    
        transform.LookAt(esferaObjetivo);
    }
}