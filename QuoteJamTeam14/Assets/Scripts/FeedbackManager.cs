using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour {

    [Header("Incorrect Input Feedback:")]

    [SerializeField, Range(1, 5)]
    private int shakeAmount = 1;

    [SerializeField, Range(0.1f, 1f)]
    private float shakeAmplitude = 0.1f;

    [SerializeField, Range(0.1f, 10.0f)]
    private float shakeTime = 1f;

    [Header("Correct Input Feedback:")]

    [SerializeField, Range(0.1f, 10.0f)]
    private float fadeTime = 1f;

    [SerializeField, Range(0.01f, 0.5f)]
    private float expandAmount = 0.1f;

    public static FeedbackManager Get;
    private void Awake()
    {
        if (Get == null)
        {
            Get = this;
        }
        else Destroy(this.gameObject);
    }

    public void correctInputFeedback(InputObject inputObj) {
        StartCoroutine(fadeAndExpandSpriteRenderer(inputObj));
    }

    public void IncorrectInputFeedback(InputObject inputObj) {
        StartCoroutine(shakeObj(inputObj.gameObject));
    }

    private IEnumerator shakeObj(GameObject obj) {

        if(obj == null) 
            yield break;

        float posDepart = obj.transform.position.x;
        float posIntermediaire = 0;

        int realAmount = shakeAmount * 2;
        float time = shakeTime / (2*realAmount);    // l'allee et le retour de chaque 'half rev'
        
        for(int i=0; i < realAmount; i++) {
            float posFinal = (i%2 == 0) ? (posDepart - shakeAmplitude) : (posDepart + shakeAmplitude);

            float timer = 0f;
            while (timer <= time) {
                timer += Time.deltaTime;
                posIntermediaire = Mathf.Lerp(posDepart, posFinal, timer / time);
                if(obj)
                    obj.transform.position = new Vector3(posIntermediaire, obj.transform.position.y, obj.transform.position.z);

                yield return null;
            }
            timer = 0f;
            while (timer <= time) {
                timer += Time.deltaTime;
                posIntermediaire = Mathf.Lerp(posFinal, posDepart, timer / time);
                if(obj)
                    obj.transform.position = new Vector3(posIntermediaire, obj.transform.position.y, obj.transform.position.z);

                yield return null;
            }
        }
        if(obj)
            obj.transform.position = new Vector3(posDepart, obj.transform.position.y, obj.transform.position.z);
    }

    private IEnumerator fadeAndExpandSpriteRenderer(InputObject inputObj) {
        if(inputObj == null) 
            yield break;

        GameObject obj = Instantiate(inputObj.gameObject);
        obj.transform.position = new Vector3(inputObj.gameObject.transform.position.x, inputObj.gameObject.transform.position.y, inputObj.gameObject.transform.position.z);
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();

        float alpha = spriteRenderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime) {
            float scaleDepart = obj.gameObject.transform.localScale.x;
            float intermAlpha = Mathf.Lerp(alpha, 0f, t), intermScale = Mathf.Lerp(scaleDepart, scaleDepart + expandAmount, t);

            Color newColor = new Color(1, 1, 1, intermAlpha);

            if(obj) {
                spriteRenderer.material.color = newColor;
                obj.gameObject.transform.localScale = new Vector3(intermScale, intermScale, intermScale);
            }
            yield return null;
        }
        Destroy(obj);
     }
}
