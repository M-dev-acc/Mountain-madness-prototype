using UnityEngine;
using System.Collections.Generic;

public class CameraObstructionHandler : MonoBehaviour
{
    public float sphereRadius;

    [Tooltip("Transparency level of obstructing objects.")]
    [Range(0.1f, 1f)]
    public float transparency = 0.3f;

    [Tooltip("Layers that can obstruct the view.")]
    public LayerMask obstructionMask;

    private Transform player;
    private RaycastHit[] rayHits = new RaycastHit[20];

    private Dictionary<Renderer, Material[]> originalMaterials = new();
    private HashSet<Renderer> currentlyTransparent = new();
    private List<Renderer> frameHits = new();

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogError("Player not found. Please tag your player GameObject as 'Player'.");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        HandleObstruction();
    }

    void HandleObstruction()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        frameHits.Clear();

        // Replace ray with spherecast: small radius like 0.5
        float sphereRadius = 0.5f;
        int hitCount = Physics.SphereCastNonAlloc(transform.position, sphereRadius, direction, rayHits, distance, obstructionMask);

        for (int i = 0; i < hitCount; i++)
        {
            RaycastHit hit = rayHits[i];
            Collider hitCollider = hit.collider;
            // ✅ Skip if the player is inside the collider (e.g., standing on or touching it)
            if (hitCollider.bounds.Contains(player.position))
                continue;

            // ✅ Skip if the collider is very close to the player (like platforms or blocks near feet)
            if (Vector3.Distance(hitCollider.ClosestPoint(player.position), player.position) < 0.5f)
                continue;

            Renderer rend = hit.collider.GetComponentInChildren<Renderer>();

            if (rend != null)
            {
                frameHits.Add(rend);

                if (!currentlyTransparent.Contains(rend))
                {
                    MakeTransparent(rend);
                    currentlyTransparent.Add(rend);
                }
            }
        }

        // Restore non-obstructing objects
        List<Renderer> toRestore = new();
        foreach (Renderer rend in currentlyTransparent)
        {
            if (!frameHits.Contains(rend))
            {
                RestoreOriginal(rend);
                toRestore.Add(rend);
            }
        }

        foreach (Renderer rend in toRestore)
        {
            currentlyTransparent.Remove(rend);
        }
    }

    void MakeTransparent(Renderer rend)
    {
        if (!originalMaterials.ContainsKey(rend))
        {
            originalMaterials[rend] = rend.materials;
        }

        foreach (Material mat in rend.materials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color col = mat.color;
                col.a = transparency;
                mat.color = col;

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            }
        }
    }

    void RestoreOriginal(Renderer rend)
    {
        if (originalMaterials.TryGetValue(rend, out Material[] originalMats))
        {
            Material[] currentMats = rend.materials;

            for (int i = 0; i < currentMats.Length; i++)
            {
                if (currentMats[i].HasProperty("_Color"))
                {
                    Color col = currentMats[i].color;
                    col.a = 1f;
                    currentMats[i].color = col;

                    currentMats[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    currentMats[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    currentMats[i].SetInt("_ZWrite", 1);
                    currentMats[i].DisableKeyword("_ALPHABLEND_ON");
                    currentMats[i].EnableKeyword("_ALPHATEST_ON");
                    currentMats[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                }
            }
        }
    }
}
