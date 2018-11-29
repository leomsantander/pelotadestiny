using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Almas : MonoBehaviour
{
    private Rigidbody rgb;

    private void Start()
    {
        transform.Rotate(0,Random.Range(0,360),0);
        rgb = GetComponent<Rigidbody>();
        rgb.AddForce(new Vector3(Random.Range(0,200), Random.Range(100, 200), Random.Range(0, 200)));
    }

    private void Update()
    {
        if (transform.position.y < -100)
            Destroy(gameObject);
    }
}
