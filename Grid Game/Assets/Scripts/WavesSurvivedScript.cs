using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WavesSurvivedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("WavesSurvivedNumber").GetComponent<TextMeshProUGUI>().text = WaveController.instance.waveCount+"";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
