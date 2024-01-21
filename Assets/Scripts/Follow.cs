using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target; // Objeto que seguirá este objeto
    private Vector3 offset;// Distancia relativa entre este objeto y el objeto objetivo

    // Start is called before the first frame update
    void Start()
    {
        // Calcula la distancia relativa entre este objeto y el objeto objetivo
        offset = transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Actualiza la posición de este objeto para que siga al objeto objetivo manteniendo la distancia relativa
        transform.position = target.transform.position + offset;
    }
}
