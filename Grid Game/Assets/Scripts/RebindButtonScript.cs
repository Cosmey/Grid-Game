using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RebindButtonScript : MonoBehaviour
{
    [SerializeField] private string keyCodeName;
    [SerializeField] private GameObject myText;
    [SerializeField] RebindController myController;
    // Start is called before the first frame update
    void Start()
    {
        UpdateKey();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        myController.StartRebindingMode(keyCodeName, myText);
    }
    private void UpdateKey()
    {
        myText.GetComponent<TextMeshProUGUI>().text = SaveDataScript.GetKeyCode(keyCodeName).ToString();
    }
}
