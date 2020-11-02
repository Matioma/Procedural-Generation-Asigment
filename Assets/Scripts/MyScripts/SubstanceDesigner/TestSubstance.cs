using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;
using System;

public class TestSubstance : MonoBehaviour
{
    [SerializeField]
    Color color;
    [SerializeField]
    float scale;


    public Substance.Game.SubstanceGraph mySubstance;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(scale);
        mySubstance.SetInputFloat("BrickSize", scale);
        mySubstance.SetInputColor("BrickColor", color);
        mySubstance.QueueForRender();
    }
}
