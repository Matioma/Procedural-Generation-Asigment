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
      
        mySubstance.SetInputFloat("TileAmountOfDeterioratedColor", deteriorationAmount);


        mySubstance.QueueForRender();

        Substance.Game.Substance.RenderSubstancesAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
