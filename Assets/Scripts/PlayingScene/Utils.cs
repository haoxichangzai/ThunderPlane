using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ࣺ
/// 1.��õ�ͼ�����С��x��yֵ
/// </summary>
public class Utils : MonoBehaviour
{
    //��ͼ�߽磬�ֱ�װ�е�ͼ ���Ͻ� �� ���½� ������
    public GameObject[] mapScope;

    //��ȡ��ͼ���Yֵ
    public float getMapMaxY()
    {
        return mapScope[0].transform.position.y;
    }

    //��ȡ��ͼ��СYֵ
    public float getMapMinY()
    {
        return mapScope[1].transform.position.y;
    }

    //��ȡ��ͼ���Xֵ
    public float getMapMaxX()
    {
        return mapScope[0].transform.position.x;
    }

    //��ȡ��ͼ��СXֵ
    public float getMapMinX()
    {
        return mapScope[1].transform.position.x;
    }
}
