using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolData : MonoBehaviour, IEventHandler
{
    public float walk_speed = 0.5f;        //移速
    public float run_speed = 0.5f;
    public int sign;                      //巡逻兵所在区域
    public bool isRun = false;            //当前是否在追逐玩家
    public bool isLive = true;            //当前是否活跃
    public Vector3 start_position;        //巡逻兵初始位置   

    /*IEventHandler响应函数*/
    public void Reaction(int pos){
        //游戏结束，灭活，取消巡逻兵的动作（立刻回调）并取消动画
        if(pos == -1){
            isLive = false;
            return;
        }

        //若玩家处于巡逻兵一样的位置，开始追逐；否则，结束追逐
        isRun = (pos == sign);
    }
}
