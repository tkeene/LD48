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

    // Update is called once per frame
    void Update()
    {
        if (enemyActor.CurrentAiState == CrashdownEnemyActor.EAiState.Dying || enemyActor.CurrentAiState == CrashdownEnemyActor.EAiState.IsDead)
        {
            _blinking = false;
            spriteRenderer.enabled = true;
            return;
        }

        if (enemyActor.damagedThisFrame)
        {
            _blinkRemaining = blinkFor;
            _blinking = true;
            spriteRenderer.enabled = _blinkCycleStage != 0;
        }

        if (_blinking)
        {
            _blinkRemaining -= Time.deltaTime;
            if (_blinkRemaining <= 0f)
            {
                _blinkRemaining = 0f;
                _blinking = false;
                spriteRenderer.enabled = true;
                return;
            }

            //_blinkCycleRemaining -= Time.deltaTime;
            //if (_blinkCycleRemaining <= 0f)
            //{
            //    _blinkCycleRemaining = blinkCycle;
            //    _blinkCycleStage = (_blinkCycleStage+1)%3;
            //    spriteRenderer.enabled = _blinkCycleStage == 0;
            //}
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
}
