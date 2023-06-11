using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountDownUIHandler : MonoBehaviour
{
    [SerializeField] private Text _countDownText;

    private void Awake()
    {
        _countDownText.text = "";
    }

    private void Start()
    {
        StartCoroutine(CountDownCO());
    }

    IEnumerator CountDownCO()
    {
        yield return new WaitForSeconds(0.3f);

        int counter = 3;

        while (true)
        {
            if (counter != 0)
            {
                _countDownText.text = counter.ToString();
            }
            else
            {
                _countDownText.text = "GO!";

                GameManager.Instance.OnRaceStart();

                break;
            }

            counter--;
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
