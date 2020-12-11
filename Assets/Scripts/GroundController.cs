using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D groundRig;
    private bool playerIn = false;
    void Start()
    {
        groundRig = GetComponent<Rigidbody2D>();
        groundRig.velocity = Vector2.up * velocity;
        // 6秒后销毁地板
        Destroy(gameObject, 6f);
    }
    /// <summary>
    /// 随机更换x轴位置
    /// </summary>
    public void ChangePos()
    {
        transform.position += Vector3.right * Random.Range(-3f, 3f);
    }

    private void Update()
    {
        // 如果玩家站在地板上按下的话,就会掉下去
        if (playerIn && GetComponent<BoxCollider2D>().enabled) 
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }              
        }
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 判断玩家是否进入地板
        if (collision.collider.CompareTag("Player"))
        {
            playerIn = true;
        }
    }
}
