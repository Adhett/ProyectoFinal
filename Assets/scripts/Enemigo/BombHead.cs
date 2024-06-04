using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHead : EnemyDamage
{
    public Rigidbody2D rb2D;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool estaSubiendo = false;
    public float velocidadSubida;
    public float tiempoEspera;
    public Animator animator;


    private void Update()
    {

        RaycastHit2D infoJugador = Physics2D.Raycast(transform.position,
            Vector3.down, distanciaLinea, capaJugador);

        if (infoJugador && !estaSubiendo)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;


        }
        if (estaSubiendo)
        {
            transform.Translate(Time.deltaTime * velocidadSubida * Vector3.up);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("suelo"))
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            if (estaSubiendo)
                estaSubiendo = false;
            else
                animator.SetTrigger("golpe");
                StartCoroutine(EsperarSuelo());
        }
    }

    private IEnumerator EsperarSuelo()
    {
        yield return new WaitForSeconds(tiempoEspera);
        estaSubiendo = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaLinea);
    }
}
