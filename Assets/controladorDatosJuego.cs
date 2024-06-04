using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class controladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
        jugador = GameObject.FindGameObjectWithTag("Player");

        if (File.Exists(archivoDeGuardado))
        {
            CargarDatos();
        }
    }

    public void GuardarEstadoEnemigo(bool isDefeated)
    {
        PlayerPrefs.SetInt("EnemigoDerrotado", isDefeated ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool CargarEstadoEnemigo()
    {
        return PlayerPrefs.GetInt("EnemigoDerrotado", 0) == 1;
    }


    private void OnDestroy()
    {
        //GuardarDatos();
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            Debug.Log("Posicion jugador: " + datosJuego.posicion);
            jugador.transform.position = datosJuego.posicion;
        }
        else
            Debug.Log("El archivo no existe");
    }

    private void GuardarDatos()
    {
        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position
        };
        string cadenaJson = JsonUtility.ToJson(nuevosDatos);
        File.WriteAllText(archivoDeGuardado, cadenaJson);

        Debug.Log("Partida Guardada");
    }

    public void GuardarPartida()
    {
        GuardarDatos();
    }
}
