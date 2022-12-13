using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventPublisher : SSEventPublisher{
    private int player_sign;    //当前玩家所处的房间
    public SceneController sceneController;

    private void Start() {
        sceneController = SSDirector.GetInstance().CurrentScenceController as SceneController;
    }
    
    private void Update() {
        //获得当前所在房间号
        int ret = FindPosition(this.transform.position);

        //发生变化
        if(ret != player_sign){
            if(ret >= 0 && ret <= 8){
                //玩家还在9个房间中，通知所有巡逻兵，让他们的GuardData变化，从而采取不同操作
                player_sign = ret;
                Notify(player_sign);
            }
            else{
                //玩家出界
                GameOver();
            }
            
        }
    }

    private int FindPosition(Vector3 pos){
        int a = Mathf.FloorToInt((pos.x + 6) / 12); //以中心定位，因此需要加上偏移
        int b = Mathf.FloorToInt((pos.z + 6) / 12);
        if(a >= 0 && a <= 2 && b >= 0 && b <= 2){
            return 3 * a + b;
        }
        else{
            return -1;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name.Contains("Guard")){//撞到巡逻兵，游戏结束
            GameOver();
        }
    }

    private void GameOver(){
        //通知三方做处理：
        //玩家控制器灭活，取消玩家的动画和移动
        PlayerController playerController = GetComponent<PlayerController>();
        playerController.isLive = false;

        //场景总控制器
        sceneController.Gameover();

        //巡逻兵的GuardData变化，取消巡逻兵的动作（立刻回调）并取消动画
        Notify(-1);
    }
}