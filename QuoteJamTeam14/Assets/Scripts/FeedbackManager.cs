using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour {

    [SerializeField, Range(1, 5)]
    private int shakeAmount = 1;

    [SerializeField, Range(0.1f, 10f)]
    private float shakeAmplitude = 1f;

    [SerializeField, Range(0.1f, 10.0f)]
    private float shakeTime = 1f;

    public static FeedbackManager Get;
    private void Awake()
    {
        if (Get == null)
        {
            Get = this;
        }
        else Destroy(this.gameObject);
    }

    public void IncorrectInputFeedback(InputObject inputObj) {
        StartCoroutine(shakeObj(inputObj.gameObject));
    }

    private IEnumerator shakeObj(GameObject obj) {

        float posDepart = obj.transform.position.x;
        float posIntermediaire = 0;

        float time = shakeTime / shakeAmount;
        float realShakeAmount = 2 * shakeAmount;
        
        for(int i=0; i < shakeAmount; i++) {
            float posFinal;
            if(i == 0) 
                posFinal = posDepart - shakeAmplitude;
            else 
                posFinal = (i%2 == 0) ? (posDepart - realShakeAmount) : (posDepart + realShakeAmount);

            float timer = 0f;
            while (timer <= time) {
                timer += Time.deltaTime;

                posIntermediaire = Mathf.Lerp(posDepart, posFinal, timer / time);

                obj.transform.position = new Vector3(posIntermediaire, obj.transform.position.y, obj.transform.position.z);

                yield return null;
            }
        }

        obj.transform.position = new Vector3(posDepart, transform.position.y, obj.transform.position.z);
    }
}
