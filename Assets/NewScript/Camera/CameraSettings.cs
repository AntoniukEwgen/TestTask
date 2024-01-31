using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Settings/CameraSettings")]
public class CameraSettings : ScriptableObject, ICameraSettings
{
    public float Speed;
    public float RotationSpeed;

    float ICameraSettings.Speed => Speed;
    float ICameraSettings.RotationSpeed => RotationSpeed;
}
