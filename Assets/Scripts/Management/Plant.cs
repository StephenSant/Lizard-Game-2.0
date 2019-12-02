using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public SpriteRenderer sprite;
    public bool fadeToTransparency;

    void Update()
    {
        if (fadeToTransparency)
        {
            sprite.color = new Color(
                            Mathf.Lerp(sprite.color.r, 0, 0.25f),
                            Mathf.Lerp(sprite.color.g, 0, 0.25f),
                            Mathf.Lerp(sprite.color.b, 0, 0.25f),
                            Mathf.Lerp(sprite.color.a, 0.25f, 0.25f)
                            );
        }
        else
        {
            sprite.color = new Color(
                                Mathf.Lerp(sprite.color.r, 1, 0.25f),
                                Mathf.Lerp(sprite.color.g, 1, 0.25f),
                                Mathf.Lerp(sprite.color.b, 1, 0.25f),
                                Mathf.Lerp(sprite.color.a, 1, 0.25f)
                                );
        }
    }
}
