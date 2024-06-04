using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respiracion : MonoBehaviour
{
    [SerializeField] private float frecuencia = 1f; // Frecuencia de la respiración
    [SerializeField] private float amplitud = 0.1f; // Amplitud de la respiración

    private Vector3 escalaInicial;

    void Start()
    {
        escalaInicial = transform.localScale;
    }

    void Update()
    {
        float escalaY = 1 + Mathf.Sin(Time.time * frecuencia) * amplitud;
        transform.localScale = new Vector3(escalaInicial.x, escalaInicial.y * escalaY, escalaInicial.z);
    }
}
