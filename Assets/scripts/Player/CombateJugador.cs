using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{
    [SerializeField] private float vida;
    private MovimientoJugador movimientoJugador;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;
    [SerializeField] private BarraDeVida barraDeVida; 

    private void Start()
    {
        movimientoJugador = GetComponent<MovimientoJugador>();
        animator = GetComponent<Animator>();

        if (barraDeVida != null)
        {
            barraDeVida.InicializarBarraDeVida(vida);
        }
    }

    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(vida);
        }

        if (vida <= 0)
        {

            Destroy(gameObject);
        }
    }

    public void TomarDa�o(float da�o, Vector2 posicion)
    {
        vida -= da�o;
        animator.SetTrigger("Golpe");
        StartCoroutine(PerderControl());
        movimientoJugador.Rebote(posicion);

        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(vida);
        }
    }

    private IEnumerator PerderControl()
    {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }
}
