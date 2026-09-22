using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Variable untuk reference ke CinemachinachinePanTilt
    [SerializeField]
    private CinemachinePanTilt _panTilt;
    // Variable untuk reference ke CinemachineInputAxisController
    [SerializeField]
    private CinemachineInputAxisController _cameraInput;

    // Property untuk mendapatkan sudut rotasi Pan camera
    public float PanAxis => _panTilt.PanAxis.Value;

    // Function untuk mengaktifkan dan non-aktifkan camera input
    public void SetCameraInputEnabled(bool isActive)
    {
        _cameraInput.enabled = isActive;
    }

    // Function untuk reset rotasi camera
    public void ResetCameraRotation()
    {
        _panTilt.PanAxis.Value = 0;
        _panTilt.TiltAxis.Value = 0;
    }

    // Function untuk mengubah nilai sudut rotasi pan camera
    public void SetPanAxisValue(float panValue)
    {
        _panTilt.PanAxis.Value = panValue;
    }

    // Function untuk mengubah nilai sudut rotasi tilt camera
    public void SetTiltAxisValue(float tiltValue)
    {
        _panTilt.TiltAxis.Value = tiltValue;
    }
}
