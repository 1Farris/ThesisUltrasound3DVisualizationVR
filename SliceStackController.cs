using UnityEngine;
using System.Collections.Generic;

public class SliceStackController : MonoBehaviour
{
    public string sliceFolder = "Slices/Neck";
    public GameObject slicePlanePrefab;
    public float sliceSpacing = 0.01f;

    private List<GameObject> slicePlanes = new List<GameObject>();

    void Start()
    {
        GenerateSliceStack();
    }

    void GenerateSliceStack()
    {
        Texture2D[] slices = Resources.LoadAll<Texture2D>(sliceFolder);

        for (int i = 0; i < slices.Length; i++)
        {
            GameObject slicePlane = Instantiate(slicePlanePrefab, transform);
            slicePlane.transform.localPosition = new Vector3(0, 0, i * sliceSpacing);

            Renderer renderer = slicePlane.GetComponent<Renderer>();
            Material mat = new Material(renderer.material);
            mat.mainTexture = slices[i];
            renderer.material = mat;

            slicePlanes.Add(slicePlane);
        }
    }
}
