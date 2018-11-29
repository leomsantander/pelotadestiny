using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject barraNivel;
    [SerializeField] private GameObject hpBossDemonio;
    [SerializeField] private Image hpDemonio;
    [SerializeField] private GameObject hpBossAnjo;
    [SerializeField] private Image hpAnjo;
    [SerializeField] private string nome;
    [SerializeField] private ParticleSystem poder;
    [SerializeField] private float tempoPoder, tempoMin, tempoMax;
    [SerializeField] public float hp = 100;
    [SerializeField] private NavMeshAgent boss;
    [HideInInspector] [SerializeField] private float randomTempo;
    [HideInInspector] [SerializeField] private float tempoRecalculo;

    private void Start()
    {
        nome = transform.name;
        barraNivel.SetActive(false);
        randomTempo = Random.Range(tempoMin, tempoMax);
        boss.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        if (nome.Equals("SuperDemonio"))
        {
            hpBossDemonio.SetActive(true);
        }
        if (nome.Equals("SuperAnjo"))
        {
            hpBossAnjo.SetActive(true);
        }
    }

    private void Update()
    {
        tempoRecalculo += Time.deltaTime;
        if(tempoRecalculo> 0.5f)
        {
            tempoRecalculo = 0;
            if(GameObject.FindGameObjectWithTag("Player"))
                boss.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }

        if (nome.Equals("SuperDemonio"))
        {
            hpDemonio.fillAmount = hp / 100;
        }
        if (nome.Equals("SuperAnjo"))
        {
            hpAnjo.fillAmount = hp / 100;
        }

        tempoPoder += Time.deltaTime;

        if(tempoPoder > randomTempo)
        {
            tempoPoder = 0;
            poder.Play();
            randomTempo = Random.Range(tempoMin, tempoMax);
        }

        if(hp <= 0)
        {
            Morte();
        }
    }

    private void Morte()
    {
        Destroy(gameObject);
    }
}
