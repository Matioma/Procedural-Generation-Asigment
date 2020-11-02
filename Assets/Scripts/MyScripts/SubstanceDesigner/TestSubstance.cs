﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;
using System;

public class TestSubstance : MonoBehaviour
{
    [SerializeField]
    Color color;
    [SerializeField]
    float valueDeterioration;


    public Substance.Game.SubstanceGraph mySubstance;

  

    private void OnValidate()
    {
        Debug.Log(mySubstance.GetInputFloat("TileAmountOfDeterioratedColor"));
        mySubstance.SetInputFloat("TileAmountOfDeterioratedColor", valueDeterioration);
        Debug.Log(mySubstance.GetInputFloat("TileAmountOfDeterioratedColor"));
        //mySubstance.SetInputColor("BrickColor", color);
        mySubstance.QueueForRender();

        Substance.Game.Substance.RenderSubstancesAsync();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
