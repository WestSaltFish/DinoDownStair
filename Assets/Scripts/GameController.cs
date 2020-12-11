using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public GameObject ResetBtn;
    private void Awake()
    {
        // 初始化单例模式
        if (!gameController)
        {
            gameController = this;
        }     
    }
    /// <summary>
    /// 玩家死亡后的相关处理
    /// </summary>
    public void GameFinished()
    {
        // 显示重新开始的按钮
        ResetBtn.SetActive(true);
        // 更新最高分数
        ScoreController.scoreController.HighScore();
    }
    /// <summary>
    /// 重启游戏
    /// </summary>
    public void RestartGame()
    {
        // 重新加载第一个场景
        SceneManager.LoadScene(0);
    }
}
