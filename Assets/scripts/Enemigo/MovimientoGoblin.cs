using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoGoblin : MonoBehaviour
{

    [SerializeField] private float velocidad;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecha;
    private Rigidbody2D rigidbody2;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo= Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        rigidbody2.velocity=new Vector2(velocidad, rigidbody2.velocity.y);

        if(informacionSuelo ==false )
        {
            Girar();
        }
    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3 (0f,transform.eulerAngles.y + 180,0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);
    }
}
