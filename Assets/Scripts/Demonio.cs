using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demonio : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Anjo"))
        {
            collision.gameObject.GetComponent<Minion>().MorteAnjo();
            Destroy(collision.transform.gameObject);
        }
    }
}
