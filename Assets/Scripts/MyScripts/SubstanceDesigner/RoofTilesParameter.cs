using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofTilesParameter : MonoBehaviour
{

    public Substance.Game.SubstanceGraph mySubstance;

    [SerializeField, Range(0,1)]
    float deteriorationAmount; 
    void Start()
    {
        Debug.Log(mySubstance.GetInputFloat("TileAmountOfDeterioratedColor"));
        mySubstance.SetInputFloat("TileAmountOfDeterioratedColor", deteriorationAmount);
        Debug.Log(mySubstance.GetInputFloat("TileAmountOfDeterioratedColor"));

        mySubstance.QueueForRender();

        Substance.Game.Substance.RenderSubstancesAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
