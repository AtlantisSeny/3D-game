using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardWalkAction : SSAction
{         
    private float pos_x, pos_z;     //移动位置记录
    private float move_length;      //移动的边长
    private bool turn_sign = true;  //是否转向        
    private int dirction = 0;       //当前方向

    private PatrolData data;         //巡逻兵数据
    private Animator anim;          //巡逻兵动画
    private Rigidbody rigid;        //巡逻兵刚体

    public override void Start()
    {
        //加载组件
        data = gameobject.GetComponent<PatrolData>();
        anim = gameobject.GetComponent<Animator>();
        rigid = gameobject.GetComponent<Rigidbody>();

        //设置走动动画
        anim.SetBool("isLive", true);
        anim.SetBool("isWalk", true);
        anim.SetBool("isRun", false);
    }

    public static GuardWalkAction GetSSAction(Vector3 location) {
        //确定初始位置
        GuardWalkAction action = CreateInstance<GuardWalkAction>();
        action.pos_x = location.x;
        action.pos_z = location.z;

        //设定移动矩形的边长
        action.move_length = Random.Range(6, 10);
        return action;
    }

    public override void FixedUpdate()
    {
        //观察GuardData中的变量isLive（Reaction会改变这个变量）
        //若被灭活，则取消巡逻兵的动作（立刻回调）并取消动画
        if(!data.isLive){
            anim.SetBool("isLive", false);
            this.destroy = true;
            this.callback.SSActionEvent(this, SSActionEventType.Competeted, -1, this.gameobject);
            return;
        }

        //若不被灭活，则按规则走动
        Walk();

        //观察GuardData中的变量isRun（Reaction会改变这个变量）
        //如果玩家进入该区域则开始追击
        if(data.isRun){
            this.destroy = true;
            this.callback.SSActionEvent(this, SSActionEventType.Competeted, 0, this.gameobject);
        }
    }

    private void Walk(){
        if (turn_sign) {
            //按照矩形逆时针移动
            switch (dirction) {
                case 0:
                    pos_x += move_length;
                    break;
                case 1:
                    pos_z += move_length;
                    break;
                case 2:
                    pos_x -= move_length;
                    break;
                case 3:
                    pos_z -= move_length;
                    break;
            }
            turn_sign = false;
        }

        //获取目标位置，并转向目标位置
        Vector3 dir = new Vector3(pos_x, gameobject.transform.position.y, pos_z);
        gameobject.transform.LookAt(dir);

        //由于有高低差等缺陷，因此采用距离而不是比较直接相等
        float distance = Vector3.Distance(dir, this.transform.position);

        if(distance > 0.9){
            //未到达，需要借助RigidBody移动
            Vector3 vec = data.walk_speed * gameobject.transform.forward;
            rigid.MovePosition(gameobject.transform.position + 10 * vec * Time.deltaTime);
        }
        else{
            //到达，转向
            Turn();
        }
    }

    private void Turn(){
        dirction = (dirction + 1) % 4;
        turn_sign = true;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name.Contains("Wall")){
            //撞墙，转向
            Turn();
        }
    }
}
