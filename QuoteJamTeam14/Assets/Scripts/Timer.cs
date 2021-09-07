using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    
    [SerializeField, Range(1.0f, 300.0f)]
    private float timerValue = 60.0f;

    public Text timerText;
[SerializeField]
    private GameObject restartButton;

    private float curTime;

    void Start() {
        curTime = timerValue;
        matchStatusChange(false);
    }

    void Update() {
        if(curTime > 0f)
            curTime -= Time.deltaTime;
        else {
            curTime = 0f;
            matchStatusChange(true);
        }
            
        timerText.text = (int)(curTime) + "";
    }

    private void matchStatusChange(bool status) {
        restartButton.SetActive(status);
    }

    public void RestartGame() {
        curTime = timerValue;
        matchStatusChange(false);
    }
}
