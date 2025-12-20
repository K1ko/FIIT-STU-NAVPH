using UnityEngine;
using UnityEngine.UI; // or TMPro if using TMP_Dropdown
using System.Linq;

public class QualitySettingsUI : MonoBehaviour
{
    public Dropdown qualityDropdown;

    void Start()
    {
        // Get available quality levels and populate dropdown
        qualityDropdown.ClearOptions();
        var options = QualitySettings.names.ToList();
        qualityDropdown.AddOptions(options);

        // Set current quality level
        int currentLevel = QualitySettings.GetQualityLevel();
        qualityDropdown.value = currentLevel;
        qualityDropdown.RefreshShownValue();

        // Listen for changes
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true); // Apply immediately
        Debug.Log($"Quality set to: {QualitySettings.names[index]} (index {index})");
    }
}
