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

    private bool isDoneWithHighScoreDisplay = false;

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
            highScoreOutput.text = string.Format(originalHighScoreString, formatVictoryOrGameOverMessage,
                formatEnemiesKilled, formatSecretsFound, formatBossesKilled, formatTimeTaken, formatWeaponUsed,
                formatTextColor);
            isDoneWithHighScoreDisplay = true;
        }
    }
}
