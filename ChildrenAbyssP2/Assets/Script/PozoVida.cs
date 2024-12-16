using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PozoVida : MonoBehaviour
{
    public float CantidadCura;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<BarraVida>())
        {
            other.GetComponent<BarraVida>().RecibirCura(CantidadCura);

            Destroy(gameObject);
        }
    }
}
