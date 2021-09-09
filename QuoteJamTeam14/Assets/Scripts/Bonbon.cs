using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonbon : MonoBehaviour
{
    //int playerId;
    //ArrayList listInput;
    [SerializeField] SpriteRenderer renderer;

    [HideInInspector] public int score;
    [HideInInspector] public BonbonType bonbonType;

    public void Init(Sprite _sprite, int _score, BonbonType type)
    {
        renderer.sprite = _sprite;
        score = _score;
        bonbonType = type;
    }
}
