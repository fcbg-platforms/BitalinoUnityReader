namespace BitalinoReader
{
    using UnityEngine;
    using System.Collections;

    public class PeakDetector : MonoBehaviour
    {
        public BITvaluesSO BITvaluesSO;

        public float minAmplitude = 0.2f;      // seuil minimum pour considérer un pic
        public float minInterval = 0.3f;       // temps min entre deux pics (en secondes)
        public float currentSignalValue = 0f;  // à mettre à jour avec ton flux (e.g. LSL)

        private float previousValue = 0f;
        private float timeSinceLastPeak = Mathf.Infinity;

        public HeartBeater heartbeatCalculator; // référence au script BPM

        void Update()
        {
            timeSinceLastPeak += Time.deltaTime;

            // Mettez currentSignalValue à jour depuis votre source ici
            currentSignalValue = BITvaluesSO.bitChannels[2] / 100f;
            float signal = currentSignalValue;

            // Détection simple : valeur passe d'ascendante à descendante + amplitude suffisante
            if (previousValue > minAmplitude &&
                previousValue > signal &&                  // maximum local détecté
                timeSinceLastPeak > minInterval)
            {
                // Battement détecté
                heartbeatCalculator.RegisterBeat();
                timeSinceLastPeak = 0f;
            }

            previousValue = signal;
        }
    }
}