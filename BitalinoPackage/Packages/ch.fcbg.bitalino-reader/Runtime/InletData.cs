namespace BitalinoReader
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using LSL4Unity.Utils;


    public class InletData : AFloatInlet
    {
        [Header("Specific datas for your App")]
        [SerializeField]
        private BITvaluesSO bitValues;

        void Reset()
        {
            StreamName = "OpenSignals";
            StreamType = "84:BA:20:AE:BC:07";
        }

        protected override void OnStreamAvailable()
        {
            if (ChannelCount == 3)
                bitValues.isStreamingDATA = true;

            else
            {
                bitValues.isStreamingDATA = false;
                Debug.Log("rien n'est lu par l'app");
            }

        }

        protected override void Process(float[] newSample, double timestamp)
        {
            bitValues.bitChannels_old = bitValues.bitChannels;
            bitValues.bitChannels = newSample;
            bitValues.tsValue = newSample[0];
            bitValues.edaValue = newSample[bitValues.edaPin];
            bitValues.pulseValue = newSample[bitValues.pulsePin];
            //Debug.Log("en cours de stream");

            if (bitValues.isStreamingDATA) { }
            //Debug.Log("ts = " + newSample[0] + " EDA = " + newSample[1] + " Pulse = " + newSample[2]);
        }
    }
}