using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// 1.�ƶ�
/// 2.����
/// 3.�ݻٱ�����
/// 4.����ҷ�����ײ
/// </summary>
public class Enemy : MonoBehaviour
{
    //�ƶ��ٶ�
    public int speed { get; private set; }

    //�õ��ӵ�Ԥ����
    public GameObject Bullet;
    //������ȴ
    public float fireCold { get; private set; }

    //�洢��ͼ���Yֵ
    public float mapMaxY { get; private set; }
    //�洢��ͼ��СYֵ
    public float mapMinY { get; private set; }

    //�Ƿ��޵�״̬
    public bool unHurt { get; private set; }

    //�õ���ըԤ����
    public GameObject EnemyBoom;

    //�õ�BuffԤ����
    public GameObject[] BuffList;
    //���ֵ�Buff����
    public int buffNum { get; private set; }

    //����
    public int score { get; private set; }

    private void Awake()
    {
        SetSpeed(8);
        SetFireCold(1.2f);
        mapMaxY = FindObjectOfType<Utils>().getMapMaxY();
        mapMinY = FindObjectOfType<Utils>().getMapMinY();
        unHurt = true;
        score = 1;
        buffNum = 1;
    }

    //�л���ͼ����
    private void FixedUpdate()
    {
        EnemyMove();
        FireCold();
        //�з�������ͼ��ΧҲ����ݻ٣����ǲ������
        if (transform.position.y < mapMinY)
        {
            Destroy(this.gameObject);
        }
    }

    //�ƶ�
    private void EnemyMove()
    {
        transform.Translate(-transform.up*speed*Time.deltaTime);
    }

    //�����ӵ�
    private void EnemyFire()
    {
        Instantiate(Bullet, transform.position, transform.rotation);
    }

    //�ӵ���ȴ������ȴʱ�䵽���ܷ����ӵ�
    private void FireCold()
    {
        if (fireCold >= 1.2f)
        {
            EnemyFire();
            fireCold = 0;
        }
        else
        {
            fireCold += Time.deltaTime;
        }
    }

    //�趨�ƶ��ٶ�
    private void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    //�趨������ȴ
    private void SetFireCold(float firecold)
    {
        this.fireCold = firecold;
    }

    //�����дݻ�
    private void EnemyDestroy()
    {
        //�з�û���޵�״̬���ܽ���ݻ�
        if (!isUnHurt())
        {
            FindObjectOfType<PlayingManager>().ScoreRise(score);//�ӷ�
            Instantiate(EnemyBoom, transform.position, transform.rotation);//��ը����
            //����Buff
            BuffDrop();
            Destroy(this.gameObject);//�ݻ��Լ�
        }
    }

    //�з��Ƿ��޵�
    private bool isUnHurt()
    {
        //��δ����ָ���߶�ʱ
        if (transform.position.y > mapMaxY - 0.3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //����ҷ�����ײ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //����ܻ����ݻٵ��˶���
            collision.collider.SendMessage("GotShot");
            EnemyDestroy();
        }
    }

    //����Buff
    private void BuffDrop()
    {
        if (!FindObjectOfType<PlayingManager>().isBossSpawn)
        {
            if (Random.Range(1, 3) == 1 || (this.score / this.buffNum > 30))//��������֮һ�ĸ���,���߷�����buff����������30 : 1(��ù��)
            {
                buffNum += 1;
                //����ֵ�����������ֻ������ѪBuff
                if (FindObjectOfType<PlayerHP>().playerHP != 3)
                {
                    Instantiate(BuffList[2], transform.position, transform.rotation);
                }
                //����������ֵ���
                else
                {
                    Instantiate(BuffList[Random.Range(0, 2)], transform.position, transform.rotation);
                }
            }
        }
        else
        {
            return;
        }
    }
}
