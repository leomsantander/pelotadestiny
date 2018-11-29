using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    [SerializeField] public Transform alvo;
    [SerializeField] private GameObject almaAnjo;
    [SerializeField] private GameObject almaDemi;
    [SerializeField] private GameObject morteAnjo;
    [SerializeField] private GameObject morteDemi;
    [SerializeField] private AtivarAudio explosao;
    [HideInInspector] [SerializeField] private NavMeshAgent minion;
    [HideInInspector] [SerializeField] private float tempo;

    private void Start()
    {
        explosao = GameObject.FindGameObjectWithTag("EXP").GetComponent<AtivarAudio>();
        minion = GetComponent<NavMeshAgent>();
        CalcularRota();
    }

    private void Update()
    {
        tempo += Time.deltaTime;

        if(tempo > 0.5f)
        {
            CalcularRota();
            tempo = 0;
        }
    }

    private void FixedUpdate()
    {
        if(minion && alvo)
            minion.SetDestination(alvo.position);
    }

    private Transform FindClosestAnjo()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Anjo closestEnemy = null;
        Anjo[] allEnemies = GameObject.FindObjectsOfType<Anjo>();

        foreach (Anjo currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        if (closestEnemy)
        {
            Debug.DrawLine(transform.position, closestEnemy.transform.position);
            return closestEnemy.transform;
        }
        else
            return transform;
    }

    private Transform FindClosestDemonio()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        Demonio closestEnemy = null;
        Demonio[] allEnemies = GameObject.FindObjectsOfType<Demonio>();

        foreach (Demonio currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        if (closestEnemy)
        {
            Debug.DrawLine(transform.position, closestEnemy.transform.position);
            return closestEnemy.transform;
        }
        else
            return transform;
    }

    private void CalcularRota()
    {
        if (!GameObject.FindGameObjectWithTag("Player")) return;
        alvo = GameObject.FindGameObjectWithTag("Player").transform;
        switch (transform.name)
        {
            case "Anjo":
                {
                    if (alvo.GetComponent<Pelota>().GetNivel() < 0.5f)
                    {
                        alvo = FindClosestDemonio();
                    }
                    break;
                }
            case "Demonio":
                {
                    if (alvo.GetComponent<Pelota>().GetNivel() > 0.5f)
                    {
                        alvo = FindClosestAnjo();
                    }
                    break;
                }
        }
    }

    public void MorteAnjo()
    {
        explosao.TocarRandomico();
        GameObject novaAlma = Instantiate(almaAnjo);
        Vector3 offset = transform.position;
        offset.y += 1;
        novaAlma.transform.position = offset;
        GameObject morte = Instantiate(morteAnjo);
        morte.transform.position = offset;
    }

    public void MorteDemi()
    {
        explosao.TocarRandomico();
        GameObject novaAlma = Instantiate(almaDemi);
        Vector3 offset = transform.position;
        offset.y += 1;
        novaAlma.transform.position = offset;
        GameObject morte = Instantiate(morteDemi);
        morte.transform.position = offset;
    }
}
