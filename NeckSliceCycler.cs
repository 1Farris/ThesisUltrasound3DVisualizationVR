using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NeckSliceCycler : MonoBehaviour
{
    public Camera mainCamera;
    public Image sliceViewer;
    public float changeInterval = 1.0f;     // Time between images
    private List<Sprite> slices = new List<Sprite>();
    private bool isCycling = false;

    void Start()
    {
        LoadSlices();
        sliceViewer.gameObject.SetActive(false);   // Hide at start
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            DetectClick();
    }

    void LoadSlices()
    {
        Sprite[] loaded = Resources.LoadAll<Sprite>("Slices");

        foreach (Sprite s in loaded)
            slices.Add(s);
    }

    void DetectClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Neck"))
            {
                if (!isCycling)
                {
                    sliceViewer.gameObject.SetActive(true);
                    StartCoroutine(CycleImages());
                }
            }
        }
    }

    IEnumerator CycleImages()
    {
        isCycling = true;

        int index = 0;

        while (true)        // infinite loop until you stop it manually
        {
            sliceViewer.sprite = slices[index];
            index = (index + 1) % slices.Count;
            yield return new WaitForSeconds(changeInterval);
        }
    }
}
