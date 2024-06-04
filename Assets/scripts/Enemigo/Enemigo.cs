using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2d;
    public Transform jugador;
    private bool mirandoDerecha = true;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraDeVida;
    public bool isDefeated = false;

    [Header("Ataque Cuerpo a Cuerpo")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    [Header("Ataque a Distancia")]
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private GameObject proyectil;
    [SerializeField] private float rangoDisparo;
    [SerializeField] private float tiempoEntreDisparos;
    private bool puedeDisparar = true;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float rangoDeteccion;
    private bool puedeAtacar = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        if (barraDeVida != null)
        {
            barraDeVida.InicializarBarraDeVida(vida);
        }
        else
        {
            Debug.LogError("BarraDeVida no está asignada en el Inspector.");
        }

        jugador = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (isDefeated) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaJugador <= rangoDeteccion)
        {
            MirarJugador();

            if (distanciaJugador > rangoDisparo)
            {
                animator.SetBool("Correr", true);
            }
            else
            {
                animator.SetBool("Correr", false);
                rb2d.velocity = Vector2.zero;

                if (distanciaJugador <= radioAtaque && puedeAtacar)
                {
                    StartCoroutine(AtacarJugador());
                }
                else if (puedeDisparar)
                {
                    StartCoroutine(DispararJugador());
                }
            }
        }
        else
        {
            animator.SetBool("Correr", false);
            rb2d.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (isDefeated) return;

        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);

        if (distanciaJugador <= rangoDeteccion && distanciaJugador > rangoDisparo)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            rb2d.velocity = new Vector2(direccion.x * velocidadMovimiento, direccion.y * velocidadMovimiento);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    private IEnumerator DispararJugador()
    {
        puedeDisparar = false;
        animator.SetTrigger("disparo");
        yield return new WaitForSeconds(0.5f); // Tiempo de espera antes de disparar para sincronizar con la animación

        GameObject nuevoProyectil = Instantiate(proyectil, puntoDisparo.position, Quaternion.identity);
        Vector2 direccionProyectil = (jugador.position - puntoDisparo.position).normalized;
        nuevoProyectil.GetComponent<Rigidbody2D>().velocity = direccionProyectil * velocidadMovimiento;

        // Ignora las colisiones entre el proyectil y el enemigo
        Physics2D.IgnoreCollision(nuevoProyectil.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }

    private IEnumerator AtacarJugador()
    {
        puedeAtacar = false;
        animator.SetTrigger("Golpe");
        yield return new WaitForSeconds(0.5f); // Tiempo de espera antes de atacar para sincronizar con la animación

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<CombateJugador>().TomarDaño(dañoAtaque);
            }
        }

        yield return new WaitForSeconds(1f); // Tiempo de espera entre ataques cuerpo a cuerpo
        puedeAtacar = true;
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (barraDeVida != null)
        {
            barraDeVida.CambiarVidaActual(vida);
        }
        else
        {
            Debug.LogError("BarraDeVida no está asignada en el Inspector.");
        }

        if (vida <= 0)
        {
            animator.SetTrigger("die");
            isDefeated = true;
            StartCoroutine(Muerte()); // Llamar a la corrutina para manejar la muerte del enemigo
        }
    }

    private IEnumerator Muerte()
    {
        // Guardar el estado del enemigo derrotado y desbloquear Freezer
        GameManager.Instance.DesbloquearPersonaje("Freezer");
        GameManager.Instance.GuardarEstadoJuego();

        // Esperar a que la animación de muerte termine (ajusta el tiempo según tu animación)
        yield return new WaitForSeconds(1.5f);

        // Destruir el objeto enemigo
        Destroy(gameObject);

        // Cargar la nueva escena después de destruir el enemigo
        SceneManager.LoadScene("MenuSeleccionPersonaje");
    }

    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
