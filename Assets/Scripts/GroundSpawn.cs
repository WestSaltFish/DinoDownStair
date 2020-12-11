using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    public GameObject ground;
    public GameObject spikeGround;
    public GameObject finishGround;
    public Animator flyAnim;
    public float spawnTime;

    private bool lastSpikeGround = false;
    void Start()
    {
        // 重复调用生产地板
        InvokeRepeating(nameof(CreateGround), spawnTime, spawnTime);
    }

    /// <summary>
    /// 展示最后的画面
    /// </summary>
    /// <returns></returns>
    IEnumerator Finish()
    {
        while (finishGround.transform.position.y < -5.1) 
        {
            finishGround.transform.position += Vector3.up * Time.deltaTime * 1.5f;
            yield return null;
        }

        flyAnim.SetTrigger("Fly");
        GameController.gameController.GameFinished();
    }

    /// <summary>
    /// 创建地板
    /// </summary>
    private void CreateGround()
    {
        // 如果到达999分的话
        if (ScoreController.scoreController.score == 999) 
        {
            CancelInvoke();
            StartCoroutine(Finish());
        }
        else if (ScoreController.scoreController.score >= 10 && !lastSpikeGround)
        {
            GameObject g = Instantiate(spikeGround, transform);
            // 随机更改地板的位置
            g.GetComponent<GroundController>().ChangePos();
            lastSpikeGround = true;
        }
        else
        {
            GameObject g = Instantiate(ground, transform);
            // 随机更改地板的位置
            g.GetComponent<GroundController>().ChangePos();
            lastSpikeGround = false;
        }    
    }
}
