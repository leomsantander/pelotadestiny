using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cenas : MonoBehaviour
{
    public void TrocarParaDepoisDe(string nome, float tempo)
    {
        StartCoroutine(Vai(nome, tempo));
    }

    private IEnumerator Vai(string nome, float tempo)
    {
        yield return new WaitForSeconds(tempo);

        SceneManager.LoadSceneAsync(nome);
    }
}
