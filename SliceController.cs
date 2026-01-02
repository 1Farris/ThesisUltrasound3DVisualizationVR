using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SliceController : MonoBehaviour
{
    public RawImage sliceDisplay;
    public Slider sliceSlider;

    private List<Texture2D> slices = new List<Texture2D>();
    private int currentSliceIndex = 0;

    void Start()
    {
        LoadSlices("Slices/Neck");
        DisplaySlice(0);
        SetupSlider();
    }

    void LoadSlices(string folderPath)
    {
        slices.Clear();
        Texture2D[] loadedSlices = Resources.LoadAll<Texture2D>(folderPath);
        slices.AddRange(loadedSlices);
    }

    void DisplaySlice(int index)
    {
        if (index < 0 || index >= slices.Count) return;

        currentSliceIndex = index;
        sliceDisplay.texture = slices[currentSliceIndex];
    }

    public void NextSlice()
    {
        if (currentSliceIndex < slices.Count - 1)
        {
            DisplaySlice(currentSliceIndex + 1);
            sliceSlider.value = currentSliceIndex;
        }
    }

    public void PreviousSlice()
    {
        if (currentSliceIndex > 0)
        {
            DisplaySlice(currentSliceIndex - 1);
            sliceSlider.value = currentSliceIndex;
        }
    }

    void SetupSlider()
    {
        sliceSlider.minValue = 0;
        sliceSlider.maxValue = slices.Count - 1;
        sliceSlider.wholeNumbers = true;
        sliceSlider.onValueChanged.AddListener(
            value => DisplaySlice((int)value)
        );
    }
}
