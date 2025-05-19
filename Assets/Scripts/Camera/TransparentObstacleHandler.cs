using UnityEngine;
using System.Collections.Generic;

public class TransparentObstacleHandler : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleMask;
    public float transparency = 0.3f;
    public float checkRadius = 0.2f;

    private List<Renderer> currentObstacles = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void LateUpdate()
    {
        RestoreObstacles();

        Vector3 direction = player.position - transform.position;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit[] hits = Physics.SphereCastAll(ray, checkRadius, direction.magnitude, obstacleMask);

        foreach (RaycastHit hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null && !currentObstacles.Contains(rend))
            {
                currentObstacles.Add(rend);

                if (!originalMaterials.ContainsKey(rend))
                    originalMaterials[rend] = rend.materials;

                MakeTransparent(rend);
            }
        }
    }

    void RestoreObstacles()
    {
        foreach (Renderer rend in currentObstacles)
        {
            if (rend == null) continue;
            Material[] mats = rend.materials;

            for (int i = 0; i < mats.Length; i++)
            {
                mats[i].SetFloat("_Mode", 0); // Opaque
                mats[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mats[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mats[i].SetInt("_ZWrite", 1);
                mats[i].DisableKeyword("_ALPHABLEND_ON");
                mats[i].renderQueue = -1;
                Color c = mats[i].color;
                c.a = 1f;
                mats[i].color = c;
            }
        }

        currentObstacles.Clear();
    }

    void MakeTransparent(Renderer rend)
    {
        Material[] mats = rend.materials;

        foreach (Material mat in mats)
        {
            mat.SetFloat("_Mode", 3); // Transparent
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.renderQueue = 3000;

            Color c = mat.color;
            c.a = transparency;
            mat.color = c;
        }
    }
}
