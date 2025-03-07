using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapStacker : MonoBehaviour
{
    public float voxelHeight = 1f; // Adjust to your voxel's height
    public Tilemap currentTilemap;

    public void IncreaseZPosition()
    {
        if (currentTilemap != null)
        {
            Vector3 currentPosition = currentTilemap.transform.position;
            currentPosition.z += voxelHeight;
            currentTilemap.transform.position = currentPosition;
        }
        else
        {
            Debug.LogWarning("No Tilemap selected.");
        }
    }
}