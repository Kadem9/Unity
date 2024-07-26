using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    public float range = 100f;
    public LayerMask monsterLayer;
    public ScoreManager scoreManager; // R�f�rence au ScoreManager
    public AudioSource audioSource; // R�f�rence � l'AudioSource
    public AudioClip gunshotSound; // R�f�rence au son du tir
    public Animator animator; // R�f�rence � l'Animator de l'arme

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (scoreManager == null)
        {
            scoreManager = FindObjectOfType<ScoreManager>(); // Trouver le ScoreManager dans la sc�ne
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
        if (Input.GetMouseButtonDown(0)) // D�tecte le clic gauche de la souris
        {
            Shoot();
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(gunshotSound); // Jouer le son du tir
        animator.SetTrigger("Shoot"); // D�clencher l'animation de recul

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range, monsterLayer))
        {
            if (hit.transform.CompareTag("Monster"))
            {
                Destroy(hit.transform.gameObject); // Supprimer l'objet touch�
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
