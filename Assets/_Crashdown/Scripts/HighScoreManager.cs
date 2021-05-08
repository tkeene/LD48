using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighScoreManager : MonoBehaviour
{
    public float timeToHoldInteractButton = 1.0f;
    public TMPro.TextMeshProUGUI highScoreOutput;
    public string victoryMessage = "CorpsCore.exe not found, system data unrecoverable.";
    public string gameOverMessage = "Virus Crashdown.exe detcted and quarantined. Game over n00b!";
    public int rollingOutputSpeedInFrames = 5;
    public string randomCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    public int numberOfAdditionalCharactersToRandomize = 20;

    public PostProcess glitchRenderer;
    public Material[] glitchRendererStages;
    public float glitchRendererTimeBetweenStages = 0.7f;

    private Controls _controls;

    private bool interactIsHeld = false;
    private float currentHeldTime = 0.0f;

    private string originalHighScoreString = "NULL";
    private int currentRollingOutputCounter;
    private string formatVictoryOrGameOverMessage = "NULL";
    private string formatEnemiesKilled = "NULL";
    private string formatBossesKilled = "FF";
    private string formatSecretsFound = "FF";
    private string formatTimeTaken = "FF.FF.FF";
    private string formatWeaponUsed = "404";
    private string formatTextColor = "white";

    private int glitchRendererCurrentStage = 0;
    private float glitchRendererCurrentTime = 0.0f;

    private bool isDoneWithHighScoreDisplay = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;

        glitchRenderer.material = glitchRendererStages[0];
        glitchRenderer.enabled = true;
    }

    private void OnEnable()
    {
        if (_controls == null)
        {
            _controls = new Controls();
        }

        _controls.Player.Interact.performed += OnInteractDown;
        _controls.Player.Interact.canceled += OnInteractUp;
        _controls.Player.Interact.Enable();

        originalHighScoreString = highScoreOutput.text;
        if (CrashdownGameRoot.TotalBossesKilled > 0)
        {
            formatVictoryOrGameOverMessage = victoryMessage;
            formatTextColor = "green";
        }
        else
        {
            formatVictoryOrGameOverMessage = gameOverMessage;
            formatTextColor = "#f88";
        }

        formatEnemiesKilled = CrashdownGameRoot.TotalEnemiesKilled.ToString();
        while (formatEnemiesKilled.Length < 4)
        {
            formatEnemiesKilled = "0" + formatEnemiesKilled;
        }

        formatBossesKilled = CrashdownGameRoot.TotalBossesKilled.ToString();
        while (formatBossesKilled.Length < 2)
        {
            formatBossesKilled = "0" + formatBossesKilled;
        }

        Debug.LogError("TODO: Track secrets");
        //formatSecretsFound = ;

        float timeMinutes = Mathf.Floor(CrashdownGameRoot.TotalTimeUsed / 60.0f);
        float timeSeconds = Mathf.Repeat(CrashdownGameRoot.TotalTimeUsed, 60.0f);
        formatTimeTaken = string.Format("{0:00.}.{1:00.00}", timeMinutes, timeSeconds);

        if (CrashdownGameRoot.FinalWeaponUsed != null)
        {
            formatWeaponUsed = CrashdownGameRoot.FinalWeaponUsed;
        }
    }

    private void OnDisable()
    {
        _controls.Player.Interact.performed -= OnInteractDown;
        _controls.Player.Interact.canceled -= OnInteractUp;
        _controls.Player.Interact.Disable();
    }

    private void OnInteractDown(InputAction.CallbackContext obj)
    {
        interactIsHeld = true;
    }

    private void OnInteractUp(InputAction.CallbackContext obj)
    {
        interactIsHeld = false;
        currentHeldTime = 0.0f;
    }

    void Update()
    {
        if (interactIsHeld)
        {
            if (currentHeldTime > timeToHoldInteractButton)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            else
            {
                currentHeldTime += Time.deltaTime;
            }
        }

        if (!isDoneWithHighScoreDisplay)
        {
            if (Time.frameCount % rollingOutputSpeedInFrames == 0)
            {
                currentRollingOutputCounter++;
            }

            string victoryOrGameOverMessageToUse = formatVictoryOrGameOverMessage;
            if (currentRollingOutputCounter >= victoryOrGameOverMessageToUse.Length)
            {
                isDoneWithHighScoreDisplay = true;
            }
            else
            {
                int amountToGarble = victoryOrGameOverMessageToUse.Length - currentRollingOutputCounter;
                if (amountToGarble > 0)
                {
                    string garbageCharcaters = GenerateGarbageString(amountToGarble);
                    victoryOrGameOverMessageToUse = victoryOrGameOverMessageToUse.Substring(0, currentRollingOutputCounter) + garbageCharcaters;
                }
            }
            string outputText = string.Format(originalHighScoreString, victoryOrGameOverMessageToUse,
                formatEnemiesKilled, formatSecretsFound, formatBossesKilled, formatTimeTaken, formatWeaponUsed,
                formatTextColor);
            if (!isDoneWithHighScoreDisplay)
            {
                int lengthOfOutput = outputText.Length;
                for (int i = 0; i < numberOfAdditionalCharactersToRandomize; i++)
                {
                    int target = UnityEngine.Random.Range(0, lengthOfOutput);
                    if (!Char.IsWhiteSpace(outputText[target]))
                    {
                        outputText = outputText.Remove(target, 1);
                        outputText = outputText.Insert(target, GenerateGarbageString(1));
                    }
                }
            }

            highScoreOutput.text = outputText;
        }

        if (glitchRendererCurrentStage < glitchRendererStages.Length)
        {
            glitchRendererCurrentTime += Time.deltaTime;
            if (glitchRendererCurrentTime > glitchRendererTimeBetweenStages)
            {
                glitchRendererCurrentTime = 0.0f;
                glitchRendererCurrentStage++;
                if (glitchRendererCurrentStage < glitchRendererStages.Length)
                {
                    glitchRenderer.material = glitchRendererStages[glitchRendererCurrentStage];
                }
            }
        }
        else
        {
            glitchRenderer.enabled = false;
        }
    }

    public string GenerateGarbageString(int length)
    {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        for (int i = 0; i < length; i++)
        {
            int indexToUse = UnityEngine.Random.Range(0, randomCharacters.Length);
            stringBuilder.Append(randomCharacters[indexToUse]);
        }
        return stringBuilder.ToString();
    }
}
