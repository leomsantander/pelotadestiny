using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraNivel : MonoBehaviour
{
    [SerializeField] private Image barra;
    [SerializeField] private Pelota nivel;

    private void Update()
    {
        barra.fillAmount = nivel.GetNivel();
    }
}
