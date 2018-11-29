using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroi : MonoBehaviour
{
    public float tempo;
	void Update ()
    {
        Destroy(gameObject, tempo);
	}
}
