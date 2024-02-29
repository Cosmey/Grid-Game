using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreenManagerScript : MonoBehaviour
{
    private static int wavesSurvived;
    [SerializeField] private GameObject wavesSurvivedText;
    // Start is called before the first frame update
    void Start()
    {
        wavesSurvivedText.GetComponent<TextMeshProUGUI>().text = "" + wavesSurvived;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWavesSurvived(int survived)
    {
        wavesSurvived = survived;
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
