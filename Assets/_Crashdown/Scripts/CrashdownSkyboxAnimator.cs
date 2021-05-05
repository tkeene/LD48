using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashdownSkyboxAnimator : MonoBehaviour
{
    public Skybox mySkybox;
    public string skyboxSkyColorPropertyName = "_SkyColor";
    public string skyboxHorizonPropertyName = "_HorizonColor";
    public string skyboxCameraOffsetStrengthPropertyName = "_CameraOffsetStrength";
    public string skyboxCameraCurrentOffsetPropertyName = "_CurrentCameraOffset";
    public AnimationCurve skyLinesPulseCurve;
    public float animationPulsePeriod = 8.0f;

    int skyboxCameraCurrentOffsetPropertyId = 0;
    int skyboxHorizonColorPropertyId = 0;
    Color defaultHorizonColor = Color.white;
    int skyboxSkyColorPropertyId = 0;
    Color defaultSkyColor = Color.white;
    float currentAnimationTime = 0.0f;

    private void Awake()
    {
        // Do not modify the project material, that will make changes that could sneak into version control.
        mySkybox.material = new Material(mySkybox.material);

        skyboxHorizonColorPropertyId = Shader.PropertyToID(skyboxHorizonPropertyName);
        defaultHorizonColor = mySkybox.material.GetColor(skyboxHorizonColorPropertyId);
        skyboxSkyColorPropertyId = Shader.PropertyToID(skyboxSkyColorPropertyName);
        defaultSkyColor = mySkybox.material.GetColor(skyboxSkyColorPropertyId);
        skyboxCameraCurrentOffsetPropertyId = Shader.PropertyToID(skyboxCameraCurrentOffsetPropertyName);
    }

    private void LateUpdate()
    {
        currentAnimationTime = Mathf.Repeat(currentAnimationTime + Time.deltaTime / animationPulsePeriod, length: 1.0f);
        mySkybox.material.SetVector(skyboxCameraCurrentOffsetPropertyId, transform.position);
        mySkybox.material.SetColor(skyboxHorizonColorPropertyId, Color.Lerp(defaultSkyColor, defaultHorizonColor, skyLinesPulseCurve.Evaluate(currentAnimationTime)));
    }
}
