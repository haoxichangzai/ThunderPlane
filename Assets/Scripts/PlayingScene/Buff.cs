using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    //�ٶ�
    public int speed { get; private set; }

    //�洢��ͼ���Yֵ
    public float mapMaxY { get; private set; }
    //�洢��ͼ��СYֵ
    public float mapMinY { get; private set; }

    //��ǰBuff��
    public string buffName { get; private set; }

    //��ʼ��
    private void Start()
    {
        this.buffName = this.gameObject.tag;
        SetSpeed(4);
        //�õ���ͼ��������СYֵ
        mapMaxY = FindObjectOfType<Utils>().getMapMaxY();
        mapMinY = FindObjectOfType<Utils>().getMapMinY();
    }

    //�޸ĸ���
    private void FixedUpdate()
    {
        BuffMove();
        //���Buff���곬����������СYֵ������ֵ����������ͼ�⣬ִ�дݻ�
        if (transform.position.y > mapMaxY + 0.5 || transform.position.y < mapMinY - 0.5)
        {
            BuffDestroy();
        }
    }

    //Buff�ƶ�
    private void BuffMove()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }

    //�ݻ�Buff
    private void BuffDestroy()
    {
        Destroy(this.gameObject);
    }

    //�趨Buff�ƶ��ٶ�
    private void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    //buff����
    public void BuffIt()
    {
        switch (buffName)
        {
            case "BuffPower":
                FindObjectOfType<Player>().FirePowerUpgrade();
                break;
            case "BuffSpeed":
                FindObjectOfType<Player>().FireSpeedUpgrade();
                break;
            case "BuffHealth":
                FindObjectOfType<PlayerHP>().PlayerHPRec();
                break;
        }
    }

    //buff��Ч
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BuffIt();
            BuffDestroy();
        }
    }
}
