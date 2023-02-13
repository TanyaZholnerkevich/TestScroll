using System;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public static event Action EnemyCollided = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyCollided();
    }
}
