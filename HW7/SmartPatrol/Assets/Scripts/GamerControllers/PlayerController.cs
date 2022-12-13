using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;  //动画组件
    private new Rigidbody rigidbody;    //刚体组件
    private new Camera camera;  //视角跟随相机组件
    private Vector3 offset;     //相机偏移量

    public float walk_speed = 3.0f; //行走速度
    public float run_speed = 6.0f;  //跑步速度
    private float speed;    //当前速度
    private float rotate_speed = 3.0f;//旋转速度
    private float camera_rotate;    //当前旋转速度
    public bool isLive = true;      //游戏是否进行中

    void Start()
    {
        //获取主相机
        camera = Camera.main;

        //设置相机的偏移
        offset = this.transform.position;
        camera.transform.position = offset + new Vector3(0, 2, -4);

        //需要刚体组件来协助移动
        rigidbody = GetComponent<Rigidbody>();

        //初始状态为行走
        speed = walk_speed;     

        //获取动画组件
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        //获取方向键输入
        Vector3 vel = rigidbody.velocity;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        bool isMove = (Mathf.Abs(v) > 0.05 || Mathf.Abs(h) > 0.05);//防止误触

        //判断游戏是否进行，来确定是否可以采用走路、跑步动画
        animator.SetBool("isLive", isLive);

        //如果有方向键变化，则设置动画为走
        if(isMove){
            animator.SetBool("isWalk", true);
        }
        if(!isMove){
            animator.SetBool("isWalk", false);
        }

        //Shift切换走跑动作
        if(Input.GetKeyDown("left shift")){
            bool ret = animator.GetBool("isRun");
            animator.SetBool("isRun", !ret);
            speed = run_speed;
        }

        //在游戏进行状态下，控制角色移动
        if(isMove && isLive){
            //获得相机的正对方向
            camera_rotate = camera.transform.eulerAngles.y / 180 * Mathf.PI;

            //给刚体一个速度，让角色向镜头方向，并综合方向键进行移动
            float sr = Mathf.Sin(camera_rotate);
            float cr = Mathf.Cos(camera_rotate);
            rigidbody.velocity = new Vector3((v * sr + h * cr) * speed, 0, (v * cr - h * sr) * speed);

            //角色也要面向镜头前方的位置
            transform.rotation = Quaternion.LookRotation(new Vector3((v * sr + h * cr), 0, (v * cr - h * sr)));
        }
        else{
            rigidbody.velocity = Vector3.zero;
        }
    }

    /*采用LateUpdate来加载相机的相关属性*/
    private void LateUpdate() {
        //更新相机的位置，跟随
        camera.transform.position += this.transform.position - offset;
        offset = this.transform.position;

        //鼠标控制相机水平移动
        float mouseX = Input.GetAxis("Mouse X") * rotate_speed;
        camera.transform.RotateAround(this.transform.position, Vector3.up, mouseX);
    }
}
