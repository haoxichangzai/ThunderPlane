using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// 1.�ƶ�
/// 2.��������
/// 3.�ݻٱ�����
/// </summary>
public class Bullet : MonoBehaviour
{
    //�ٶ�
    public int speed { get; private set; }

    //�洢��ͼ���Yֵ
    public float mapMaxY { get; private set; }
    //�洢��ͼ��СYֵ
    public float mapMinY { get; private set; }

    //�Ƿ������������ӵ�
    public bool isPlayerBullet;

    private void Start()
    {
        SetSpeed(10);
        //�õ���ͼ��������СYֵ
        mapMaxY = FindObjectOfType<Utils>().getMapMaxY();
        mapMinY = FindObjectOfType<Utils>().getMapMinY();
    }

    private void FixedUpdate()
    {
        BulletMove();
        //����ӵ����곬����������СYֵ������ֵ����������ͼ�⣬ִ�дݻ�
        if (transform.position.y > mapMaxY + 0.5 || transform.position.y < mapMinY - 0.5)
        {
            BulletDestroy();
        }
    }

    //�ӵ��ƶ�
    private void BulletMove()
    {
        //����ӵ������ƶ�
        if(isPlayerBullet)
        {
            transform.Translate(transform.up * speed * Time.deltaTime);
        }
        //�з��ӵ������ƶ�
        else
        {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }
    }

    //�ӵ���������
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {
            case "Player":
                //�з��ӵ�
                if (!isPlayerBullet)
                {
                    collider.SendMessage("GotShot");
                    BulletDestroy();
                }
                break;
            case "Enemy":
                //����ӵ�
                if (isPlayerBullet)
                {
                    collider.SendMessage("EnemyDestroy");
                    BulletDestroy();
                }
                break;
            case "Boss":
                //����ӵ�
                if (isPlayerBullet)
                {
                    collider.SendMessage("GotShot");
                    BulletDestroy();
                }
                break;
        }

    }

    //�ݻ��ӵ�
    private void BulletDestroy()
    {
        Destroy(this.gameObject);
    }

    private void SetSpeed(int speed)
    {
        this.speed = speed;
    }
}
