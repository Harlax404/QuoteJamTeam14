using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    public GameObject spriteP1;
    public GameObject spriteP2;

    public GameObject smashButtonP1;
    public GameObject smashButtonP2;

    public static CanvasManager Get;
    private void Awake()
    {
        if (Get == null)
        {
            Get = this;
        }
        else Destroy(this.gameObject);
    }
}
