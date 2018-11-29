using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarAudio : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip[] sons;

    public void Tocar(int indice)
    {
        sfx.clip = sons[indice];
        sfx.Play();
    }

    public void TocarRandomico()
    {
        int n = Random.Range(1, sons.Length);
        sfx.clip = sons[n];
        sfx.Play();
        sons[n] = sons[0];
        sons[0] = sfx.clip;
    }
}
