using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pelota;
    [SerializeField] private GameObject pelota2;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject jogo;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject creditos;

    private void Start()
    {
        menu.SetActive(true);
        jogo.SetActive(false);
        hud.SetActive(false);
        creditos.SetActive(false);
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Jogar()
    {
        GetComponent<CameraJogo>().enabled = true;
        pelota.GetComponent<Pelota>().Jogar();
        pelota.SetActive(true);
        pelota2.SetActive(false);
        menu.SetActive(false);
        jogo.SetActive(true);
        hud.SetActive(true);
        enabled = false;
    }

    public void Creditos()
    {
        menu.SetActive(false);
        creditos.SetActive(true);
    }

    public void Voltar()
    {
        menu.SetActive(true);
        creditos.SetActive(false);
    }
}
