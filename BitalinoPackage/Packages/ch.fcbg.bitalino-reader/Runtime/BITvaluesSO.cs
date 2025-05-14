using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bitData_", menuName = "BitData/BITvalueSO", order = 1)]

public class BITvaluesSO : ScriptableObject
{
    public bool isStreamingDATA; //is there any data received
    public float[] bitChannels;
    public float edaValue;
    public float tsValue;
    public float pulseValue;
    public float[] bitChannels_old;

    public int pulsePin, edaPin;



    public float edaAverageValue;
    public float edaCount;
    public float edaValues;
    public float edaMax;
    public float edaMin;
    public List<float> edaChannels;



    [Range(0f, 100f)]
    public float cursorSO; // Valeur entre 0 et 100

}
