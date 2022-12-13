using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;                     //管理器
    private GUIStyle small_style = new GUIStyle();
    private GUIStyle big_style = new GUIStyle();
    void Start () {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        small_style.normal.textColor = new Color(0, 0, 0, 1);
        small_style.fontSize = 16;
        big_style.fontSize = 24;
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width - 170, 10, 40, 50), "分数:", small_style);
        GUI.Label(new Rect(Screen.width - 130, 10, 40, 50), action.GetScore().ToString(), small_style);
        GUI.Label(new Rect(Screen.width / 2 - 80, 10, 100, 100), "WASD移动，鼠标移动视角", small_style);
        GUI.Label(new Rect(Screen.width / 2 - 80, 30, 100, 100), "Shift切换走、跑", small_style);
        GUI.Label(new Rect(Screen.width / 2 - 80, 50, 100, 100), "成功躲避巡逻兵追捕加1分", small_style);
        GUI.Label(new Rect(Screen.width / 2 - 80, 70, 100, 100), "出界或被巡逻兵抓到，游戏结束", small_style);
        if (action.isOver()) {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", big_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始")) {
                action.Restart();
                return;
            }
        }  
    }
}