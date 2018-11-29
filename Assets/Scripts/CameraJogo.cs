using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJogo : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] [HideInInspector] private Vector3 newOffSet;
    [SerializeField] private float velocidade;
    [SerializeField] private float tempoTremer;
    [SerializeField] private float tempo;
    [SerializeField] private AtivarAudio tremor;

    private void Start()
    {
        tempoTremer = Random.Range(10, 30);
    }

    private void Update()
    {
        if (!player) return;
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Tremer(1,1);
        }
        */
        tempo += Time.deltaTime;
        if (tempo > tempoTremer)
        {
            tempo = 0;
            Tremer(1,1);
            tempoTremer = Random.Range(10, 30);
        }
    }

    private void FixedUpdate()
    {
        if (!player) return;
        newOffSet.x = player.position.x;
        newOffSet.y = player.position.y;
        newOffSet.z = player.position.z;

        newOffSet.x += offset.x;
        newOffSet.y += offset.y;
        newOffSet.z += offset.z;

        transform.position = Vector3.Lerp(transform.position, newOffSet, velocidade * Time.deltaTime);
    }

    public void Tremer(float tempo, float magnitude)
    {
        tremor.Tocar(0);
        if (!player) return;
        StartCoroutine(Shake(tempo, magnitude));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(player.position.x -5f, player.position.x + 5f) * magnitude;
            float y = Random.Range(15f, 20f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
