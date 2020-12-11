using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(-10, -1)]
    public float fallSpeed;
    public float velocityMod;
    public bool isDie = false;
    public AudioSource dieAu;
    public AudioSource getPoint;

    private Rigidbody2D playerRig;
    private SpriteRenderer playerSp;
    private Animator playerAnim;
    private Vector2 playerVelocity;
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
        playerSp = GetComponent<SpriteRenderer>();
        playerAnim = GetComponent<Animator>();
        playerVelocity = Vector2.zero;
    }

    private void Update()
    {
        // 玩家未死亡的时候
        if (!isDie)
        {
            // 动画切换逻辑
            // 如果速度不为0
            // 需要使用自己定义的速度判定,因为直接拿刚体的速度来判定, 撞墙的时候会出错
            if (playerVelocity.x != 0)
            {
                // 动画状态变为移动
                playerAnim.SetBool("isRun", true);

                // 根据速度调整图片的左右反转
                if (playerVelocity.x < 0)
                {
                    playerSp.flipX = true;
                }
                else if (playerVelocity.x > 0)
                {
                    playerSp.flipX = false;
                }
            }
            // 如果没有在动的话把动画调整为静止
            else
            {
                playerAnim.SetBool("isRun", false);
            }
        }
        else
        {
            // 空格重启游戏
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameController.gameController.RestartGame();
            }
        }

        // 控制掉落速度
        if (playerRig.velocity.y < fallSpeed)
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, fallSpeed);
        }

        // 如果摔下去的话就死亡
        if(!isDie && transform.position.y < -7f)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        // 移动逻辑
        if (!isDie)
        {
            // 保存住应该需要移动的速度
            playerVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * velocityMod, playerRig.velocity.y);
            // 把速度赋值到刚体上
            playerRig.velocity = playerVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果踩到平台的话
        if (collision.collider.CompareTag("Ground") && collision.GetContact(0).normal.y == 1f) 
        {
            // 加分
            ScoreController.scoreController.AddScore();
            getPoint.Play();
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果碰到尖刺的话
        if (collision.CompareTag("Spikes"))
        {
            // 死亡
            Die();
        }
    }

    private void Die()
    {
        dieAu.Play();
        // 调整人物状态
        isDie = true;
        // 播放死亡动画
        playerAnim.SetTrigger("Die");
        // 速度归零后让重力自然把玩家掉落下去
        playerRig.velocity = Vector2.zero;
        // 关掉碰撞器
        GetComponent<BoxCollider2D>().enabled = false;
        // 运行游戏控制器中关于玩家死亡后的相关代码
        GameController.gameController.GameFinished();
    }
}
