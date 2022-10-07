using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFunc : MonoBehaviour
{
    GUIStyle myStyle;
    private int turn = 1;
    private int num = 0;

    private int[,] matrix = new int[3,3];
    int middle=Screen.width / 2;

    void reset(){
        turn = 1;
        num = 0;
        for(int i=0;i<3;i++){
            for(int j=0;j<3;j++){
                matrix[i,j]=0;
            }
        }
        print("game is reseted.");
    }

    int check(){
        for(int i=0;i<3;i++){
            //列的检测
            if(matrix[i,0]!=0 && (matrix[i,0]==matrix[i,1] && matrix[i,0]==matrix[i,2])){
                return matrix[i,0];
            }
            //行的检测
            if(matrix[0,i]!=0 && (matrix[0,i]==matrix[1,i] && matrix[0,i]==matrix[2,i])){
                return matrix[0,i];
            }
        }
        //对角线检测
        if(matrix[0,0]!=0&&(matrix[0,0]==matrix[1,1]&&matrix[0,0]==matrix[2,2])){
            return matrix[0,0];
        }
        if(matrix[0,2]!=0&&(matrix[0,2]==matrix[1,1]&&matrix[0,2]==matrix[2,0])){
            return matrix[0,2];
        }
        if(num<9) return 0;
        return 3;
    }

    void result(int res){
        switch(res){
            case 0:
                if(turn == 1)
                    GUI.Box(new Rect(middle-45,55,100,35),"O's turn!");
                else if(turn == 2)
                    GUI.Box(new Rect(middle-45,55,100,35),"X's turn!");
                break;

            case 1:
                GUI.Box(new Rect(middle-45,55,100,35),"0 has Win!");
                break;

            case 2:
                GUI.Box(new Rect(middle-45,55,100,35),"X has Win!");
                break;
            case 3:
                GUI.Box(new Rect(middle-45,55,100,35),"Draw!");
                break;
        }
    }

    void click(int i,int j){
        matrix[i,j]=turn;
        num++;
        turn = (turn == 1) ? 2 : 1;
    }

    void OnGUI(){

        //游戏背景的box
        GUI.Box(new Rect(middle-120,30,252,330),"Tic-Tac-Toe");

        //restart按钮
        if(GUI.Button(new Rect(middle-35,95,75,30),"Restart")){
            reset();
        }

        int res=check();
        for(int i=0;i<3;i++){
            for(int j=0;j<3;j++){
                if(matrix[i,j]==1){
                    GUI.Button(new Rect(middle-120+i*84,135+j*70,84,70),"O");
                }
                if(matrix[i,j]==2){
                    GUI.Button(new Rect(middle-120+i*84,135+j*70,84,70),"X");
                }
                if(GUI.Button(new Rect(middle-120+i*84,135+j*70,84,70),"")){
                    click(i,j);
                }
            }
            result(res);
        }
    }



}
