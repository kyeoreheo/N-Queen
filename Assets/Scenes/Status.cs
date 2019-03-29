//Kyeore Heo.
//91kyoheo@gmail.com
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Text txtCurrentSpot;
    public Text txtOtherOption;
    public GameObject myChecked;

    private int myCurrentRow_;
    private int myCurrentCol_;
    public List<GameObject> myOtherOptionList;

    public void SetCurrentSpot (int r, int c)
    { myCurrentRow_ = r; myCurrentCol_ = c; }

    public int GetCurrentRow()
    { return myCurrentRow_; }
    public int GetCurrentCol()
    { return myCurrentCol_; }

    void Update()
    {
        txtCurrentSpot.text = "(" + myCurrentRow_ + "," + myCurrentCol_ + ")";
        txtOtherOption.text = "" + myOtherOptionList.Count;
    }

}

