using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PozoMaldito : MonoBehaviour
{
    public float CantidadDanio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<BarraVida>())
        {
            other.GetComponent<BarraVida>().RecibirDanio(CantidadDanio);
        }
    }
}
