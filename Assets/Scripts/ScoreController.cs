using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController scoreController;

    public int score = 0;
    public Text scoreText;
    public Text highScoreText;
    public Text when100Text;
    public Animator when100;
    public string[] texts;

    private int test = 0;

    private int textCount = 0;
    private int changeColor = 0;
    private void Awake()
    {
        if (!scoreController)
        {
            scoreController = this;
        }
    }
    private void Start()
    {
        // 开局刷新一下最高分
        highScoreText.text = "HI " + PlayerPrefs.GetInt("HighScore", 0).ToString().PadLeft(3, '0');
    }
    /// <summary>
    /// 加分
    /// </summary>
    public void AddScore()
    {
        // 分数自增1
        score++;
        changeColor++;
        test++;
        // 把分数显示到屏幕上
        // PadLeft作用是在文字的左边补上想要的内容一直补到指定的单位为止, 下面的这个情况是一直补0, 补到三位数, 如果本身就是三位数就不需要补
        scoreText.text = score.ToString().PadLeft(3, '0');

        if (changeColor == 20)
        {
            changeColor = 0;
            GetComponent<ColorController1>().Change();
        }

        //if (test == 3)
        //{
        //    test = 0;
        //    when100Text.text = texts[textCount];
        //    when100.SetTrigger("100");
        //    textCount++;

        //}

        // 在特定时间段显示特定词语
        switch (score)
        {
            case 1:   case 5:   case 9:   case 14:   case 20:   case 30:  case 40:  case 50:
            case 60:  case 70:  case 80:  case 90:   case 100:  case 120: case 140: case 160:
            case 180: case 200: case 300: case 400:  case 450:  case 500: case 510: case 520:
            case 700: case 800: case 900: case 910:  case 920:  case 930: case 950: case 960:
            case 970: case 980: case 990: case 999:

                when100Text.text = texts[textCount];
                when100.SetTrigger("100");
                textCount++;
                break;
        }

        #region 剧情
        /* 
         * 1 哈喽 01
         * 2 欢迎开到 05
         * 3 小恐龙下楼梯 09
         * 4 很久很久以前 14
         * 5 有一只小恐龙 20
         * 6 他在下楼 30
         * 7 不断地下楼... 40
         * 8 没有人知道 50
         * 9 他下了多少层 60
         * 10 也没有人知道 70
         * 11 他下了多久 80
         * 12 就这样... 90
         * 13 Exelent! 100
         * 14 不断的往下走 120
         * 15 似乎 140
         * 16 这是他的使命 160
         * 17 嘿你瞧 180
         * 18 他已经到达200层了 200
         * 19 300层 300
         * 20 真是疯狂 400
         * 21 似乎很执着呢 450
         * 22 没有终点 500
         * 23 累了的话 510 
         * 24 就休息一下吧 520
         * 25 不知疲倦... 700
         * 26 这个游戏会结束吗... 800
         * 27 900了 900
         * 28 想知道 910
         * 29 到1000的时候 920
         * 30 会怎么样吗 930
         * 31 加油! 950
         * 32 快到了! 960
         * 33 还差一点! 970
         * 34 非常感谢... 980
         * 35 坚持到了这里 990
         * 36 一切都结束了... 999      
        */
        #endregion
    }

    /// <summary>
    /// 更新最高分
    /// </summary>
    public void HighScore()
    {
        // 更新最高分 并显示到屏幕上
        if (PlayerPrefs.GetInt("HighScore", 0) < score) 
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "HI " + score.ToString().PadLeft(3, '0');
        }
    }
}
