using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pelota : MonoBehaviour
{
    [SerializeField] private bool jogo = false;
    [SerializeField] private float veloc;
    [SerializeField] private float turnSpeed;
    [Range (0f, 1f)][SerializeField] private float nivel;
    [SerializeField] private SkinnedMeshRenderer pelota;
    [SerializeField] private SkinnedMeshRenderer asas;
    [SerializeField] private Animator anim;
    [SerializeField] private Material corPelota;
    [SerializeField] private Material corAsas;
    [SerializeField] private float multplicadorCor = 2;
    [SerializeField] private GameObject[] almaPelota;
    [SerializeField] public bool ataque = false;
    [SerializeField] public AtivarAudio[] ataques;
    [SerializeField] private AtivarAudio explosao;
    [SerializeField] private Cenas cenas;
    [SerializeField] private GameObject bossAnjo;
    [SerializeField] private GameObject bossDemonio;
    [SerializeField] private bool egg = false;
    [SerializeField] private GameObject ester;
    [SerializeField] private GameObject butao;

    private void Update()
    {
        if (!jogo) return;

        if (egg == true)
        {
            butao.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ester.activeSelf)
                {
                    ester.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    ester.SetActive(true);
                    Time.timeScale = 0f;
                }
            }
        }
        else
        {
            butao.SetActive(false);
        }
        if (nivel>=1)
        {
            bossAnjo.SetActive(true);
           
        }
        if (bossAnjo.GetComponent<Boss>().hp <= 0)
        {
            jogo = false;
            cenas.TrocarParaDepoisDe("LoadFinalDemonio", 1.5f);
        }
        if (nivel <= 0)
        {
            bossDemonio.SetActive(true);
            
        }
        if (bossDemonio.GetComponent<Boss>().hp <= 0)
        {
            jogo = false;
            cenas.TrocarParaDepoisDe("LoadFinalAnjo", 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cenas.TrocarParaDepoisDe("LoadJogo", 0f);
        }
        
        if (!bossDemonio.activeSelf || !bossAnjo.activeSelf)
        {
            pelota.SetBlendShapeWeight(0, nivel * 100);
            asas.SetBlendShapeWeight(0, nivel * 100);
            corPelota.mainTextureScale = new Vector2(nivel * multplicadorCor, 0);
            corAsas.mainTextureScale = new Vector2(nivel * multplicadorCor, 0);
            if (nivel < 0.10f && multplicadorCor > 2)
                multplicadorCor -= 0.1f;
            else if (nivel > 0.90f && multplicadorCor < 12)
                multplicadorCor += 0.1f;
            else if (nivel < 0.90f && nivel > 0.10f)
            {
                if (multplicadorCor > 2)
                    multplicadorCor -= 0.1f;
            }
        }
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.X)) && !ataque)
        {
            ataque = true;
            anim.SetTrigger("ataque");
            ataques[0].TocarRandomico();
            ataques[1].TocarRandomico();
            StartCoroutine(VoltarAtaque(0.5f));
        }

        anim.SetFloat("velocidade", Input.GetAxis("Vertical"));

        if (transform.position.y < -10)
            GameOver();
    }

    private void FixedUpdate()
    {
        if (!jogo) return;

        transform.Translate( 0, 0, Input.GetAxis("Vertical") * Time.deltaTime * veloc);

        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed, 0);
    }

    public float GetNivel()
    {
        return nivel;
    }

    public void SomarNivel(float valor)
    {
        if(bossDemonio || bossAnjo)
        if (bossDemonio.activeSelf || bossAnjo.activeSelf) return;
            nivel +=valor;
    }

    public void Jogar()
    {
        jogo = true;
    }

    public IEnumerator VoltarAtaque(float tempo)
    {
        ataque = true;
        yield return new WaitForSeconds(tempo);
        ataque = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ataque)
        {
            if (collision.transform.tag.Equals("Anjo"))
            {
                if (!bossDemonio.activeSelf || !bossAnjo.activeSelf)
                    nivel += 0.05f;
                collision.gameObject.GetComponent<Minion>().MorteAnjo();
                Destroy(collision.transform.gameObject);
            }
            if (collision.transform.tag.Equals("Demonio"))
            {
                if (!bossDemonio.activeSelf || !bossAnjo.activeSelf)
                    nivel -= 0.05f;
                collision.gameObject.GetComponent<Minion>().MorteDemi();
                Destroy(collision.transform.gameObject);
            }
            if (collision.transform.tag.Equals("Boss"))
            {
                collision.transform.GetComponent<Boss>().hp -= 10;
            }
        }
        else
        {
            if (collision.transform.tag.Equals("Anjo") && nivel >= 0.5f)
            {
                collision.gameObject.GetComponent<Minion>().MorteAnjo();
                GameOver();
                Destroy(collision.gameObject);
            }
            if (collision.transform.tag.Equals("Demonio") && nivel <= 0.5f)
            {
                GameOver();
                collision.gameObject.GetComponent<Minion>().MorteDemi();
                Destroy(collision.gameObject);
            }
            if (collision.transform.tag.Equals("Boss"))
            {
                GameOver();
            }
        }
        if (collision.transform.tag.Equals("AlmaAnjo"))
        {
            if (!bossDemonio.activeSelf || !bossAnjo.activeSelf)
                nivel += 0.01f;
            Destroy(collision.transform.gameObject);
        }
        if (collision.transform.tag.Equals("AlmaDemonio"))
        {
            if (!bossDemonio.activeSelf || !bossAnjo.activeSelf)
                nivel -= 0.01f;
            Destroy(collision.transform.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("EGG"))
        {
            egg = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("EGG"))
        {
            egg = false;
        }
    }

    public void GameOver()
    {
        jogo = false;
        Vector3 offset = transform.position;
        offset.y += 1;
        explosao.TocarRandomico();
        if (nivel > 0.5f)
        {
            GameObject alma = Instantiate(almaPelota[1]);
            
            alma.transform.position = offset;
        }
        else if(nivel < 0.5f)
        {
            GameObject alma = Instantiate(almaPelota[0]);

            alma.transform.position = offset;
        }
        else
        {
            GameObject almae = Instantiate(almaPelota[0]);
            GameObject almaa = Instantiate(almaPelota[1]);

            almaa.transform.position = offset;
            almae.transform.position = offset;
        }
        Destroy(gameObject);
        cenas.TrocarParaDepoisDe("LoadJogo", 1.5f);
    }
}
