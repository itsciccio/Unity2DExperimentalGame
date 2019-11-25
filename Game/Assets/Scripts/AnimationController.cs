using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    public float timer;

    void Update()
    {
        timer += 1.0f * Time.deltaTime;

        if (timer >= 1)
        {
            Destroy(gameObject);
        }
    }
}