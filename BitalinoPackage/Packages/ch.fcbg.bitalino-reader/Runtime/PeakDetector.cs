namespace BitalinoReader
{
    using UnityEngine;
    using System.Collections;

    public class PeakDetector : MonoBehaviour
    {
        public BITvaluesSO BITvaluesSO;

        public float minAmplitude = 0.2f;      // seuil minimum pour consid�rer un pic
        public float minInterval = 0.3f;       // temps min entre deux pics (en secondes)
        public float currentSignalValue = 0f;  // � mettre � jour avec ton flux (e.g. LSL)

        private float previousValue = 0f;
        private float timeSinceLastPeak = Mathf.Infinity;

        public HeartBeater heartbeatCalculator; // r�f�rence au script BPM

        void Update()
        {
            timeSinceLastPeak += Time.deltaTime;

            // Mettez currentSignalValue � jour depuis votre source ici
            currentSignalValue = BITvaluesSO.bitChannels[2] / 100f;
            float signal = currentSignalValue;

            // D�tection simple : valeur passe d'ascendante � descendante + amplitude suffisante
            if (previousValue > minAmplitude &&
                previousValue > signal &&                  // maximum local d�tect�
                timeSinceLastPeak > minInterval)
            {
                // Battement d�tect�
                heartbeatCalculator.RegisterBeat();
                timeSinceLastPeak = 0f;
            }

            previousValue = signal;
        }
    }
}