using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuOpciones : MonoBehaviour
{
    public GameObject opciones; // El Panel de opciones
    private bool isPaused = false;

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "SampleScene" && Input.GetKeyDown(KeyCode.Escape)) // Modifica esta línea
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }


    public void PauseGame()
    {
        opciones.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        opciones.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void GuardarPartida()
    {
        GameObject controladorDatosJuego = GameObject.Find("ControladorDeDatos");
        controladorDatosJuego.GetComponent<controladorDatosJuego>().GuardarPartida();
    }
}
