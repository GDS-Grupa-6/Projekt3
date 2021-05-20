using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GhostSkinVFX : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skineMesh;
    [SerializeField] private VisualEffect vfxGraph;
    [SerializeField] private float refreshRate;

    private void Start()
    {
        StartCoroutine(UpdateVFXGraphCourutine());
    }

    IEnumerator UpdateVFXGraphCourutine()
    {
        while (gameObject.activeSelf)
        {
            Mesh m = new Mesh();
            skineMesh.BakeMesh(m);

            Vector3[] vertices = m.vertices;
            Mesh m2 = new Mesh();
            m2.vertices = vertices;

            vfxGraph.SetMesh("Mesh", m2);

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
