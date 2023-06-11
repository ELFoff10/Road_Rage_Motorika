using UnityEngine;
using UnityEngine.UI;

public class SetLeaderBoardItemInfo : MonoBehaviour
{
    [SerializeField] private Text _positionText; 
    [SerializeField] private Text _driverNameText;

    public void SetPositionText(string newPosition)
    {
        _positionText.text = newPosition;
    }

    public void SetDriverNameText(string newDriverName)
    {
        _driverNameText.text = newDriverName;
    }
}
