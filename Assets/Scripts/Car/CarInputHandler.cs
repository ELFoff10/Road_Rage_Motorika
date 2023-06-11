using UnityEngine;

[RequireComponent (typeof(CarController))]
public class CarInputHandler : MonoBehaviour
{
    public bool IsUIInput = false;

    private CarController _carController;

    private Vector2 _inputVector = Vector2.zero;
    private ButtonInputAggregator buttonInputAggregator;

    private void Awake()
    {
        _carController = GetComponent<CarController>();
        buttonInputAggregator = GetComponent<ButtonInputAggregator>();
    }

    private void Update()
    {
        if (IsUIInput)
        {
            if(buttonInputAggregator != null)
                _inputVector.x = buttonInputAggregator.GetHorizontalInput();
        }
        else
        {
            _inputVector = Vector2.zero;

            _inputVector.x = Input.GetAxis("Horizontal");
            //inputVector.y = Input.GetAxis("Vertical");
        }

        _carController.SetInputVector(_inputVector);
    }

    public void SetInput(Vector2 newInput)
    {
        _inputVector = newInput;
    }
}
