using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction {
    //得到分数
    int GetScore();
    //得到游戏结束标志
    bool isOver();
    //重新开始
    void Restart();
}