using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour, IUserAction, ISceneController{

    public GuardFactory guardFactory;  //巡逻兵工厂
    public ScoreRecorder recorder;  //记分员
    public GuardActionManager actionManager;    //运动管理器
    public List<GameObject> cells;  //每个小房间
    public GameObject player;   //玩家
    public UserGUI gui;     //交互界面

    public PlayerEventPublisher playerEventPublisher;   //事件发布者

    private List<GameObject> guards;    //场景中所有的巡逻兵
    private bool game_over = false;     //游戏结束

    private void Start() {
        //初始化这个脚本的相关控件
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        guardFactory = gameObject.AddComponent<GuardFactory>() as GuardFactory;
        recorder = gameObject.AddComponent<ScoreRecorder>() as ScoreRecorder;
        actionManager = gameObject.AddComponent<GuardActionManager>() as GuardActionManager;
        gui = gameObject.AddComponent<UserGUI>() as UserGUI;

        LoadResources();
    }

    /*ISceneController接口相关函数*/
    public void LoadResources(){
        //加载房间
        for(int i=0;i<9;i++){
            GameObject cell = Instantiate(Resources.Load<GameObject>("Prefabs/Cell"));
            int a = i % 3;
            int b = i / 3;
            cell.transform.position = new Vector3(12 * a, 0, 12 * b);
            cells.Add(cell);
        }
        
        //加载玩家
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(24, 0, 24), Quaternion.identity) as GameObject;
        playerEventPublisher = player.GetComponent<PlayerEventPublisher>();     

        //加载巡逻兵
        guards = guardFactory.GetGuards();

        //让每个巡逻兵都订阅玩家的事件发布者
        //并让每个巡逻兵调用动作管理器开始走动
        for (int i = 0; i < guards.Count; i++) {
            IEventHandler e = guards[i].GetComponent<PatrolData>() as IEventHandler;
            playerEventPublisher.AddListener(e);
            actionManager.GuardWalk(guards[i], player);
        }
    }

    /*IUserAction接口相关函数*/
    public int GetScore() {
        return recorder.GetScore();
    }

    public bool isOver() {
        return game_over;
    }

    public void Restart() {
        //重新生成这个场景即可
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    /*动作管理器会回调的函数*/
    public void Record() {
        recorder.Record();
    }

    /*玩家管理器会回调的函数*/
    public void Gameover() {
        game_over = true;
    }
}