using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bola;
    private MovimientoJugador movimientoJugador;

    private void Start()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        if (movimientoJugador == null)
        {
            return;
        }

        Vector3 direccion = movimientoJugador.MirandoDerecha ? Vector3.right : Vector3.left;
        GameObject bala = Instantiate(bola, controladorDisparo.position, controladorDisparo.rotation);
        Bala balaComponent = bala.GetComponent<Bala>();

        if (balaComponent != null)
        {
            balaComponent.SetDireccion(direccion);
        }
    }
}
