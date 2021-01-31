
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GuiManager guiManager;

    public Button quitButton;
    public Button resumeButton;
    public Button startOverButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(OnButtonExitClick);
        resumeButton.onClick.AddListener(OnButtonResumeClick);
        startOverButton.onClick.AddListener(OnButtonStartOverClick);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnButtonExitClick()
    {
        //todo
    }

    private void OnButtonResumeClick()
    {
        guiManager.TogglePause();
    }

    private void OnButtonStartOverClick()
    {
        //todox
    }
}
