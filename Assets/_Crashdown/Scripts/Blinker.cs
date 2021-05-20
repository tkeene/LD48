using System;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public CrashdownEnemyActor enemyActor;
    public SpriteRenderer spriteRenderer;

    public float blinkFor = 0.6f;
    public float blinkCycle = 0.12f;

    private bool _blinking = false;
    private float _blinkRemaining = 0f;
    private float _blinkCycleRemaining = 0f;
    private int _blinkCycleStage = 2;

    const string kBlinkShaderPropertyName = "_DamageTint";
    private int cachedBlinkShaderProperty = 0;

    private void Start()
    {
        spriteRenderer.material = new Material(spriteRenderer.material);
        cachedBlinkShaderProperty = Shader.PropertyToID(kBlinkShaderPropertyName);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyActor.CurrentAiState == CrashdownEnemyActor.EAiState.Dying || enemyActor.CurrentAiState == CrashdownEnemyActor.EAiState.IsDead)
        {
            _blinking = false;
            SetBlinkAmount(0.0f);
            return;
        }

        if (enemyActor.damagedThisFrame)
        {
            _blinkRemaining = blinkFor;
            _blinking = true;
            SetBlinkAmount(_blinkCycleStage);
        }

        if (_blinking)
        {
            _blinkRemaining -= Time.deltaTime;
            if (_blinkRemaining <= 0f)
            {
                _blinkRemaining = 0f;
                _blinking = false;
                SetBlinkAmount(0.0f);
                return;
            }

            _blinkCycleRemaining -= Time.deltaTime;
            if (_blinkCycleRemaining <= 0f)
            {
                _blinkCycleRemaining = blinkCycle;
                _blinkCycleStage = (_blinkCycleStage + 1) % 3;
                SetBlinkAmount(_blinkCycleStage);
            }
        }
    }

    private void SetBlinkAmount(float amount)
    {
        const float kBlinkScale = 0.4f;
        spriteRenderer.material.SetFloat(cachedBlinkShaderProperty, kBlinkScale * amount);
    }
}
