using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 100f;
    public LayerMask monsterLayer;
    public ScoreManager scoreManager; // Référence au ScoreManager
    public AudioSource audioSource; // Référence à l'AudioSource
    public AudioClip gunshotSound; // Référence au son du tir
    public Animator animator; // Référence à l'Animator de l'arme

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>(); // Trouver le ScoreManager dans la scène
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>(); // Trouver l'AudioSource sur l'objet
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Trouver l'Animator sur l'objet
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Détecte le clic gauche de la souris
        {
            Shoot();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(gunshotSound); // Jouer le son du tir
        animator.SetTrigger("Shoot"); // Déclencher l'animation de recul

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, monsterLayer))
        {
            if (hit.transform.CompareTag("Monster"))
            {
                Destroy(hit.transform.gameObject); // Supprimer l'objet touché
                if (scoreManager != null)
                {
                    scoreManager.AddScore(1); // Ajouter un point au score
                }
                else
                {
                    Debug.LogWarning("ScoreManager is not assigned or found in the scene.");
                }
            }
            else
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
