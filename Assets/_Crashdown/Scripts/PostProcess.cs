// Thanks to Linden Reid https://lindenreid.wordpress.com/2018/02/05/camera-shaders-unity/

using UnityEngine;

[ExecuteInEditMode]
public class PostProcess : MonoBehaviour
{

    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
}
