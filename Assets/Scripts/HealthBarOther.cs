using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarOther : MonoBehaviour
{
    [SerializeField] private float timeToDecrease = 0.25f;
    [SerializeField] private Gradient healthGradient;
    private Image _image;

    private float targetFill=1f;

    private Color newHealthBarColor;

    private Coroutine drainHealthCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<Image>();
        // set health bar to full on start
        _image.color = healthGradient.Evaluate(targetFill);
        CheckHealthColor();
        
    }

    // Update is called once per frame
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        targetFill = currentHealth / maxHealth;
        drainHealthCoroutine = StartCoroutine(DrainHealthBar());
        CheckHealthColor();
    }

    private IEnumerator DrainHealthBar()
    {
        float fillAmount = _image.fillAmount;
        Color currentColor = _image.color;

        float elapsed = 0f;
        while (elapsed < timeToDecrease)
        {
            elapsed += Time.deltaTime;
            // Lerp fill amount
            _image.fillAmount = Mathf.Lerp(fillAmount, targetFill, elapsed / timeToDecrease);
            // Lerp color
            _image.color = Color.Lerp(currentColor, newHealthBarColor, elapsed / timeToDecrease);
            yield return null;
        }
        
    }

    private void CheckHealthColor()
    {
        newHealthBarColor = healthGradient.Evaluate(targetFill);
    }
}
