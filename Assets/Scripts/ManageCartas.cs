using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta;                //carta a ser descartada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada;     //indicam quando as cartas são viradas
    private GameObject carta1, carta2;      //gameObjects das cartas selecionadas
    private string linhaCarta1, linhaCarta2;

    bool timerPausado, timerAcionado;       //indicador do Timer
    float timer;                            //tempo

    int numTentativas = 0;                  // Numero de tentativas totais

    int numAcertos = 0;                     //numero de acertos

    AudioSource somOK;                      //som quando acerta

    int ultimoJogo = PlayerPrefs.GetInt("Jogadas");

    int recordJogo = PlayerPrefs.GetInt("Records");

    // Start is called before the first frame update
    void Start()
    {
        MostraCartas();
        UpDateTentativas();
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);
        recordJogo = PlayerPrefs.GetInt("Records", 0);
        if(ultimoJogo == 0)
        {
            recordJogo = 0;
        }
        else if (recordJogo == 0 && ultimoJogo != 0)
        {
            recordJogo = ultimoJogo;
            TelaRecord();
        }
        else if(recordJogo > ultimoJogo)
        {
            recordJogo = ultimoJogo;
            TelaRecord();
        }
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Recorde de tentativas: " + recordJogo;
        GameObject.Find("ultimoJogo").GetComponent<Text>().text = "Jogo anterior: " + ultimoJogo;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            print(timer);
            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;
                if (carta1.tag == carta2.tag)
                {
                    Destroy(carta1);
                    Destroy(carta2);
                    numAcertos++;
                    somOK.Play();
                    if(numAcertos == 26)
                    {
                        PlayerPrefs.SetInt("Jogadas", numTentativas);
                        PlayerPrefs.SetInt("Records", recordJogo);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                else
                {
                    carta1.GetComponent<Tile>().EscondeCarta();
                    carta2.GetComponent<Tile>().EscondeCarta();
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                timer = 0;
            }
        }
    }

    void MostraCartas()
    {
        int[] arrayEmbaralhado = CriarArrayEmbaralhado();
        int[] arrayEmbaralhado2 = CriarArrayEmbaralhado();
        int[] arrayEmbaralhado3 = CriarArrayEmbaralhado();
        int[] arrayEmbaralhado4 = CriarArrayEmbaralhado();
        //Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        //AddUmaCarta();
        for (int i=0; i<13; i++)
        {
            //AddUmaCarta(i);                           quando as cartas estavam ordenadas
            //AddUmaCarta(i,  arrayEmbaralhado[i]);     quando havi somente uma linha embaralhada
            AddUmaCarta(0, i, arrayEmbaralhado[i]);
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);
            AddUmaCarta(2, i, arrayEmbaralhado3[i]);
            AddUmaCarta(3, i, arrayEmbaralhado4[i]);

        }
    }

    void AddUmaCarta(int linha, int rank, int valor)
    {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal )/ 110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * 1.2f), centro.transform.position.y, centro.transform.position.z);
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y, centro.transform.position.z);
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13/2) * fatorEscalaX), centro.transform.position.y + ((linha - 4/2) * fatorEscalaY), centro.transform.position.z);
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank*1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.tag = "" + (valor + 1);
        //c.name = "" + valor;
        c.name = "" + linha + "_" + valor;
        string nomeDaCarta = "";
        string numeroDaCarta = "";
        /*if (rank == 0)
            numeroDaCarta = "ace";
        else if (rank == 10)
            numeroDaCarta = "jack";
        else if (rank == 11)
            numeroDaCarta = "queen";
        else if (rank == 12)
            numeroDaCarta = "king";
        else
            numeroDaCarta = "" + (rank + 1);*/              //else if para array ordenado
        if(linha == 1)
        {
            if (valor == 0)
                numeroDaCarta = "ace";
            else if (valor == 10)
                numeroDaCarta = "jack";
            else if (valor == 11)
                numeroDaCarta = "queen";
            else if (valor == 12)
                numeroDaCarta = "king";
            else
                numeroDaCarta = "" + (valor + 1);                //else if para array embaralhado
            nomeDaCarta = numeroDaCarta + "_of_clubs";
        }
        else if(linha == 2)
        {
            if (valor == 0)
                numeroDaCarta = "ace";
            else if (valor == 10)
                numeroDaCarta = "jack";
            else if (valor == 11)
                numeroDaCarta = "queen";
            else if (valor == 12)
                numeroDaCarta = "king";
            else
                numeroDaCarta = "" + (valor + 1);                //else if para array embaralhado
            nomeDaCarta = numeroDaCarta + "_of_hearts";
        }
        else if (linha == 3)
        {
            if (valor == 0)
                numeroDaCarta = "ace";
            else if (valor == 10)
                numeroDaCarta = "jack";
            else if (valor == 11)
                numeroDaCarta = "queen";
            else if (valor == 12)
                numeroDaCarta = "king";
            else
                numeroDaCarta = "" + (valor + 1);                //else if para array embaralhado
            nomeDaCarta = numeroDaCarta + "_of_spades";
        }
        else if (linha == 0)
        {
            if (valor == 0)
                numeroDaCarta = "ace";
            else if (valor == 10)
                numeroDaCarta = "jack";
            else if (valor == 11)
                numeroDaCarta = "queen";
            else if (valor == 12)
                numeroDaCarta = "king";
            else
                numeroDaCarta = "" + (valor + 1);                //else if para array embaralhado
            nomeDaCarta = numeroDaCarta + "_of_diamonds";
        }

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        //GameObject.Find("" + rank).GetComponent<Tile>().setCartaOriginal(s1);
        //GameObject.Find("" + valor).GetComponent<Tile>().setCartaOriginal(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1);


    }

    public int[] CriarArrayEmbaralhado()
    {
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };          //criando um array com o numero de cartas
        int temp;
        for(int t=0; t<13; t++)                                                         //funcao para embaralhar o array criado
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);                                            
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<Tile>().RevelaCarta();
        }
        else if(primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            segundaCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<Tile>().RevelaCarta();
            VerificaCarta();
        }

    }

    public void VerificaCarta()
    {
        DisparaTimer();
        numTentativas++;
        UpDateTentativas();
    }

    public void DisparaTimer()
    {
        timerPausado = false;
        timerAcionado = true;
    }

    void UpDateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas: " + numTentativas;
    }

    public void TelaRecord()
    {
        PlayerPrefs.SetInt("Records", recordJogo);
        SceneManager.LoadScene("lab3_Parabens");
    }
}
