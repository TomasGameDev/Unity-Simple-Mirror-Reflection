using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Mirror : MonoBehaviour
{
    public float reflectionScale = 1f;
    [Space]
    public Transform mainCameraTransform;
    [Space]
    public Camera mirrorCamera;
    [Space]
    public Transform mirrorCenter;

    [Space]
    public MeshRenderer surface;
    public RenderTexture renderTexture;
    public Vector2Int renderTextureSize = new Vector2Int(256, 256);

    private void Update()
    {
        if (renderTexture == null)
        {
            renderTexture = new RenderTexture(renderTextureSize.x, renderTextureSize.y, 16);
            renderTexture.name = "MirrorReflection" + GetInstanceID();
            renderTexture.hideFlags = HideFlags.DontSave;

            mirrorCamera.targetTexture = renderTexture;
            surface.material.mainTexture = renderTexture;
        }
        Vector3 localPos = mirrorCenter.InverseTransformPoint(mainCameraTransform.position) * reflectionScale;
        mirrorCamera.lensShift = new Vector2(-localPos.x, -localPos.y);
        mirrorCamera.focalLength = mirrorCamera.sensorSize.x * localPos.z;//renderCam.sensorSize.x = 12.8
        mirrorCamera.transform.localPosition = Vector3.Reflect(localPos, Vector3.forward);
        mirrorCamera.nearClipPlane = -mirrorCamera.transform.localPosition.z * mirrorCenter.localScale.x;
    }
}
