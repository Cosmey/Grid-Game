using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject currentMenu;
    private KeyCode backKeyCode = KeyCode.Escape;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
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
        if(currentMenu.GetComponent<MenuScript>().GetPreviousMenu() != null)
        {
            currentMenu.SetActive(false);
            currentMenu = currentMenu.GetComponent<MenuScript>().GetPreviousMenu();
            currentMenu.SetActive(true);
        }
        
    }
    public void Exit()
    {
        Application.Quit();
    }
    private void CheckInputs()
    {
        if(Input.GetKeyDown(backKeyCode))
        {
            BackMenu();
        }
    }
}
