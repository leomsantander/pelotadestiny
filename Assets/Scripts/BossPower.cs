using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPower : MonoBehaviour
{
    [SerializeField] Pelota pelota;
        
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag.Equals("Anjo"))
        {
            other.GetComponent<Minion>().MorteAnjo();
            Destroy(other);
        }
        if (other.tag.Equals("Demonio"))
        {
            other.GetComponent<Minion>().MorteDemi();
            Destroy(other);
        }
        if(other.tag.Equals("Player"))
        {
            pelota.GameOver();
        }
    }
}
