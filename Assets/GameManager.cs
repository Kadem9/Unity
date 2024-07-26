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
        endGameText.gameObject.SetActive(false); // Assurez-vous que le texte de fin de jeu est caché au début
        defeatPanel.SetActive(false); // Masquer le panneau de défaite par défaut
        victoryPanel.SetActive(false); // Masquer le panneau de victoire par défaut
        pausePanel.SetActive(false); // Masquer le panneau de pause par défaut
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
                    CheckVictory(); // Vérifier si tous les monstres sont éliminés
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
        // Vérifier s'il reste des monstres
        if (GameObject.FindGameObjectsWithTag("Monster").Length == 0)
        {
            ShowVictoryPanel();
        }
    }

    void ShowDefeatPanel()
    {
        gameEnded = true;
        defeatPanel.SetActive(true);
        Time.timeScale = 0f; // Arrêter le jeu
        playerMovement.SetPaused(true); // Désactiver les mouvements du joueur
    }

    void ShowVictoryPanel()
    {
        gameEnded = true;
        victoryPanel.SetActive(true);
        Time.timeScale = 0f; // Arrêter le jeu
        playerMovement.SetPaused(true); // Désactiver les mouvements du joueur
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
        Time.timeScale = 1f; // Redémarrer le jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharger la scène actuelle
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Redémarrer le jeu
        SceneManager.LoadScene("Menu"); // Charger la scène du menu principal
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        playerMovement.SetPaused(false);
    }
}
