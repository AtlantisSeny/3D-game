using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFactory : MonoBehaviour
{
    private GameObject guard = null;                               //巡逻兵
    private List<GameObject> used = new List<GameObject>();        //正在使用的巡逻兵列表
    private Vector3[] vec = new Vector3[9];                        //每个巡逻兵的初始位置

    public List<GameObject> GetGuards() {
        //为每一个巡逻兵，在一定范围内随机生成位置。
        int[] pos_x = {-5, 7, 19};
        int[] pos_z = {-5, 7, 19};
        int index = 0;
        for(int i=0;i < 3;i++) {
            for(int j=0;j < 3;j++) {
                pos_x[i] += Random.Range(0,3);
                pos_z[i] += Random.Range(0,3);
                vec[index] = new Vector3(pos_x[i], 0, pos_z[j]);
                index++;
            }
        }

        //加载巡逻兵，初始化位置和sign，并放在数组中。
        for(int i = 0; i < 8; i++) {
            guard = Instantiate(Resources.Load<GameObject>("Prefabs/Guard"));
            guard.transform.position = vec[i];
            guard.GetComponent<PatrolData>().sign = i;
            guard.GetComponent<PatrolData>().start_position = vec[i];
            used.Add(guard);
        }   
        return used;
    }
}
