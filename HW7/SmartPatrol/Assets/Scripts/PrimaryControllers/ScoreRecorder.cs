using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
    public SceneController sceneController;
    private int score = 0;

    void Start() {
        sceneController = (SceneController)SSDirector.GetInstance().CurrentScenceController;
        sceneController.recorder = this;
    }
    public int GetScore() {
        return score;
    }
    public void Record() {
        score++;
    }
}