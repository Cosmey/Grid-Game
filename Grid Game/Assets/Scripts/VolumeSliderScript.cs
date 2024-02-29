using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    [SerializeField] private string volumeName;
    // Start is called before the first frame update
    void Start()
    {
        GetSaveValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ValueUpdated()
    {
        SaveDataScript.SetVolume(volumeName, GetComponent<Slider>().value);
    }
    private void GetSaveValue()
    {
        GetComponent<Slider>().value = SaveDataScript.GetVolume(volumeName);
    }
}
