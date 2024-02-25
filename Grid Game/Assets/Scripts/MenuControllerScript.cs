using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    private GameObject currentMenu;
    private GameObject previousMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Single);
    }
    public void SetMenu(GameObject menu)
    {
        currentMenu.SetActive(false);
        currentMenu = menu;
        currentMenu.SetActive(true);
    }
    public void BackMenu()
    {
        currentMenu.SetActive(false);
        currentMenu = previousMenu;
        currentMenu.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
