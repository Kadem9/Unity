using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public Button playButton;
    public Button creditsButton;
    public Button quitButton;
    public Button backButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        creditsButton.onClick.AddListener(ShowCredits);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(HideCredits);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    void ShowCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    void HideCredits()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
