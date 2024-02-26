using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebindController : MonoBehaviour
{
    private bool isRebinding;
    private string currentRebindName;
    [SerializeField] private GameObject rebindBackground;
    private GameObject rebindButtonText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RebindingMode();
    }

    public void StartRebindingMode(string keyCodeName,GameObject rebindText)
    {
        rebindBackground.SetActive(true);
        isRebinding = true;
        currentRebindName = keyCodeName;
        rebindButtonText = rebindText;
    }

    private void RebindingMode()
    {
        if(isRebinding)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    isRebinding = false;
                    rebindBackground.SetActive(false);
                    SaveDataScript.SetKeyCode(currentRebindName, kcode);
                    rebindButtonText.GetComponent<TextMeshProUGUI>().text = kcode.ToString();
                }

            }
        }
        
    }
}
