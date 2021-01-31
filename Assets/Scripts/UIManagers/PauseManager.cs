
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Application.Quit();
    }

    private void OnButtonResumeClick()
    {
        guiManager.TogglePause();
    }

    private void OnButtonStartOverClick()
    {
        guiManager.TogglePause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
