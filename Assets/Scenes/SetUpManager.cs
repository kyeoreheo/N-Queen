//Kyeore Heo.
//91kyoheo@gmail.com
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SetUpManager : MonoBehaviour
{
    public static int myInputBoardSize = 1;
    public static int myInputBoxSize = 1;
    public InputField myInput;
    public GameObject myBoard;
    public GameObject mySetUp;

    public void GetInput()
    {
        if(myInput.text == "")
        {
            myInput.text = "8"; 
        }
        myInputBoardSize = Convert.ToInt32(myInput.text);
        myInputBoxSize = 960 / myInputBoardSize;
        myBoard.SetActive(true);
        mySetUp.SetActive(false);
    }
}
