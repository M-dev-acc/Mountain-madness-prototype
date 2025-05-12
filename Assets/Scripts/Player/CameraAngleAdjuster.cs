using UnityEngine;
using Cinemachine;

public class CameraAngleAdjuster : MonoBehaviour
{
    public Transform player;
    public CinemachineFreeLook freeLookCam;
    public float minDistance = 2f;
    public float maxTilt = 60f; // More top-down when close
    public float defaultTilt = 30f;
    public float lerpSpeed = 5f;

    void Update()
    {
        float distance = Vector3.Distance(freeLookCam.transform.position, player.position);
        float targetTilt = distance < minDistance ? maxTilt : defaultTilt;

        // Lerp middle rig tilt for smoother transition
        var middleRig = freeLookCam.m_Orbits[1];
        middleRig.m_Height = Mathf.Lerp(middleRig.m_Height, targetTilt, Time.deltaTime * lerpSpeed);

        freeLookCam.m_Orbits[1] = middleRig;
    }
}
