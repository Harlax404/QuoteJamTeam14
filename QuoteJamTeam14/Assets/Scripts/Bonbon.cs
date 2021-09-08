using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonbon : MonoBehaviour
{
    int playerId;
    ArrayList listInput;
    Sprite emballage;
    Sprite bonbon;
    [SerializeField] SpriteRenderer renderer;

    public void Init(ArrayList inputs, int _playerId, Sprite emballage, Sprite bonbon)
    {
        listInput = inputs;
        playerId = _playerId;

        renderer.sprite = emballage;
    }
}
