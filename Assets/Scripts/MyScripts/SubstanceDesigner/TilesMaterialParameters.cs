using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMaterialParameters : MonoBehaviour
{
    [SerializeField, Range(0,1)]
    float valueDeterioration;

    public Substance.Game.SubstanceGraph mySubstance;

    private void OnValidate()
    {

        //Debug.Log(mySubstance.GetInputFloat("TileAmountOfDeterioratedColor"));
        mySubstance.SetInputFloat("TileAmountOfDeterioratedColor", valueDeterioration);
        //Debug.Log(mySubstance.GetInputFloat("TileAmountOfDeterioratedColor"));
        //mySubstance.SetInputColor("BrickColor", color);
        mySubstance.QueueForRender();

        Substance.Game.Substance.RenderSubstancesAsync();
    }
}
