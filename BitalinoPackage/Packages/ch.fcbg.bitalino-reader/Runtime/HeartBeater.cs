namespace BitalinoReader
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class HeartBeater : MonoBehaviour
    {
        [SerializeField] BITvaluesSO _bitValues;
        [SerializeField] float smoothSpeed = 5f;

        private float smoothedValue;

        [Space, SerializeField] TMP_Text _heartBeatText;
        [SerializeField] int _heartbeatsOnLastXseconds = 30;

        private List<float> beatTimestamps = new List<float>();
        private float bpm = 0f;

        private void Start()
        {
            StartCoroutine(BpmTextUpdater());
        }

        void Update()
        {
            smoothedValue = Mathf.Lerp(smoothedValue, _bitValues.bitChannels[2] / 100f, Time.deltaTime * smoothSpeed);
            transform.localScale = Vector3.one * smoothedValue;
        }


        // Appelle cette fonction chaque fois qu'un battement est détecté
        public void RegisterBeat()
        {
            float currentTime = Time.time;
            beatTimestamps.Add(currentTime);

            PruneOldBeats();

            // BPM = (nb de battements / durée en secondes) * 60
            float duration = Mathf.Max(currentTime - beatTimestamps[0], 0.1f);
            bpm = (beatTimestamps.Count / duration) * 60f;

            //Debug.Log("BPM: " + bpm.ToString("F1"));
        }

        private void PruneOldBeats()
        {
            float cutoff = Time.time - _heartbeatsOnLastXseconds;
            beatTimestamps.RemoveAll(t => t < cutoff);
        }

        private IEnumerator BpmTextUpdater()
        {
            while (true)
            {
                PruneOldBeats();
                _heartBeatText.text = bpm.ToString("F1") + " bpm";
                yield return new WaitForSeconds(1);
            }
        }
    }
}