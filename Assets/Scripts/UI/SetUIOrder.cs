using UnityEngine;
using UnityEngine.UI;

public class SetUIOrder : MonoBehaviour
{
    public int sortingOrder;

    void Start()
    {
        // Get the Canvas component of the UI element
        Canvas canvas = GetComponent<Canvas>();

        // Set the sorting order of the Canvas
        canvas.sortingOrder = sortingOrder;
    }
}
