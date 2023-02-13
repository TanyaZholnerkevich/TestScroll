using System;
using UnityEngine;

public class PrizeBoxCollision : MonoBehaviour
{
    public static event Action PrizeBoxCollided = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PrizeBoxCollided();
        gameObject.SetActive(false);
    }
}
