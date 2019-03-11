using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_anim : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject[] Buttons;
    public Animator cubeAni;
    public GameObject theCube;

    string whichBtn;
    bool btn0Pressed = false;
    bool btn1Pressed = false;
    bool btn2Pressed = false;

    int j = 0;
    static int[] solution = new int[3] { 0, 1, 2 };
    int userInput;
    int[] turns = new int[3];

    bool win = false;

    // Use this for initialization
    void Start () {

        for(int i = 0; i < 3; ++i)
        {
            Buttons[i] = GameObject.Find("virtBtn" + i);
            Buttons[i].GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        }

        /*
        Buttons[0] = GameObject.Find("virtBtn0");
        Buttons[0].GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        Buttons[1] = GameObject.Find("virtBtn1");
        Buttons[1].GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        Buttons[2] = GameObject.Find("virtBtn2");
        Buttons[2].GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        */

        cubeAni.GetComponent<Animator>();
        theCube = GameObject.Find("theCube");
    }
	
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        whichBtn = vb.transform.name;

        if(!win)
        {
            switch (whichBtn)
            {
                case "virtBtn0":
                    theCube.GetComponent<Renderer>().material.color = Color.blue;
                    btn0Pressed = true;
                    userInput = 0;
                    break;
                case "virtBtn1":
                    theCube.GetComponent<Renderer>().material.color = Color.green;
                    btn1Pressed = true;
                    userInput = 1;
                    break;
                case "virtBtn2":
                    theCube.GetComponent<Renderer>().material.color = Color.yellow;
                    btn2Pressed = true;
                    userInput = 2;
                    break;
                default:
                    break;
            }

            turns[2] = turns[1];
            turns[1] = turns[0];
            turns[0] = userInput;

            //cubeAni.Play("cubeAni");
            //theCube.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("BTN Pressed");
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //cubeAni.Play("none");
        Debug.Log("BTN Released");
        //theCube.GetComponent<Renderer>().material.color = Color.green;  
    }

    // Update is called once per frame
    void Update () {
        if (btn0Pressed == true && btn1Pressed == true && btn2Pressed == true)
        {
            if (turns[0] == solution[0] && turns[1] == solution[1] && turns[2] == solution[2])
            {
                theCube.GetComponent<Renderer>().material.color = Color.black;
                cubeAni.Play("cubeAni");
                win = true;
                if (j == 100)
                {
                    btn0Pressed = false;
                    btn1Pressed = false;
                    btn2Pressed = false;
                    theCube.GetComponent<Renderer>().material.color = Color.white;
                    j = 0;
                    cubeAni.Play("none");
                    win = false;
                }
                else
                {
                    ++j;
                }
            }
        }
    }
}
