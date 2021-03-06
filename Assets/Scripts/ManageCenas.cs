using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageCenas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        int numTentativas = 0;
        int recordJogo = 0;
        PlayerPrefs.SetInt("Jogadas", numTentativas);
        PlayerPrefs.SetInt("Records", recordJogo);
        SceneManager.LoadScene("lab3");
    }

    public void Comeca()
    {
        int numTentativas = 0;
        int recordJogo = 0;
        PlayerPrefs.SetInt("Jogadas", numTentativas);
        PlayerPrefs.SetInt("Records", recordJogo);
        SceneManager.LoadScene("lab3");
    }

    public void Finaliza()
    {
        SceneManager.LoadScene("lab3_Final");
    }

}
