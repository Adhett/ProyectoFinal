using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInicial : MonoBehaviour
{
    public GameObject menuInicial;
    public GameObject menuSeleccion;
    public GameObject opciones;
    private bool isPaused = false;
    public bool isGameStarted = false;
    private void Start()
    {
        menuInicial.SetActive(true);
        menuSeleccion.SetActive(false);
    }

    public void NewGame()
    {
        menuInicial.SetActive(false);
        menuSeleccion.SetActive(true);
        isGameStarted = true;
    }

    public void Opciones()
    {
        if (isGameStarted)
        {
            opciones.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void SeleccionPersonaje()
    {
        menuInicial.SetActive(false);
        menuSeleccion.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }

    public void VolverAlMenuInicial()
    {
        menuSeleccion.SetActive(false);
        menuInicial.SetActive(true);
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




}
