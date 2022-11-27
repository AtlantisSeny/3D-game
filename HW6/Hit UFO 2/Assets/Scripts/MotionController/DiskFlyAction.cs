﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFlyAction : SSAction {
    public float gravity = -5;                                 //向下加速度
    private Vector3 start_vector;                              //初速度
    private Vector3 gravity_vector = Vector3.zero;             //加速度
    private Vector3 current_angle = Vector3.zero;              //欧拉角
    private float time;                                        //时间

    private DiskFlyAction() { }
    public static DiskFlyAction GetSSAction(int lor, float angle, float power) {
        //初始化物体
        DiskFlyAction action = CreateInstance<DiskFlyAction>();
        if (lor == -1) {
            action.start_vector = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * power;
        }
        else {
            action.start_vector = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        }
        return action;
    }

    public override void Update() {
        //计算物体的向下的速度
        time += Time.fixedDeltaTime;
        gravity_vector.y = gravity * time/5f;

        //位移模拟
        transform.position += (start_vector + gravity_vector) * Time.fixedDeltaTime/5f;
        current_angle.z = Mathf.Atan((start_vector.y + gravity_vector.y) / start_vector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = current_angle;

        //如果物体y坐标小于-10，动作就做完了
        if (this.transform.position.y < -10) {
            this.destroy = true;
            this.callback.SSActionEvent(this);      
        }
    }

    public override void Start() { }
}
