using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// 1.�ƶ�
/// 2.����
/// 3.������ʱ����BossHP��
/// 4.�ݻٱ�����
/// </summary>
public class Boss : MonoBehaviour
{
    //�ƶ��ٶ�
    public float speed { get; private set; }
    //���߶�
    public float siteY { get; private set; }
    //�Ƿ��ƶ�
    public bool isMove { get; private set; }

    //�����ƶ�����ֵ
    public int right { get; private set; }
    //�Ƿ��޵�״̬
    public bool unHurt { get; private set; }

    //�õ��ӵ�Ԥ����
    public GameObject bullet;
    //������ȴ
    public float fireCold { get; private set; }

    //�õ���ըԤ����
    public GameObject BossBoom;

    //�õ�����Ԥ����
    public GameObject Laser;

    //����
    public int score { get; private set; }

    private void Awake()
    {
        speed = 2;
        siteY = 7.0f;
        IsMove(true);
        right = 1;
        score = 100;
        unHurt = true;//����ʱBoss��δ��λ��Ѫ�������֣�Ϊ�޵�״̬
    }

    //��ʼ��
    private void Update()
    {
        if (isMove)
        {
            BossMove();
        }
        FireCold();
    }

    //boss�ƶ�
    private void BossMove()
    {
        //��δ����ָ���߶�ʱ�������ƶ�
        if (transform.position.y > siteY)
        {
            transform.Translate(-transform.up * speed * Time.deltaTime);
        }
        //����ָ���߶Ⱥ����Һ����ƶ�
        else
        {
            unHurt = false;
            FindObjectOfType<PlayingManager>().SetBossHPActive();
            transform.Translate(transform.right * right * speed * Time.deltaTime);
            //�������ұ߽�����෴�����ƶ�
            if (Mathf.Abs(transform.position.x) >= 4)
            {
                right = -right;
            }
        }
    }

    //�����ӵ�
    private void BossFire()
    {
        Instantiate(bullet, new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z), transform.rotation);//����
        Instantiate(bullet, new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z), transform.rotation);//����
    }

    //�ӵ���ȴ������ȴʱ�䵽���ܷ����ӵ�
    private void FireCold()
    {
        if (fireCold >= 1.2f)
        {
            BossFire();
            fireCold = 0;
        }
        else
        {
            fireCold += Time.deltaTime;
        }
    }

    //Boss������
    private void GotShot()
    {
        //�����ʱ�����޵�״̬
        if (!unHurt)
        {
            FindObjectOfType<BossHP>().GotShot();
        }
    }

    //Boss���ݻ�
    public void BossDestroy()
    {
        FindObjectOfType<PlayingManager>().ScoreRise(100);
        Instantiate(BossBoom, transform.position, transform.rotation);//��ը����
        Destroy(this.gameObject);//�ݻ��Լ�
    }

    //�����Ƿ��ƶ�
    public void IsMove(bool isMove)
    {
        this.isMove = isMove;
    }

    //���伤��
    public void Lasing()
    {
        Instantiate(Laser, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z), transform.rotation);
    }

    //����ҷ�����ײ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            //����ܻ���Boss�ܻ�
            collision.collider.SendMessage("GotShot");
            GotShot();
        }
    }
}
