using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebindButtonScript : MonoBehaviour
{
    [SerializeField] private string keyCodeName;
    [SerializeField] private GameObject myText;
    [SerializeField] RebindController myController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed()
    {
        myController.StartRebindingMode(keyCodeName, myText);
    }
}
