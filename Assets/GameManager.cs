using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameDuration = 30f;
    private float timeRemaining;
    public TMP_Text timeText; // Utilisez TMP_Text pour TextMeshPro
    public TMP_Text endGameText; // Texte de fin de jeu pour afficher "Victoire" ou "Game Over"
    public ScoreManager scoreManager;
    public GameObject defeatPanel;
    public GameObject victoryPanel;
    public GameObject pausePanel;
    public PlayerMovement playerMovement;

    private bool gameEnded = false;
    private bool isPaused = false;

    void Start()
    {
        timeRemaining = gameDuration;
        endGameText.gameObject.SetActive(false); // Assurez-vous que le texte de fin de jeu est cach� au d�but
        defeatPanel.SetActive(false); // Masquer le panneau de d�faite par d�faut
        victoryPanel.SetActive(false); // Masquer le panneau de victoire par d�faut
        pausePanel.SetActive(false); // Masquer le panneau de pause par d�faut
    }

    void Update()
    {
        if (!gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.P)) // Touche pour mettre en pause
            {
                TogglePause();
            }

            if (!isPaused)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    UpdateTimeText();
                    CheckVictory(); // V�rifier si tous les monstres sont �limin�s
                }
                else
                {
                    ShowDefeatPanel();
                }
            }
        }
    }

    void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + Mathf.Floor(timeRemaining).ToString();
        }
    }

    void CheckVictory()
    {
        // V�rifier s'il reste des monstres
        if (GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            ShowVictoryPanel();
        }
    }

    void ShowDefeatPanel()
    {
        gameEnded = true;
        defeatPanel.SetActive(true);
        Time.timeScale = 0f; // Arr�ter le jeu
        playerMovement.SetPaused(true); // D�sactiver les mouvements du joueur
    }

    void ShowVictoryPanel()
    {
        gameEnded = true;
        victoryPanel.SetActive(true);
        Time.timeScale = 0f; // Arr�ter le jeu
        playerMovement.SetPaused(true); // D�sactiver les mouvements du joueur
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
        playerMovement.SetPaused(isPaused);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Red�marrer le jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharger la sc�ne actuelle
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Red�marrer le jeu
        SceneManager.LoadScene("Menu"); // Charger la sc�ne du menu principal
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        playerMovement.SetPaused(false);
    }
}
