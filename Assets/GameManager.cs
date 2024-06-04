using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<Personaje> personajes;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DesbloquearPersonaje(string nombrePersonaje)
    {
        foreach (var personaje in personajes)
        {
            if (personaje.nombre == nombrePersonaje)
            {
                personaje.desbloqueado = true;
                break;
            }
        }
    }

    public void GuardarEstadoJuego()
    {
        foreach (var personaje in personajes)
        {
            PlayerPrefs.SetInt(personaje.nombre, personaje.desbloqueado ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void CargarEstadoJuego()
    {
        foreach (var personaje in personajes)
        {
            personaje.desbloqueado = PlayerPrefs.GetInt(personaje.nombre, 0) == 1;
        }
    }

    [System.Serializable]
    public class Personaje
    {
        public string nombre;
        public Sprite imagen;
        public GameObject personajeJugable;
        public bool desbloqueado;
    }
}
