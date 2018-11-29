using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    [SerializeField] private string cenaCarregar;
    [SerializeField] private float tempoFixoSeg = 5;
    [SerializeField] private enum tipoCarregar {Carregamento, TempoFixo};
    [SerializeField] private tipoCarregar TipoDeCarregamento;
    [SerializeField] private Image barraDeCarregamento;
    [SerializeField] Text textoProgresso;
    [SerializeField] private int progresso = 0;
    [SerializeField] private string textoOriginal;

    void Start ()
    {
        switch (TipoDeCarregamento)
        {
            case tipoCarregar.Carregamento:
                StartCoroutine(CenaDeCarregamento(cenaCarregar));
                break;
            case tipoCarregar.TempoFixo:
                StartCoroutine(TempoFixo(cenaCarregar));
                break;
        }
        if(textoProgresso!=null)
            textoOriginal = textoProgresso.text;

        if (barraDeCarregamento != null)
        {
            barraDeCarregamento.type = Image.Type.Filled;
            barraDeCarregamento.fillMethod = Image.FillMethod.Horizontal;
            barraDeCarregamento.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
	}
	
    IEnumerator CenaDeCarregamento(string cena)
    {
        AsyncOperation carregamento = SceneManager.LoadSceneAsync(cena);
        while (!carregamento.isDone)
        {
            progresso = (int)(carregamento.progress * 100.0f);
            yield return null;
        }
    }
    IEnumerator TempoFixo(string cena)
    {
        yield return new WaitForSeconds(tempoFixoSeg);
        SceneManager.LoadScene(cena);
    }
	void Update ()
    {
        switch (TipoDeCarregamento)
        {
            case tipoCarregar.Carregamento:
                break;
            case tipoCarregar.TempoFixo:
                progresso = (int)(Mathf.Clamp((Time.time / tempoFixoSeg),0.0f,1.0f) * 100.0f);
                break;
        }
        if (textoProgresso != null)
            textoProgresso.text = textoOriginal + " " + progresso + "%";
        if (barraDeCarregamento != null)
        {
            barraDeCarregamento.fillAmount = (progresso / 100.0f);
        }
    }
}
