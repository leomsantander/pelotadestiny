using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anjo : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Demonio"))
        {
            collision.gameObject.GetComponent<Minion>().MorteDemi();
            Destroy(collision.transform.gameObject);
        }
    }
}
