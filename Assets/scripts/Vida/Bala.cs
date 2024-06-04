using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad = 1000f;
    [SerializeField] private float daño;
    private Vector3 direccion;

    public void SetDireccion(Vector3 nuevaDireccion)
    {
        direccion = nuevaDireccion;
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * (direccion.x > 0 ? 1 : -1);
        transform.localScale = escala;
    }

    private void Update()
    {
        transform.Translate(direccion.normalized * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Bala colisionó con: " + other.name);
        if (other.CompareTag("enemigo"))
        {
            Goblin goblin = other.GetComponent<Goblin>();
            if (goblin != null)
            {
                goblin.TomarDaño(daño);
            }
            else
            {
                Enemigo enemigo = other.GetComponent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDaño(daño);
                }
            }
            Destroy(gameObject);
        }
    }
}
