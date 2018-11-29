using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject anjos;
    [SerializeField] private GameObject demonios;
    [SerializeField] private GameObject morteAnjo;
    [SerializeField] private GameObject morteDemi;
    [SerializeField] private float tempoMinimo;
    [SerializeField] private float tempoMaximo;
    [HideInInspector] [SerializeField] private float tempo;
    [HideInInspector] [SerializeField] private int tipo;

    private void Update()
    {
        tempo += Time.deltaTime;

        if (tempo > Random.Range(tempoMinimo, tempoMaximo))
        {
            tempo = 0;
            tipo = 0;
            tipo = Random.Range(0, 2);

            switch (tipo)
            {
                case 0:
                    {
                        GameObject minion = Instantiate(anjos);
                        GameObject morte = Instantiate(morteAnjo);
                        minion.transform.position = new Vector3(Random.Range(-30, 30), 1, Random.Range(-30, 30));
                        morte.transform.position = minion.transform.position;
                        minion.transform.name = "Anjo";
                        break;
                    }
                case 1:
                    {
                        GameObject minion = Instantiate(demonios);
                        GameObject morte = Instantiate(morteDemi);
                        minion.transform.position = new Vector3(Random.Range(-30, 30), 1, Random.Range(-30, 30));
                        morte.transform.position = minion.transform.position;
                        minion.transform.name = "Demonio";
                        break;
                    }
            }
            
        }
    }
}
