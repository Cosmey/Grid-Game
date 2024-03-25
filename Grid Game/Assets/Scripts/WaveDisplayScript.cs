//coded by reece

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveDisplayScript : MonoBehaviour
{
    [SerializeField] private GameObject waveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWaveText(int wave)
    {
        waveText.GetComponent<TextMeshProUGUI>().text = "Wave " + wave;
    }
}
