using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManagerScript : MonoBehaviour
{
    public GameObject tutorialText;
    private List<TutorialStep> tutorial;
    private int currentStep;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }
    private void Setup()
    {
        tutorial = new List<TutorialStep>();
        currentStep = 0;
        tutorial.Add(new TutorialStep("Welcome Really Good Grid Game", 2));
        tutorial.Add(new TutorialStep("Left Click any white square to place a tower",KeyCode.Mouse0, 1));
        KeyCode leftKeyCode = SaveDataScript.GetKeyCode("moveLeftKeyCode");
        tutorial.Add(new TutorialStep("Press " + leftKeyCode.ToString() + " to move left", leftKeyCode, 0.2f));
        KeyCode rightKeyCode = SaveDataScript.GetKeyCode("moveRightKeyCode");
        tutorial.Add(new TutorialStep("Press " + rightKeyCode.ToString() + " to move right", rightKeyCode, 0.2f));
        KeyCode upKeyCode = SaveDataScript.GetKeyCode("moveUpKeyCode");
        tutorial.Add(new TutorialStep("Press " + upKeyCode.ToString() + " to move up", upKeyCode, 0.2f));
        KeyCode downKeyCode = SaveDataScript.GetKeyCode("moveDownKeyCode");
        tutorial.Add(new TutorialStep("Press " + downKeyCode.ToString() + " to move down", downKeyCode, 0.2f));

        KeyCode cycleTowersKeyCode = SaveDataScript.GetKeyCode("cycleTowerKeyCode");
        tutorial.Add(new TutorialStep("Press " + cycleTowersKeyCode.ToString() + " to cycle between towers to place", cycleTowersKeyCode, 2));

        tutorial.Add(new TutorialStep("use scroll wheel to zoom in and out", 2));

        tutorial.Add(new TutorialStep("Prepare for incoming wave in 10", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 9", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 8", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 7", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 6", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 5",  1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 4", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 3", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 2", 1));
        tutorial.Add(new TutorialStep("Prepare for incoming wave in 1", 1));

        SetStep();
    }

    // Update is called once per frame
    void Update()
    {
        TutorialOperations();
    }

    private void TutorialOperations()
    {
        if (tutorial[currentStep].CheckCompleted())
        {
            currentStep++;
            SetStep();
        }
    }

    private void SetStep()
    {
        if(currentStep >= tutorial.Count)
        {
            WaveController.StartWaves();
            tutorialText.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            tutorialText.SetActive(true);
            tutorialText.GetComponent<TextMeshProUGUI>().text = tutorial[currentStep].instructions;
        }
        
    }
}

public class TutorialStep
{
    public string instructions;
    public KeyCode keybind;
    public float duration;
    private bool isTimer;

    public TutorialStep(string newInstructions,KeyCode newKeybind,float newDuration)
    {
        instructions = newInstructions;
        keybind = newKeybind;
        duration = newDuration;
    }
    public TutorialStep(string newInstructions, float newDuration)
    {
        isTimer = true;
        instructions = newInstructions;
        duration = newDuration;
    }
    private bool Count()
    {
        if(isTimer)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckKeybind()
    {
       if(Input.GetKey(keybind))
       {
            if(duration != 0)
            {
                isTimer = true;
                return false;
            }
            else
            {
                return true;
            }
       }
        return false;
    }
    public bool CheckCompleted()
    {
        return (Count() || CheckKeybind());
    }

}
