using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Start is called before the first frame update
    public void CambioNivel(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
