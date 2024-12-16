using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Referencia al componente TextMeshPro
    public float totalTime = 60.0f;   // Tiempo inicial en segundos
    private float timeRemaining;     // Tiempo restante
    private bool timerRunning = false;

    public BarraVida barraVida;      // Referencia al script BarraVida

    void Start()
    {
        timeRemaining = totalTime;
        timerRunning = true;
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (timerRunning)
        {
            // Actualizar visualmente el temporizador
            timerText.text = "Tiempo: " + Mathf.Ceil(timeRemaining).ToString() + "s";
        }
    }

    IEnumerator StartCountdown()
    {
        while (timeRemaining > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeRemaining--;

            if (timeRemaining <= 0)
            {
                timerRunning = false;
                TimerFinished();
            }
        }
    }

    void TimerFinished()
    {
        timerText.text = "¡Tiempo agotado!";
        Debug.Log("El temporizador ha terminado.");
        if (barraVida != null)
        {
            barraVida.RecibirDanio(barraVida.Salud); // Reducir la salud del jugador a 0
        }
    }
}
