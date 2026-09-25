using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingUIManager : MonoBehaviour
{
    [Header("Top Bar")]
    public TextMeshProUGUI titleText;

    [Header("Instruction Panel")]
    public GameObject instructionPanel;
    public TextMeshProUGUI instructionText;

    [Header("Place Office Button")]
    public GameObject placeOfficeButton;

    [Header("Fire Warning")]
    public GameObject fireWarningPanel;
    public TextMeshProUGUI fireWarningText;

    [Header("Extinguisher Selection")]
    public GameObject extinguisherPanel;
    public Button waterBtn, dryChemBtn, co2Btn;
    public Image waterImg, dryChemImg, co2Img;

    [Header("Feedback")]
    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;

    [Header("Spray UI")]
    public GameObject sprayButton;
    public GameObject reticle;
    public Slider fireProgressBar;

    [Header("Success")]
    public GameObject successPanel;

    void Start()
    {
        if (titleText != null) titleText.text = "FIRE SAFETY TRAINING";
        ShowInstruction("Scan Area\nPoint your camera at the office floor.");

        if (placeOfficeButton != null) placeOfficeButton.SetActive(false);
        if (fireWarningPanel != null) fireWarningPanel.SetActive(false);
        if (extinguisherPanel != null) extinguisherPanel.SetActive(false);
        if (feedbackPanel != null) feedbackPanel.SetActive(false);
        if (sprayButton != null) sprayButton.SetActive(false);
        if (reticle != null) reticle.SetActive(false);
        if (successPanel != null) successPanel.SetActive(false);

        if (waterBtn != null)
            waterBtn.onClick.AddListener(() =>
                FindObjectOfType<ExtinguisherManager>().SelectExtinguisher(ExtinguisherType.Water));
        if (dryChemBtn != null)
            dryChemBtn.onClick.AddListener(() =>
                FindObjectOfType<ExtinguisherManager>().SelectExtinguisher(ExtinguisherType.DryChemical));
        if (co2Btn != null)
            co2Btn.onClick.AddListener(() =>
                FindObjectOfType<ExtinguisherManager>().SelectExtinguisher(ExtinguisherType.CO2));
    }

    public void ShowInstruction(string text)
    {
        if (instructionPanel != null) instructionPanel.SetActive(true);
        if (instructionText != null) instructionText.text = text;
    }

    public void ShowPlaceOfficeButton(bool show)
    {
        if (placeOfficeButton != null) placeOfficeButton.SetActive(show);
        if (show) ShowInstruction("Tap 'Place Office' to generate workspace");
    }

    public void OnPlaceOfficePressed()
    {
        ARPlacementManager apm = FindObjectOfType<ARPlacementManager>();
        OfficeSpawner spawner = FindObjectOfType<OfficeSpawner>();
        if (apm == null || spawner == null) return;
        Pose p = apm.GetPlacementPose();
        spawner.SpawnOffice(p.position, p.rotation);
    }

    public void ShowFireWarning(bool show)
    {
        if (fireWarningPanel != null) fireWarningPanel.SetActive(show);
        if (show)
        {
            if (fireWarningText != null)
                fireWarningText.text = "FIRE DETECTED\nSelect the correct extinguisher";
            if (extinguisherPanel != null) extinguisherPanel.SetActive(true);
            if (instructionPanel != null) instructionPanel.SetActive(false);
        }
    }

    public void ShowExtinguisherFeedback(bool correct)
    {
        if (feedbackPanel != null) feedbackPanel.SetActive(true);
        if (correct)
        {
            if (feedbackText != null)
            {
                feedbackText.text = "Correct Extinguisher";
                feedbackText.color = Color.green;
            }
            if (extinguisherPanel != null) extinguisherPanel.SetActive(false);
            if (instructionPanel != null) instructionPanel.SetActive(true);
            if (instructionText != null)
                instructionText.text = "Aim at the base of the fire\nPress and hold SPRAY";
            if (reticle != null) reticle.SetActive(true);
        }
        else
        {
            if (feedbackText != null)
            {
                feedbackText.text = "Incorrect Extinguisher\nTry Again";
                feedbackText.color = Color.red;
            }
            Invoke(nameof(HideFeedback), 2f);
        }
    }

    void HideFeedback()
    {
        if (feedbackPanel != null) feedbackPanel.SetActive(false);
    }

    public void ShowSprayButton(bool show)
    {
        if (sprayButton != null) sprayButton.SetActive(show);
    }

    public void UpdateFireProgress(float level)
    {
        if (fireProgressBar != null) fireProgressBar.value = level / 100f;
    }

    public void ShowFireExtinguished()
    {
        if (successPanel != null)
        {
            successPanel.SetActive(true);
            TextMeshProUGUI successText = successPanel.GetComponentInChildren<TextMeshProUGUI>();
            if (successText != null)
                successText.text = "FIRE EXTINGUISHED\nTraining completed successfully";
        }
        if (fireWarningPanel != null) fireWarningPanel.SetActive(false);
        if (sprayButton != null) sprayButton.SetActive(false);
        if (reticle != null) reticle.SetActive(false);
        if (instructionPanel != null) instructionPanel.SetActive(false);
    }

    public void OnSprayDown()
    {
        FireSprayController fsc = FindObjectOfType<FireSprayController>();
        if (fsc != null) fsc.StartSpray();
    }

    public void OnSprayUp()
    {
        FireSprayController fsc = FindObjectOfType<FireSprayController>();
        if (fsc != null) fsc.StopSpray();
    }

    public void OnResetScenario()
    {
        TrainingManager tm = FindObjectOfType<TrainingManager>();
        if (tm != null) tm.ResetScenario();
    }

    public void OnRestartTraining() => OnResetScenario();

    public void OnExitAR()
    {
        Application.Quit();
    }
}