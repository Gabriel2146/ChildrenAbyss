using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickTime : MonoBehaviour
{
    public GameObject DisplayBox;
    public GameObject PassBox;
    public GameObject QTECanvas; // Canvas del QTE
    public PlayerMove playerMove;

    private List<KeyCode> QTESequence = new List<KeyCode>();
    private int currentStep;
    private bool eventActive;
    private bool keyReleasedAfterStart = false; // Verifica si se liberó la tecla de inicio
    private Coroutine qteCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !eventActive)
        {
            StartQTE();
        }

        if (eventActive && keyReleasedAfterStart && Input.anyKeyDown)
        {
            if (Input.GetKeyDown(QTESequence[currentStep]))
            {
                currentStep++;
                PassBox.GetComponent<Text>().text = "¡CORRECTO!";

                if (currentStep >= QTESequence.Count) // Si completaste todas las teclas correctamente
                {
                    Success();
                }
            }
            else
            {
                Fail();
            }
        }

        if (eventActive && Input.GetKeyUp(KeyCode.T))
        {
            keyReleasedAfterStart = true; // Confirmar que la tecla T fue liberada
        }
    }

    void StartQTE()
    {
        eventActive = true;
        keyReleasedAfterStart = false; // Reiniciar estado de la tecla T
        QTECanvas.SetActive(true);

        // Generar secuencia de 3 letras aleatorias
        QTESequence.Clear();
        QTESequence.Add(GetRandomKey());
        QTESequence.Add(GetRandomKey());
        QTESequence.Add(GetRandomKey());

        currentStep = 0;
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = ""; // Limpiar display

        qteCoroutine = StartCoroutine(ShowQTESequence());
    }

    IEnumerator ShowQTESequence()
    {
        for (int i = 0; i < QTESequence.Count; i++)
        {
            DisplayBox.GetComponent<Text>().text = QTESequence[i].ToString(); // Mostrar letra actual
            currentStep = i; // Actualizar el paso actual
            PassBox.GetComponent<Text>().text = "";

            yield return new WaitForSeconds(1.5f); // Tiempo para presionar la letra

            if (currentStep == i) // Si el jugador no presiona la tecla correcta en este paso
            {
                Fail();
                yield break;
            }
        }

        // Si llegamos aquí, el jugador completó la secuencia
        DisplayBox.GetComponent<Text>().text = "";
    }

    void Success()
    {
        if (qteCoroutine != null)
        {
            StopCoroutine(qteCoroutine);
        }

        PassBox.GetComponent<Text>().text = "¡ÉXITO!";
        eventActive = false;

        // Convertir al personaje en demonio solo si no lo es ya
        if (!playerMove.Demonio)
        {
            playerMove.ToggleSkin(); // Asegúrate de que esta línea no esté comentada
        }

        StartCoroutine(EndQTE());
    }

    void Fail()
    {
        if (qteCoroutine != null)
        {
            StopCoroutine(qteCoroutine);
        }

        PassBox.GetComponent<Text>().text = "¡FALLASTE!";
        //playerMove.barraVida.Salud -= 10; // Reducir vida
        eventActive = false;

        StartCoroutine(EndQTE());
    }

    IEnumerator EndQTE()
    {
        yield return new WaitForSeconds(1.5f);
        PassBox.GetComponent<Text>().text = "";
        DisplayBox.GetComponent<Text>().text = "";
        QTECanvas.SetActive(false);
    }

    KeyCode GetRandomKey()
    {
        KeyCode[] keys = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.Y, KeyCode.U };
        return keys[Random.Range(0, keys.Length)];
    }
}
