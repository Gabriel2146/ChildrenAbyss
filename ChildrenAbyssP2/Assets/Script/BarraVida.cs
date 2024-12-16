using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public float Salud = 100;
    public float SaludMaxima = 100;

    [Header("Interfaz")]
    public Image BarraSalud;
    public Text TextoSalud;
    public CanvasGroup PantallaRoja;

    [Header("Muerto")]
    public GameObject Muerto;
    void Update()
    {
        if(PantallaRoja.alpha > 0)
        {
            PantallaRoja.alpha -= Time.deltaTime;
        }
        ActualizarInterfaz();
    }

    public void RecibirCura(float cura)
    {
        Salud += cura;
        if(Salud > SaludMaxima)
        {
            Salud = SaludMaxima;
        }
    }

    public void RecibirDanio(float danio)
    {
        Salud -= danio;
        PantallaRoja.alpha = 1;

        if (Salud <= 0)
        {
            Salud = 0;
            Instantiate(Muerto);
            Destroy(gameObject);
        }
    }

    public void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "Salud: " + Salud.ToString("f0");
    }
}
