using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSeleccionPersonaje : MonoBehaviour
{
    private int index;
    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.CargarEstadoJuego(); // Cargar el estado del juego

        index = PlayerPrefs.GetInt("JugadorIndex", 0);
        if (index > gameManager.personajes.Count - 1)
            index = 0;
        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        while (!gameManager.personajes[index].desbloqueado)
        {
            index = (index + 1) % gameManager.personajes.Count;
        }

        PlayerPrefs.SetInt("JugadorIndex", index);
        imagen.sprite = gameManager.personajes[index].imagen;
        nombre.text = gameManager.personajes[index].nombre;

        imagen.color = gameManager.personajes[index].desbloqueado ? Color.white : Color.gray;
    }

    public void SiguientePersonaje()
    {
        int startIndex = index;
        do
        {
            index = (index + 1) % gameManager.personajes.Count;
        } while (!gameManager.personajes[index].desbloqueado && index != startIndex);

        CambiarPantalla();
    }

    public void AnteriorPersonaje()
    {
        int startIndex = index;
        do
        {
            index = (index - 1 + gameManager.personajes.Count) % gameManager.personajes.Count;
        } while (!gameManager.personajes[index].desbloqueado && index != startIndex);

        CambiarPantalla();
    }

    public void IniciarJuego()
    {
        GameObject controladorDatosJuegoObj = GameObject.Find("ControladorDeDatos");
        controladorDatosJuego controladorDatosJuegoScript = controladorDatosJuegoObj.GetComponent<controladorDatosJuego>();

        if (gameManager.personajes[index].desbloqueado || controladorDatosJuegoScript.CargarEstadoEnemigo())
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Personaje bloqueado. No se puede iniciar el juego.");
        }
    }
}
