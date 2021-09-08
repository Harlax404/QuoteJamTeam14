using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonbon : MonoBehaviour
{
    //int playerId;
    //ArrayList listInput;
    [SerializeField] SpriteRenderer renderer;

    [HideInInspector] public int score;

    public void Init(/*ArrayList inputs, int _playerId,*/ Sprite _sprite, int _score)
    {
        //listInput = inputs;
        //playerId = _playerId;

        renderer.sprite = _sprite;
        score = _score;
    }
}
