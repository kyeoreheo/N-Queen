//Kyeore Heo.
//91kyoheo@gmail.com
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    //-------Vectors ----------------------------
    public static GameObject[,] myBoardVector;
    public static GameObject[] myStatusVector;

    //-------Scenes------------------------------
    public GameObject mySetUpManager;
    public GameObject myBoard;

    //-------Instantiates------------------------
    public GameObject frame;
    public GameObject checkBox_;
    public GameObject dark_;
    public GameObject light_;

    //-------Setting Values---------------------
    public int myBoardSize;
    public int myBoxSize;

    //-------Debug-------------------------------
    public Text txtDebug;

    //---------------------------------------------------------------------------------------------------------------
    void Start()
    {
        myBoardSize = SetUpManager.myInputBoardSize;
        myBoxSize = SetUpManager.myInputBoxSize;
        myStatusVector = new GameObject[myBoardSize];
        myBoardVector = new GameObject[myBoardSize, myBoardSize];
        DisplayBoard();
    }

    //---------------------------------------------------------------------------------------------------------------
    public void DisplayBoard()
    {
        for (int b = 0; b < myBoardSize; b++)
        {
            GameObject myCheckBox = Instantiate(checkBox_, new Vector2(myBoxSize * b + 80, 1850), transform.rotation);
            myCheckBox.transform.SetParent(frame.transform);
            //myCheckBox.GetComponent<Status>().myBoxNumber = b;
            myStatusVector[b] = myCheckBox;
            myCheckBox.transform.name = "Row: " + b.ToString();
        }

        for (int c = 0; c < myBoardSize; c++)
        {
            for (int r = 0; r < myBoardSize; r++)
            {
                if (c % 2 == 0)
                {
                    if (r % 2 == 0)//even number
                    {
                        GameObject myDark = Instantiate(dark_, new Vector2(myBoxSize * r + 75 , -myBoxSize * c + 1700), transform.rotation);
                        myDark.transform.SetParent(frame.transform);
                        myDark.gameObject.GetComponent<ChessBlock>().SetBlock(r, c);
                        myBoardVector[r, c] = myDark;
                        myDark.transform.name = "(" + r.ToString() + "," + c.ToString() + ")";
                    }
                    else //odd number
                    {
                        GameObject myLight = Instantiate(light_, new Vector2(myBoxSize * r + 75, -myBoxSize * c + 1700), transform.rotation);
                        myLight.transform.SetParent(frame.transform);
                        myLight.gameObject.GetComponent<ChessBlock>().SetBlock(r, c);
                        myBoardVector[r, c] = myLight;
                        myLight.transform.name = "(" + r.ToString() + "," + c.ToString() + ")";
                    }
                }
                else
                {
                    if (r % 2 == 0)//even number
                    {
                        GameObject myLight = Instantiate(light_, new Vector2(myBoxSize * r + 75, -myBoxSize * c + 1700), transform.rotation);
                        myLight.transform.SetParent(frame.transform);
                        myLight.gameObject.GetComponent<ChessBlock>().SetBlock(r, c);
                        myBoardVector[r, c] = myLight;
                        myLight.transform.name = "(" + r.ToString() + "," + c.ToString() + ")";
                    }
                    else //odd number
                    {
                        GameObject myDark = Instantiate(dark_, new Vector2(myBoxSize * r + 75, -myBoxSize * c + 1700), transform.rotation);
                        myDark.transform.SetParent(frame.transform);
                        myDark.gameObject.GetComponent<ChessBlock>().SetBlock(r, c);
                        myBoardVector[r, c] = myDark;
                        myDark.transform.name = "(" + r.ToString() + "," + c.ToString() + ")";
                    }
                }
            }
        }
    }

    //---------------------------------------------------------------------------------------------------------------
    public void CheckUnderAttack(int row, int col)
    {
        bool result = false;
        for (int i = 0; i < myBoardSize; i++)
        {
            if (myBoardVector[i, col].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.QUEEN && (i != row))
                result = true; //check the row
            else if (myBoardVector[row, i].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.QUEEN && (i != col))
                result = true; //check the col
            else if ((row - col >= 0) && (row - col + i < myBoardSize) && myBoardVector[row - col + i, 0 + i].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.QUEEN)
                result = true; //diagnal check  \\
            else if ((row - col < 0) && (Mathf.Abs(row - col) + i < myBoardSize) && myBoardVector[0 + i, Mathf.Abs(row - col) + i].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.QUEEN)
                result = true; //왼쪽 위 부터 중간까지
            else if ((row + col < myBoardSize) && (row + col - i >= 0) && myBoardVector[row + col - i, 0 + i].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.QUEEN)
                result = true;
            else if ((row + col >= myBoardSize) && (row + col - i < myBoardSize) && (i < myBoardSize) && myBoardVector[row + col - i, i].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.QUEEN)
                result = true;
        }

        if (result)//if the spot is underattack
            myBoardVector[row, col].gameObject.GetComponent<ChessBlock>().SetSpot(ChessBlock.SPOT.UNDERATTACK);
        else // if it is safe
            myBoardVector[row, col].gameObject.GetComponent<ChessBlock>().SetSpot(ChessBlock.SPOT.SAFE);
    }

    //---------------------------------------------------------------------------------------------------------------
    void Update() //run every frame
    {
        for (int c = 0; c < myBoardSize; c++)
        {
            for (int r = 0; r < myBoardSize; r++)
            {
                if (letsClear) // When I clicked "Clear"
                {
                    for (int i = 0; i < myBoardSize; i++) // Cleaning up myCheckBoxes
                    { myStatusVector[i].gameObject.GetComponent<Status>().myChecked.SetActive(false); }
                    myRow = 0; myCol = 0;
                    myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().SetSpot(ChessBlock.SPOT.SAFE);
                    if (r == myBoardSize - 1 && c == myBoardSize - 1)
                        letsClear = false; //Done Cleaning.
                }
                else //All the time = Normal Situation
                {
                    switch (myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().GetSpot())
                    {
                        case ChessBlock.SPOT.UNDERATTACK:
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myMarked.SetActive(true);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myQueen.SetActive(false);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myNot.SetActive(false);
                            CheckUnderAttack(r, c);
                            break;
                        case ChessBlock.SPOT.SAFE:
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myMarked.SetActive(false);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myQueen.SetActive(false);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myNot.SetActive(false);
                            CheckUnderAttack(r, c);
                            break;
                        case ChessBlock.SPOT.QUEEN:
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myMarked.SetActive(false);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myQueen.SetActive(true);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myNot.SetActive(false);
                            break;
                        case ChessBlock.SPOT.NOTTHISSPOT:
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myMarked.SetActive(false);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myQueen.SetActive(false);
                            myBoardVector[r, c].gameObject.GetComponent<ChessBlock>().myNot.SetActive(true);
                            break;
                    }
                }
            }
        }
    }

    //---------------------------------------------------------------------------------------------------------------
    public int myRow, myCol;
    public int safeSpots;

    public void PlaceTheQueen()
    {
        safeSpots = 0;//When ever this function is called, starts at 0
        for (int c = myBoardSize-1; c >= 0; c--) // Count back from bottom -> so I can get the most top at the end.
        {
            if (myBoardVector[myRow, c].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.SAFE)
            {
                myCol = c; 
                safeSpots++;
            }
        }
        /*This is Fail situation. 
          I increase myRow, myCol ++ after I place my Queen. So when it reaches the maximum size. 
          When there are no safeSpot, that Column is fail*/
        if (myRow >= myBoardSize && myCol >= myBoardSize || myCol >= myBoardSize || safeSpots == 0)
        {
            Debug.Log("Im looking here Row: " + myRow + "And there are no safe Spot, so fail");
            Debug.Log("My Last Queen R: " + myRow + " C:" + myCol + "is not working");
            //I Failed this Col but there are other options
            if (myStatusVector[myRow].gameObject.GetComponent<Status>().myOtherOptionList.Count > 0)
            { 
                myBoardVector[myRow, myCol].gameObject.GetComponent<ChessBlock>().SetSpot(ChessBlock.SPOT.NOTTHISSPOT);
                myStatusVector[myRow].gameObject.GetComponent<Status>().myOtherOptionList.RemoveAt(0);
            }
            //I Failed and there are no other options
            else 
            { 
                for(int i = 0; i<myBoardSize; i++)
                {
                    //set the whole spot in that col Safe. => even if everythin is safe, they will be re-checked in Update frame
                    myBoardVector[myRow, i].gameObject.GetComponent<ChessBlock>().SetSpot(ChessBlock.SPOT.SAFE);
                    //uncheck my CheckBox
                    myStatusVector[myRow].gameObject.GetComponent<Status>().myChecked.SetActive(false);
                }
                myRow--; //Since I increase after my place down, I have to -1 to get current Row.
            }
            //If I didnt reach the fail&no other option <- because I clear the fail part I need current Row,Col with -1 
            myRow = myStatusVector[myRow].gameObject.GetComponent<Status>().GetCurrentRow();
            myCol = myStatusVector[myRow].gameObject.GetComponent<Status>().GetCurrentCol();
        }
        //Safe -> Place Queen 
        else if (myBoardVector[myRow, myCol].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.SAFE)
        {
            for(int i = 0; i<myBoardSize; i++)
            {
                //Getting other Option except itself.
                if(i!=myCol && myBoardVector[myRow, i].gameObject.GetComponent<ChessBlock>().GetSpot() == ChessBlock.SPOT.SAFE)
                    myStatusVector[myRow].gameObject.GetComponent<Status>().myOtherOptionList.Add(myBoardVector[myRow, i]);
            }
            myBoardVector[myRow, myCol].gameObject.GetComponent<ChessBlock>().SetSpot(ChessBlock.SPOT.QUEEN);
            myStatusVector[myRow].gameObject.GetComponent<Status>().myChecked.SetActive(true);//Check the Box ON After I place a Queen
            myStatusVector[myRow].gameObject.GetComponent<Status>().SetCurrentSpot(myRow, myCol);
            txtDebug.text = "Placed Queen R: " + myRow + " C: " + myCol;
            myRow++; // myRow => next Row to go. 
        }
        else if (myRow < myBoardSize)//when I cannot place the queen, move to next Col Col=Y
            myCol++;
    }

    //---------------------------------------------------------------------------------------------------------------
    private bool letsClear;

    public void ClearTheBoard()
    {
        letsClear = true;
        for(int i = 0; i < myBoardSize; i++)
        {
            myStatusVector[i].gameObject.GetComponent<Status>().SetCurrentSpot(0, 0);
            //myStatusVector[i].gameObject.GetComponent<Status>().SetOtherSpot(0, 0);
        }
    }

    //---------------------------------------------------------------------------------------------------------------
    public float timer;

    public GameObject btnAuto;
    public GameObject btnStopAuto;
    public void Auto()
    {
        //InvokeRepeating("PlaceTheQueen", 0f, 0f);
        InvokeRepeating("PlaceTheQueen", 0.5f, 0.5f);
        btnAuto.SetActive(false);
        btnStopAuto.SetActive(true);

    }

    //---------------------------------------------------------------------------------------------------------------
    public GameObject btnFast;
    public GameObject btnStopFast;
    public void SuperFast()
    {
        //InvokeRepeating("PlaceTheQueen", 0f, 0f);
        InvokeRepeating("PlaceTheQueen", 0.1f, 0.1f);
        btnFast.SetActive(false);
        btnStopFast.SetActive(true);

    }

    //---------------------------------------------------------------------------------------------------------------
    public void StopAuto()
    {
        CancelInvoke("PlaceTheQueen");
        btnStopAuto.SetActive(false);
        btnStopFast.SetActive(false);
        btnAuto.SetActive(true);
        btnFast.SetActive(true);
    }
}
