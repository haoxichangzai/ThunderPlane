using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pointer : MonoBehaviour
{
    //����������ָ��
 
    //��ȡ��������Ļ����
    Vector3 mousePositionOnScreen;
    //���������������ת��Ϊ����Ļ����
    Vector3 screenPosition;
    //��������Ļ����ת��Ϊ����������
    Vector3 mousePositionInWorld;

    //�洢��ͼ���Yֵ
    public float mapMaxY { get; private set; }
    //�洢��ͼ��СYֵ
    public float mapMinY { get; private set; }    
    //�洢��ͼ���Xֵ
    public float mapMaxX { get; private set; }
    //�洢��ͼ��СXֵ
    public float mapMinX { get; private set; }

    private void Awake()
    {
        //������걾����
        Cursor.visible = false;

        if (SceneManager.GetActiveScene().name == "Playing")
        {
            //�õ���ͼ�������Сֵ
            mapMaxY = FindObjectOfType<Utils>().getMapMaxY();
            mapMinY = FindObjectOfType<Utils>().getMapMinY();
            mapMaxX = FindObjectOfType<Utils>().getMapMaxX();
            mapMinX = FindObjectOfType<Utils>().getMapMinX();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Playing")
        {
            //��������ָ��������ָ�뷽��
            CrossFollowMouse();
        }
    }

    //����ָ��������ָ�뷽��
    private void CrossFollowMouse()
    {
        //��ȡ����ڳ���������
        mousePositionOnScreen = Input.mousePosition;

        //��ȡ���������������е�λ�ã���ת��Ϊ��Ļ���ꣻ
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //����������Z������ ���� �����������Z������
        mousePositionOnScreen.z = screenPosition.z;
        //��������Ļ����ת��Ϊ��������
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);

        //�����곬����ͼ�߽磬�Ͱ����ֵ��������ʹ����Ҳ��ܳ����߽�
        if (mousePositionInWorld.x > mapMaxX)
        {
            mousePositionInWorld.x = mapMaxX;
        }
        if (mousePositionInWorld.x < mapMinX)
        {
            mousePositionInWorld.x = mapMinX;
        }
        if (mousePositionInWorld.y > mapMaxY)
        {
            mousePositionInWorld.y = mapMaxY;
        }
        if (mousePositionInWorld.y < mapMinY)
        {
            mousePositionInWorld.y = mapMinY;
        }

        //������������Ϊ�����������꣬�����������ƶ�
        transform.position = mousePositionInWorld;

    }

}
