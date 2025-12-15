using UnityEngine;
using System;

public class Boss : MonoBehaviour
{
    [Header("References")]
    public Transform player;

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

    public event Action<int> OnPhaseChanged;

    public bool isFacingRight = false;

    private void Awake()
    {
        currentHealth = maxHealth;
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

        CheckPhaseTransition();

        if (currentHealth <= 0)
        {
            Die();
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

    private void Die()
    {
        Debug.Log("Boss died.");
        // TODO: add death animation, etc.
    }
}
