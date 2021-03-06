﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false;      //indicador da carta virada ou não
    public Sprite originalCarta;           // Sprite da carta desejada
    public Sprite backCarta;                 // Sprite do Avesso

    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        print("Vou pressionou num Tile");
        /*if (tileRevelada)
            EscondeCarta();
        else
            RevelaCarta();*/            //aqui não guarda as cartas

        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);

    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
            tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;

    }

    
}
