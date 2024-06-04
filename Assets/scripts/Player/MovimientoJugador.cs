using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velocidadRebote;

    private float spawnBala;
    private Rigidbody2D rigidbody2;
    private Animator animator;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float suavizadoDeMovimiento;

    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform ControladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    private bool enSuelo;
    private bool salto = false;
    private int saltosRestantes;
    private bool enRebote = false;

    [Header("Animación de Respiración")]
    [SerializeField] private float frecuenciaRespiracion = 1f;
    [SerializeField] private float amplitudRespiracion = 0.1f;
    private float escalaYInicial;

    public bool MirandoDerecha => mirandoDerecha;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        saltosRestantes = 2;
        escalaYInicial = transform.localScale.y;
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        if (movimientoHorizontal > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (movimientoHorizontal < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (Input.GetButtonDown("Jump") && saltosRestantes > 0)
        {
            salto = true;
        }
        if (Input.GetKey(KeyCode.F) && Time.time > spawnBala + 0.25f)
        {
            Shoot();
            spawnBala = Time.time;
        }

        float nuevaEscalaY = escalaYInicial * (1 + Mathf.Sin(Time.time * frecuenciaRespiracion) * amplitudRespiracion);
        transform.localScale = new Vector3(transform.localScale.x, nuevaEscalaY, transform.localScale.z);
    }

    private void Shoot()
    {
        Vector3 direccion = mirandoDerecha ? Vector3.right : Vector3.left;
        Vector3 posicionBala = transform.position + direccion * 0.5f; // Ajusta la posición de instanciación


    }

    private void FixedUpdate()
    {
        rigidbody2.velocity = new Vector2(movimientoHorizontal, rigidbody2.velocity.y);
        enSuelo = Physics2D.OverlapBox(ControladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        if(sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }
        if (enSuelo)
        {
            saltosRestantes = 2;
        }
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        salto = false;
    }
    public void Rebote(Vector2 puntoGolpe)
    {
        if (!enRebote)
        {
            Vector2 direccion = (transform.position - (Vector3)puntoGolpe).normalized;
            Vector2 nuevaVelocidad = new Vector2(direccion.x * velocidadRebote.x, rigidbody2.velocity.y);

            rigidbody2.velocity = nuevaVelocidad;

            enRebote = true;

            StartCoroutine(ResetRebote());
        }
    }


    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rigidbody2.velocity.y);
        rigidbody2.velocity = Vector3.SmoothDamp(rigidbody2.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (saltar && saltosRestantes > 0)
        {
            rigidbody2.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            saltosRestantes--;
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        if (ControladorSuelo != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(ControladorSuelo.position, dimensionesCaja);
        }
    }

    private IEnumerator ResetRebote()
    {
        yield return new WaitForSeconds(0.4f);
        enRebote = false;
    }
}
