using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager {
    public DiskFlyAction fly;
    public PhysicsDiskFlyAction pfly;
    public SceneController scene_controller;           

    protected void Start() {
        scene_controller = (SceneController)SSDirector.GetInstance().CurrentScenceController;
        scene_controller.action_manager = this;     
    }

    //运动学运动
    public void DiskFly(GameObject disk, float angle, float power) {
        disk.GetComponent<Rigidbody>().isKinematic = true;
        int lor = 1;
        if (disk.transform.position.x > 0) lor = -1;
        fly = DiskFlyAction.GetSSAction(lor, angle, power);
        this.RunAction(disk, fly, this);
    }

    //物理学运动
    public void physicsDiskFly(GameObject disk, float power)
    {
        disk.GetComponent<Rigidbody>().isKinematic = false;
        int lor = 1;
        if (disk.transform.position.x > 0) lor = -1;
        pfly = PhysicsDiskFlyAction.GetSSAction(lor, power);
        this.RunAction(disk, pfly, this);
    }
}
