using UnityEngine;

public class ButtonInputAggregator : MonoBehaviour
{
    private MobileButton leftButton;
    private MobileButton rightButton;


    private void Start()
    {
        if (leftButton && rightButton != null)
        {
            leftButton = GameObject.Find("LeftButton").GetComponent<MobileButton>();
            rightButton = GameObject.Find("RightButton").GetComponent<MobileButton>();
        }
    }

    public float GetHorizontalInput()
    {
        float horizontalInput = leftButton.GetHorizontalInput() + rightButton.GetHorizontalInput();
        return Mathf.Clamp(horizontalInput, -1f, 1f);
    }
}