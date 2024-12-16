using UnityEngine;
using TMPro; // Asegúrate de tener este namespace para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Texto donde se mostrará el puntaje (asignar desde el Inspector)
    private float score = 1000f;       // Puntaje inicial
    private float timer = 0f;          // Temporizador para la disminución del puntaje

    void Start()
    {
        // Asegúrate de que el texto del puntaje se inicialice correctamente
        UpdateScoreText();
    }

    void Update()
    {
        // Disminuir el puntaje con el tiempo (por ejemplo, cada segundo disminuye 1 punto)
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            score -= 1f;   // Disminuir el puntaje por segundo
            timer = 0f;    // Resetear el temporizador

            UpdateScoreText();
        }
    }

    // Actualizar el texto en la UI
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString("F0"); // Muestra el puntaje sin decimales
    }

    // Métodos para guardar y cargar el puntaje (llamados desde el SaveLoadManager)
    public float GetScore()
    {
        return score;
    }

    public void SetScore(float newScore)
    {
        score = newScore;
        UpdateScoreText();
    }
}
