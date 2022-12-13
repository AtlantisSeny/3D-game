using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRunAction : SSAction
{
    private GameObject player;        //玩家
    private PatrolData data;           //巡逻兵数据
    private Animator anim;            //巡逻兵动画
    private Rigidbody rigid;          //巡逻兵刚体

    public override void Start() {
        //初始化组件
        data = gameobject.GetComponent<PatrolData>();
        anim = gameobject.GetComponent<Animator>();
        rigid = gameobject.GetComponent<Rigidbody>();

        //设置跑动的动画
        anim.SetBool("isLive", true);
        anim.SetBool("isWalk", true);
        anim.SetBool("isRun", true);
    }

    public static GuardRunAction GetSSAction(GameObject player) {
        GuardRunAction action = CreateInstance<GuardRunAction>();
        action.player = player;
        return action;
    }

    public override void FixedUpdate() {
        //观察GuardData中的变量isLive（Reaction会改变这个变量）
        //若被灭活，则取消巡逻兵的动作（立刻回调）并取消动画
        if(!data.isLive){
            anim.SetBool("isLive", false);
            this.destroy = true;
            this.callback.SSActionEvent(this, SSActionEventType.Competeted, -1, this.gameobject);
            return;
        }
        
        //若不被灭活，则跑向玩家
        Run();

        //观察GuardData中的变量isRun（Reaction会改变这个变量）
        //如果玩家脱离该区域则继续巡逻
        if (!data.isRun) {
            this.destroy = true;
            this.callback.SSActionEvent(this, SSActionEventType.Competeted, 1, this.gameobject);
        }
    }

    private void Run(){
        //控制巡逻兵跑向玩家
        Vector3 dir = player.transform.position - player.transform.forward; //玩家偏后一点的位置
        gameobject.transform.LookAt(dir);   //转向
        Vector3 vec = data.run_speed * gameobject.transform.forward;    //设置移动向量
        rigid.MovePosition(gameobject.transform.position + 5 * vec * Time.deltaTime);   //借助RigidBody移动
    }
}
