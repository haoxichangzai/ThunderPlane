using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// 1.������������겢�����ڵ�
/// 2.��������
/// 3.����Boss
/// </summary>
public class ShellFire : MonoBehaviour
{
    //�ڵ�Ԥ����
    public GameObject Shell;
    //�ڵ�������ȴ
    public float fireCold { get; private set; }
    //�ڵ�������ȴ��ʱ��
    public float fireColdCount { get; private set; }

    //�洢��ͼ��СXֵ
    public float mapMinX { get; private set; }
    //�洢��ͼ���Xֵ
    public float mapMaxX { get; private set; }

    //�����������
    private Vector3 randomX;

    private void Awake()
    {
        //�ڵ�������ȴʱ��
        fireCold = 0.9f;
        fireColdCount = 0.9f;
        //�õ���ͼ����������Сֵ
        mapMinX = FindObjectOfType<Utils>().getMapMinX();
        mapMaxX = FindObjectOfType<Utils>().getMapMaxX();
    }

    private void FixedUpdate()
    {
        ShellFireCold();
    }

    //������ȴ��
    private void ShellFireCold()
    {
        if (fireColdCount >= fireCold)
        {
            //ÿ�ζ�������֮һ�ĸ��ʲ�����
            if (Random.Range(1,3) > 1)
            {
                Instantiate(Shell, new Vector3(GetRandomX().x, transform.position.y, transform.position.z), transform.rotation);
                fireColdCount = 0;
            }
        }
        else
        {
            fireColdCount += Time.deltaTime;
        }
    }

    //���һ������ĺ�������
    private Vector3 GetRandomX()
    {
        randomX.x = Random.Range(mapMinX + 0.3f, mapMaxX - 0.3f);
        return randomX;
    }
}
