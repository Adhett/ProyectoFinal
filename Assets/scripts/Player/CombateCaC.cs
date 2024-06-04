using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform contorladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Golpe1();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
        if (Input.GetButtonDown("Fire3"))
        {
            Golpe2();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    private void Golpe()
    {
        animator.SetTrigger("ataque");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(contorladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("enemigo"))
            {
                Enemigo enemigo = colisionador.GetComponent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(dañoGolpe);
                }

                Goblin goblin = colisionador.GetComponent<Goblin>();
                if (goblin != null)
                {
                    goblin.TomarDaño(dañoGolpe);
                }
            }
        }
    }

    private void Golpe1()
    {
        animator.SetTrigger("attack");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(contorladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("enemigo"))
            {
                Enemigo enemigo = colisionador.GetComponent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(dañoGolpe);
                }

                Goblin goblin = colisionador.GetComponent<Goblin>();
                if (goblin != null)
                {
                    goblin.TomarDaño(dañoGolpe);
                }
            }
        }
    }

    private void Golpe2()
    {
        animator.SetTrigger("patada");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(contorladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("enemigo"))
            {
                Enemigo enemigo = colisionador.GetComponent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(dañoGolpe);
                }

                Goblin goblin = colisionador.GetComponent<Goblin>();
                if (goblin != null)
                {
                    goblin.TomarDaño(dañoGolpe);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(contorladorGolpe.position, radioGolpe);
    }
}
