using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour, OnTouch3D
{
   public GameObject cube;
   public GameObject cylinder0;
   public GameObject cylinder1;
   public GameObject cylinder2;
   public GameObject cylinder3;
   public GameObject cylinder4;
   public GameObject cylinder5;
   public GameObject cylinder6;
   public GameObject cylinder7;
   public GameObject cylinder8;
   public Text messageText;

   public float debounceTime = 0.3f;
   // Stores a counter for the current remaining wait time.
   private float remainingDebounceTime;
   private static List<int> boardState = new List<int>() {0,0,0,0,0,0,0,0,0};
   private static bool gameContinue = true;

   void Start()
   {
       remainingDebounceTime = 0.5f;
   }

   void Update()
   {
       // Time.deltaTime stores the time since the last update.
       // So all we need to do here is subtract this from the remaining
       // time at each update.
       if (remainingDebounceTime > 0) {
           remainingDebounceTime -= Time.deltaTime;
       }
   }

   public void OnTouch()
   {
       if (remainingDebounceTime <= 0)
       {
           string objectName = this.gameObject.name;
           int index = objectName[objectName.Length - 1] - '0';
           if (boardState[index] == 0 && gameContinue) {
               boardState[index] = 1;
               cube.gameObject.SetActive(true);
               if (checkWin(1)) {
                   messageText.gameObject.SetActive(true);
                   messageText.text = "WINNER WINNER CHICKEN DINNER!!!";
                   gameContinue = false;
               }

               System.Random random = new System.Random();
               int aiIndex = random.Next(9);
               bool full = true;
               for (int i = 0; i < boardState.Count; ++i) {
                    if(boardState[i] == 0) {
                        full = false;
                    }
               }
               while(boardState[aiIndex] != 0 && !full) {
                   aiIndex = random.Next(9);
               }
               if(!full) {
                   switch(aiIndex) {
                       case 0:
                       cylinder0.gameObject.SetActive(true);
                       break;
                       case 1:
                       cylinder1.gameObject.SetActive(true);
                       break;
                       case 2:
                       cylinder2.gameObject.SetActive(true);
                       break;
                       case 3:
                       cylinder3.gameObject.SetActive(true);
                       break;
                       case 4:
                       cylinder4.gameObject.SetActive(true);
                       break;
                       case 5:
                       cylinder5.gameObject.SetActive(true);
                       break;
                       case 6:
                       cylinder6.gameObject.SetActive(true);
                       break;
                       case 7:
                       cylinder7.gameObject.SetActive(true);
                       break;
                       case 8:
                       cylinder8.gameObject.SetActive(true);
                       break;
                   }
                   boardState[aiIndex] = 2;
               }else {
                   if(gameContinue) {
                       messageText.gameObject.SetActive(true);
                       messageText.text = "TIE!!";
                       gameContinue = false;
                   }
               }
               if (gameContinue && checkWin(2)) {
                   messageText.gameObject.SetActive(true);
                   messageText.text = "YOU LOSE, GOOD LUCK NEXT TIME!!!";
                   gameContinue = false;
               }
           }
       }
   }

   private bool checkWin(int num) {
       bool win1 = (boardState[0] == num && boardState[4] == num && boardState[8] == num);
       bool win2 = (boardState[0] == num && boardState[3] == num && boardState[6] == num);
       bool win3 = (boardState[1] == num && boardState[4] == num && boardState[7] == num);
       bool win4 = (boardState[2] == num && boardState[5] == num && boardState[8] == num);
       bool win5 = (boardState[2] == num && boardState[4] == num && boardState[6] == num);
       bool win6 = (boardState[0] == num && boardState[1] == num && boardState[2] == num);
       bool win7 = (boardState[3] == num && boardState[4] == num && boardState[5] == num);
       bool win8 = (boardState[6] == num && boardState[7] == num && boardState[8] == num);
       return win1 || win2 || win3 || win4 || win5 || win6 || win7 || win8;
   }
}
