using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

    [SerializeField]
    private Text keyboardLayoutText;

    private ArrayList inputListP1, inputListP2;

    private InputsNormalized inputsNormalized;

    public static PlayerInput Get;
    private void Awake()
    {
        if (Get == null)
        {
            Get = this;
        }
        else Destroy(this.gameObject);
    }

    void Start() {

        inputsNormalized = new InputsNormalized();

        // set text and button for detected keyboard layout and option to change
        if(inputsNormalized.getIsQwerty())
            keyboardLayoutText.text = "The keyboard layout is currently set to \'QWERTY\'";
        else 
            keyboardLayoutText.text = "La disposition du clavier est actuellement définie sur \'AZERTY\'";

        //inputListP1 = new ArrayList();
        //inputListP1.Add(InputsNormalized.UP);
        //inputListP1.Add(InputsNormalized.DOWN);
        //inputListP1.Add(InputsNormalized.LEFT);
        //inputListP1.Add(InputsNormalized.RIGHT);
        //inputListP1.Add(InputsNormalized.LEFT);
        //inputListP1.Add(InputsNormalized.RIGHT);
        //printListP1();

        //inputListP2 = new ArrayList();
        //inputListP2.Add(InputsNormalized.UP);
        //inputListP2.Add(InputsNormalized.DOWN);
        //inputListP2.Add(InputsNormalized.DOWN);
        //inputListP2.Add(InputsNormalized.DOWN);
        //inputListP2.Add(InputsNormalized.RIGHT);
        //inputListP2.Add(InputsNormalized.LEFT);
        //printListP2();

    }

    void Update() {
        if(Input.anyKeyDown) {
            StartCoroutine("InputCheckThreadPlayer1");
            StartCoroutine("InputCheckThreadPlayer2");
        }
    }

    private IEnumerator InputCheckThreadPlayer1() {

        //if(inputListP1.Count != 0 && Input.GetKeyDown((KeyCode)inputListP1[0])) {
        if(inputListP1.Count != 0 && Input.GetKeyDown(inputsNormalized.realInput(true, (int)inputListP1[0]))) {
            inputListP1.RemoveAt(0);
            p1InputPressed();
            //if(inputListP1.Count == 0) 
            //    p1Success(); 
            ///////////////////////delete after debugging {
            //else {             
            //    printListP1();
            //}
            ///////////////////////////////////////////// }
        } else if((inputsNormalized.getIsQwerty() && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) ||
                (!inputsNormalized.getIsQwerty() && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))) {
                // Le jouer 1 a clicker un faut boutton
                p1Fail();
        }

        yield return null;
    }

    private IEnumerator InputCheckThreadPlayer2() {
        
        //if(inputListP2.Count != 0 && Input.GetKeyDown((KeyCode)inputListP2[0])) {
        if(inputListP2.Count != 0 && Input.GetKeyDown(inputsNormalized.realInput(false, (int)inputListP2[0]))) {
            inputListP2.RemoveAt(0);
            p2InputPressed();
            //if(inputListP2.Count == 0) 
            //    p2Success();
            /////////////////////////delete after debugging {
            //else {
            //    printListP2();
            //}
            ///////////////////////////////////////////// }
        } else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {
                // Le jouer 2 a clicker un faut boutton
            p2Fail();
        }

        yield return null;
    }

    // correct input
    private void p1InputPressed() {
        BonbonManager.Get.DestroyInput(1);
    }
    private void p2InputPressed() {
        BonbonManager.Get.DestroyInput(2);
    }

    // mauvais input
    private void p1Fail()
    {     // animation fail  pour p1
        Debug.Log("P1 WRONG BUTTON");
        printListP1();
    }
    private void p2Fail()
    {     // animation fail  pour p2
        Debug.Log("P2 WRONG BUTTON");
        printListP2();
    }

    // fin de tous input
    //private void p1Success() {  // next bonbon pour p1
    //    Debug.Log("P1 SUCCESS");
    //}
    //private void p2Success() {  // next bonbon  pour p2
    //    Debug.Log("P2 SUCCESS"); 
    //}



    public void ChangeKeyboardLayoutButton() {
        inputsNormalized.setIsQwerty(!inputsNormalized.getIsQwerty());
        if(inputsNormalized.getIsQwerty())
            keyboardLayoutText.text = "The keyboard layout is currently set to \'QWERTY\'";
        else 
            keyboardLayoutText.text = "La disposition du clavier est actuellement définie sur \'AZERTY\'";

        printListP1();
        printListP2();
    }

    public void SetListInput(bool isP1, ArrayList list)
    {
        if (isP1)
        {
            inputListP1 = list;
            printListP1();
        }
        else
        {
            inputListP2 = list;
            printListP2();
        }
    }

    //////////////////////////// DELETE EVERYTHING AFTER HERE WHEN DONE DEBUGGING
    private void printListP1() {
        string debug = "";
        for(int i=0; i<inputListP1.Count; i++) {
            KeyCode key = inputsNormalized.realInput(true, (int)inputListP1[i]);
            debug += key + " ";
        }
        Debug.Log(debug);
    }
    private void printListP2() {
        string debug = "";
        for(int i=0; i<inputListP2.Count; i++) {
            KeyCode key = inputsNormalized.realInput(false, (int)inputListP2[i]);
            debug += key + " ";
        }
        Debug.Log(debug);
    }
}
