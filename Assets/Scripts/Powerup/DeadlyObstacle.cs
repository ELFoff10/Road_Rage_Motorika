using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CarController car = collision.transform.root.GetComponent<CarController>();

        if (car != null && car.tag == "Player")
        {
            GameManager.Instance.OnRaceCompleted();

            car.GetComponent<CarInputHandler>().enabled = false;
        }
    }
}
