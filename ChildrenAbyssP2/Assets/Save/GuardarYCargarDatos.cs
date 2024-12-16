using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public int puntos;
    public TMP_Text Puntostxt;

    private void Start()
    {
        CargarDatos();
        PonerDatosEnTexto();
    }

    public void GuardarDatos()
    {
        PlayerPrefs.SetInt("puntos", puntos);
        PlayerPrefs.Save(); // Asegura que los datos se guarden inmediatamente
        PonerDatosEnTexto();
    }

    public void CargarDatos()
    {
        puntos = PlayerPrefs.GetInt("puntos", 0); // Proporciona un valor predeterminado de 0
    }

    public void PonerDatosEnTexto()
    {
        Puntostxt.text = "Puntos: " + puntos.ToString();
    }
}
