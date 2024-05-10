using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// 1.�ܵ�����
/// 2.ͨ��Ѫ���ж���Ϸ�Ƿ���Ҫ����
/// </summary>
public class PlayerHP : MonoBehaviour
{
    //���Ѫ��
    public int playerHP = 3;
    //������Ѫ��
    private int playerHPMax = 3;

    //��ǰ����
    public SpriteRenderer spr;
    //Ѫ����������
    public Sprite[] HPBar;

    //��Դ���
    public AudioSource audioSource;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        ResetState();

        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.loop = false;
    }

    //����״̬
    public void ResetState()
    {
        SetPlayerHP(3);
    }

    private void Update()
    {
        UpdateHPBar();
        if (playerHP == 0)
        {
            PlayerDead();
        }
    }

    //�������Ѫ��
    public void SetPlayerHP(int HP)
    {
        this.playerHP = HP;
    }

    //�����ָ�
    public void PlayerHPRec()
    {
        SetPlayerHP(this.playerHP + 1);
        audioSource.Play();
    }

    //ͨ���������ֵ����Ѫ������
    private void UpdateHPBar()
    {
        if (spr != null && playerHP >= 0 && playerHP <= playerHPMax)
        {
            switch (playerHP)
            {
                case 0:
                    spr.sprite = HPBar[0];
                    break;
                case 1:
                    spr.sprite = HPBar[1];
                    break;
                case 2:
                    spr.sprite = HPBar[2];
                    break;
                case 3:
                    spr.sprite = HPBar[3];
                    break;
            }
        }
    }

    //������
    public void GotShot()
    {
        SetPlayerHP(playerHP-1);
    }

    //ͨ���������ֵ�ж���Ϸ�Ƿ���Ҫ����
    private void PlayerDead()
    {
        FindObjectOfType<PlayingManager>().GameOver(false);
    }
}
