using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateScreenManager : MonoBehaviour
{
    public GuiManager guiManager;
    private PlayerStats playerStats;

    private State currentState;

    public Button quitButton;
    public Button resumeButton;
    public Button startOverButton;

    public Text title;
    public Canvas actual;

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(OnButtonQuitClick);
        resumeButton.onClick.AddListener(OnButtonResumeClick);
        startOverButton.onClick.AddListener(OnButtonStartOverClick);

        playerStats = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerStats>();
    }

    public void Summon(State state)
    {
        currentState = state;
        switch (currentState)
        {
            case State.PAUSE:
                resumeButton.gameObject.SetActive(true);
                title.text = "Paused";
                break;
            case State.DEATH:
                resumeButton.gameObject.SetActive(false);
                title.text = "Game Over";
                break;
            case State.WIN:
                resumeButton.gameObject.SetActive(false);
                title.text = "You won!";
                break;
        }

        TogglePause(currentState == State.OFF);
    }

    private void TogglePause(bool isScreenDisabled)
    {
        Time.timeScale = isScreenDisabled ? 1f : 0f;

        if(playerStats == null)
            playerStats = GameObject
                .FindGameObjectWithTag("Player")
                .GetComponent<PlayerStats>();

        if (!isScreenDisabled)
            playerStats.Freeze();
        else
            playerStats.Unfreeze();

        actual.gameObject.SetActive(!isScreenDisabled);
    }

    public State CurrentState() => currentState;

    private void OnButtonQuitClick()
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

    public enum State
    {
        OFF,
        PAUSE,
        DEATH,
        WIN,
    }
}
