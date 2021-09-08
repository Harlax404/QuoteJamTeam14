using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputObject : MonoBehaviour
{
    private int keycode;
    private Sprite sprite;
    [SerializeField] SpriteRenderer renderer;

    public void Init(int key, Sprite _sprite)
    {
        keycode = key;
        sprite = _sprite;

        renderer.sprite = sprite;
    }
}
