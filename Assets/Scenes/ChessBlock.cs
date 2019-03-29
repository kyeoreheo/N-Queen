//Kyeore Heo.
//91kyoheo@gmail.com
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBlock : MonoBehaviour
{
    public enum SPOT { SAFE, QUEEN, UNDERATTACK, NOTTHISSPOT }
    public Text myText;
    public GameObject myQueen;
    public GameObject myMarked;
    public GameObject myNot;
    public SPOT mySpot_;

    private int myR_;
    private int myC_;

    public void SetSpot(SPOT spot_)
    { mySpot_ = spot_; }

    public void SetBlock(int r, int c) 
    { myR_ = r; myC_ = c; }

    public  int GetR()
    { return myR_; }

    public  int GetC()
    { return myC_; }

    public SPOT GetSpot()
    { return mySpot_; }

    void Start()
    { myText.text = myR_ + "," + myC_; }

    public void TouchTheSpot()
    {
        if (mySpot_ == SPOT.QUEEN)
            mySpot_ = SPOT.SAFE;
        else if (mySpot_ == SPOT.SAFE)
            mySpot_ = SPOT.QUEEN;
    }
}

