using UnityEngine;

public class CameraObstructionHandler : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask obstructionMask; // Set to voxel environment layer
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float smoothSpeed = 10f;

    private float defaultDistance;
    private Vector3 defaultOffset;

    private void Start()
    {
        defaultOffset = cameraTransform.position - player.position;
        defaultDistance = defaultOffset.magnitude;
    }

    private void LateUpdate()
    {
        Vector3 desiredCameraPos = player.position + defaultOffset;
        RaycastHit hit;

        if (Physics.Raycast(player.position, defaultOffset.normalized, out hit, defaultDistance, obstructionMask))
        {
            float hitDistance = Mathf.Clamp(hit.distance - 0.2f, minDistance, defaultDistance);
            Vector3 newPos = player.position + defaultOffset.normalized * hitDistance;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, newPos, Time.deltaTime * smoothSpeed);
        }
        else
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredCameraPos, Time.deltaTime * smoothSpeed);
        }

        cameraTransform.LookAt(player);
    }
}
