using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDisparo;
    [SerializeField] private float rangoAtaque;
    [SerializeField] private float tiempoEntreDisparos;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private GameObject proyectil;

    private Transform jugador;
    private bool puedeDisparar = true;
    private bool puedeAtacar = true;
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaJugador <= rangoDeteccion)
        {
            if (distanciaJugador > rangoDisparo)
            {
                SeguirJugador();
            }
            else
            {
                if (distanciaJugador <= rangoAtaque && puedeAtacar)
                {
                    StartCoroutine(AtacarJugador());
                }
                else if (puedeDisparar)
                {
                    StartCoroutine(DispararJugador());
                }
            }
        }
    }

    private void SeguirJugador()
    {
        Vector2 direccion = (jugador.position - transform.position).normalized;
        rb.velocity = new Vector2(direccion.x * velocidadMovimiento, rb.velocity.y);

        if ((direccion.x > 0 && transform.localScale.x < 0) || (direccion.x < 0 && transform.localScale.x > 0))
        {
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }

        animator.SetBool("Correr", true);
    }

    private IEnumerator DispararJugador()
    {
        puedeDisparar = false;
        animator.SetTrigger("disparo");
        yield return new WaitForSeconds(0.5f);
        GameObject nuevoProyectil = Instantiate(proyectil, puntoDisparo.position, Quaternion.identity);
        Vector2 direccionProyectil = (jugador.position - puntoDisparo.position).normalized;
        nuevoProyectil.GetComponent<Rigidbody2D>().velocity = direccionProyectil * velocidadMovimiento;

        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }

    private IEnumerator AtacarJugador()
    {
        puedeAtacar = false;
        animator.SetTrigger("Golpe");
        yield return new WaitForSeconds(0.5f); 

        if (Vector2.Distance(transform.position, jugador.position) <= rangoAtaque)
        {
            Debug.Log("Jugador atacado");
        }

        yield return new WaitForSeconds(tiempoEntreAtaques);
        puedeAtacar = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}
