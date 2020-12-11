using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController1 : MonoBehaviour
{
    public Material mat;
    public float changeSpeed;
    public bool black = false;

    private void Start()
    {
        mat.SetFloat("_Inverse", 0);
    }

    /// <summary>
    /// 反转画面颜色
    /// </summary>
    public void Change()
    {
        black = !black;
        if (black)
        {
            StartCoroutine(BeBlack());
        }
        else
        {
            StartCoroutine(BeWhite());
        }
    }

    /// <summary>
    /// 转为主黑
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeBlack()
    {
        while (mat.GetFloat("_Inverse") < 1)
        {
            mat.SetFloat("_Inverse", mat.GetFloat("_Inverse") + Time.deltaTime * changeSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// 转为主白
    /// </summary>
    /// <returns></returns>
    private IEnumerator BeWhite()
    {
        while (mat.GetFloat("_Inverse") > 0)
        {
            mat.SetFloat("_Inverse", mat.GetFloat("_Inverse") - Time.deltaTime * changeSpeed);
            yield return null;
        }
    }
}
