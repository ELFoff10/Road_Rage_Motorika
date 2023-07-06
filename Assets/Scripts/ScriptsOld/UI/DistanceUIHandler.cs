using UnityEngine;
using UnityEngine.UI;

public class DistanceUIHandler : MonoBehaviour
{
    [SerializeField] private Text distanceText;
    private Transform carTransform;

    private float startingPosition;
    private float distanceTraveled;

    private void Start()
    {
        carTransform = GameObject.FindGameObjectWithTag("Player").transform;

        startingPosition = carTransform.position.y;
        distanceTraveled = 0f;
    }

    private void Update()
    {
        // Calculate the distance traveled
        float currentDistance = carTransform.position.y - startingPosition;

        // Update the distance text
        distanceTraveled = Mathf.Max(currentDistance, distanceTraveled) / 30;
        distanceText.text = distanceTraveled.ToString("F0") + "Km";
    }
}
