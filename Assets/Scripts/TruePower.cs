using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruePower : MonoBehaviour
{
    [SerializeField] Pelota pelota;
    
    private void OnParticleCollision(GameObject other)
    {
        if(other.tag.Equals("Anjo"))
        {
            other.GetComponent<Minion>().MorteAnjo();
            pelota.SomarNivel(0.05f);
            Destroy(other);
        }
        if (other.tag.Equals("Demonio"))
        {
            other.GetComponent<Minion>().MorteDemi();
            pelota.SomarNivel(-0.05f);
            Destroy(other);
        }
        if (other.tag.Equals("Boss"))
        {
            if(other.transform.name.Equals("SuperAnjo"))
                other.transform.GetComponent<Boss>().hp -= 0.25f;
            if (other.transform.name.Equals("SuperDemonio"))
                other.transform.GetComponent<Boss>().hp -= 0.75f;
        }
    }
}
