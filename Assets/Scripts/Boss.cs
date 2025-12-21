using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public HealthBarOther healthBar;

    [Header("Health")]
    public int maxHealth = 300;
    public int currentHealth;

    [Header("Phase Thresholds")]
    public int phase2Threshold = 200;
    public int phase3Threshold = 80;

    public int currentPhase = 1;

    [Header("Attack Ranges Per Phase")]
    public float phase1AttackRange = 10f;
    public float phase2AttackRange = 15f;
    public float phase3AttackRange = 20f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitClip;

    public Animator anim;
    public event Action<int> OnPhaseChanged;

    public bool isFacingRight = false;
    public bool isDead = false;
    private void Awake()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFacingRight)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = false;
        }
        else if (transform.position.x < player.position.x && !isFacingRight)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = true;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Boss took {amount} damage, HP = {currentHealth}");

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
        }

        CheckPhaseTransition();
        Debug.Log("Boss current phase: " + currentPhase);

        if (currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("Boss isDead: " + isDead);
            anim.SetTrigger("Die");
        }
    }

    private void CheckPhaseTransition()
    {
        if (currentPhase == 1 && currentHealth <= phase2Threshold)
        {
            currentPhase = 2;
            Debug.Log("Boss entering Phase 2");
            OnPhaseChanged?.Invoke(2);
        }
        else if (currentPhase == 2 && currentHealth <= phase3Threshold)
        {
            currentPhase = 3;
            Debug.Log("Boss entering Phase 3");
            OnPhaseChanged?.Invoke(3);
        }
    }

    public float GetCurrentAttackRange()
    {
        switch (currentPhase)
        {
            case 1: return phase1AttackRange;
            case 2: return phase2AttackRange;
            case 3: return phase3AttackRange;
            default: return phase1AttackRange;
        }
    }

    public void PlayHitSound()
    {
        if (audioSource != null && hitClip != null)
        {
            audioSource.PlayOneShot(hitClip);
        }
    }

    private void Die()
    {
        int books = GameManager.Instance.GetBooks();
        Debug.Log($"Books collected: {books}");
        if (books >= 4)
        {
            SceneManager.LoadScene("TrueEnding");
            Debug.Log("True ending triggered.");
        }
        else if (books >= 3)
        {
            SceneManager.LoadScene("NormalEnding");
            Debug.Log("Normal ending triggered.");
        }
        else
        {
            SceneManager.LoadScene("BadEnding");
            Debug.Log("Bad ending triggered.");
        }
        Debug.Log("Boss died.");
        Destroy(gameObject);
    }
    public void OnDeathAnimationFinished()
    {
        Die();
    }
}