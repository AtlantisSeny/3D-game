using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardActionManager : SSActionManager, ISSActionCallback {
    private GameObject player;    //玩家
    public SceneController sceneController;

    private void Start() {
        sceneController = SSDirector.GetInstance().CurrentScenceController as SceneController;
        sceneController.actionManager = this;
    }
    public void GuardWalk(GameObject guard, GameObject player) {
        //首先需要让巡逻兵走动
        this.player = player;
        GuardWalkAction walkAction = GuardWalkAction.GetSSAction(guard.transform.position);
        this.RunAction(guard, walkAction, this);
    }

    public void SSActionEvent(
        SSAction source, SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, GameObject objectParam = null) {
        if (intParam == 0) {
            //巡逻状态返回，开始追逐
            GuardRunAction runAction = GuardRunAction.GetSSAction(player);
            this.RunAction(objectParam, runAction, this);
        } else if(intParam == 1){
            //追逐状态返回，开始巡逻
            GuardWalkAction walkAction = GuardWalkAction.GetSSAction(objectParam.GetComponent<PatrolData>().start_position);
            this.RunAction(objectParam, walkAction, this);
            sceneController.Record();
        }
        //对于其他状态，则是动作直接销毁，不再生成新动作
    }
}
   
