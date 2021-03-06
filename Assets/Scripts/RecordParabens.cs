using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RecordParabens : MonoBehaviour
{

    public int recordJogo;


    // Start is called before the first frame update
    void Start()
    {
        recordJogo = PlayerPrefs.GetInt("Records");
        GameObject.Find("parabens").GetComponent<Text>().text = "PARABÉNS, SEU NOVO RECORD É DE: " + recordJogo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continua()
    {
        recordJogo = PlayerPrefs.GetInt("Records");
        PlayerPrefs.SetInt("Jogadas", recordJogo);
        PlayerPrefs.SetInt("Records", recordJogo);

        SceneManager.LoadScene("lab3");
    }

}
