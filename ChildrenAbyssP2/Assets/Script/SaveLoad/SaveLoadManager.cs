using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public GameManager gameManager; // Referencia al GameManager (asignar desde el Inspector)

    // Guardar puntaje
    public void SaveGame()
    {
        float score = gameManager.GetScore();  // Obtener el puntaje actual
        PlayerPrefs.SetFloat("Score", score);  // Guardar el puntaje
        PlayerPrefs.Save();                   // Asegurarse de que los datos se guarden
        Debug.Log("Juego guardado. Puntaje: " + score);
    }

    // Cargar puntaje
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            float loadedScore = PlayerPrefs.GetFloat("Score"); // Cargar el puntaje
            gameManager.SetScore(loadedScore);                 // Establecer el puntaje en el GameManager
            Debug.Log("Juego cargado. Puntaje: " + loadedScore);
        }
        else
        {
            Debug.LogWarning("No se encontró un puntaje guardado.");
        }
    }
}
