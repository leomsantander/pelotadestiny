using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    [SerializeField] private Pelota pelota;
    [SerializeField] private ParticleSystem[] powerAnjo;
    [SerializeField] private ParticleSystem[] powerDemonio;
    [SerializeField] private GameObject powerAnjoUi;
    [SerializeField] private Image powerAnjoNivel;
    [SerializeField] private GameObject powerDemonioUi;
    [SerializeField] private Image powerDemonioNivel;
    [SerializeField] private float quantiaPower;
    [SerializeField] private float velocidadePower;
    [HideInInspector] [SerializeField] private float nivelPower;
    [HideInInspector] [SerializeField] private float tempoPower;

    private void Update()
    {
        powerAnjoNivel.fillAmount = nivelPower / 100;
        powerDemonioNivel.fillAmount = nivelPower / 100;

        tempoPower += Time.deltaTime;

        if(pelota.GetNivel() < 0.5f)
        {
            powerAnjoUi.SetActive(false);
            powerDemonioUi.SetActive(true);
        }
        if (pelota.GetNivel() > 0.5f)
        {
            powerAnjoUi.SetActive(true);
            powerDemonioUi.SetActive(false);
        }
        if(tempoPower > velocidadePower)
        {
            if(nivelPower <= 100)
                nivelPower += quantiaPower;
        }
        if((Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z)) && nivelPower >= 100 && powerDemonioUi.activeSelf == true)
        {
            AtivarPowerAnjo();
        }
        if ((Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.C)) && nivelPower >= 100 && powerAnjoUi.activeSelf == true)
        {
            AtivarPowerDemonio();
        }
    }

    public void AtivarPowerAnjo()
    {
        nivelPower = 0;
        StartCoroutine(pelota.VoltarAtaque(2));
        pelota.ataques[3].Tocar(0);
        for (int i = 0; i < powerAnjo.Length; i++)
        {
            if (powerAnjo[i].gameObject.activeSelf == false)
                powerAnjo[i].gameObject.SetActive(true);
            powerAnjo[i].Play();
        }
    }

    public void AtivarPowerDemonio()
    {
        nivelPower = 0;
        StartCoroutine(pelota.VoltarAtaque(2));
        pelota.ataques[2].TocarRandomico();
        for (int i =0; i < powerDemonio.Length; i++)
        {
            if(powerDemonio[i].gameObject.activeSelf == false)
                powerDemonio[i].gameObject.SetActive(true);
            powerDemonio[i].Play();
        }
    }
}
