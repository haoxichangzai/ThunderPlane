using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������
/// 1.�ܵ�����
/// 2.Ѫ����ͬ�׶α�ɫ
/// 3.ͨ��Ѫ���ж���Ϸ�Ƿ���Ҫ����
/// </summary>
public class BossHP : MonoBehaviour
{
    //Ѫ��
    public int bossHP { get; private set; }

    public Slider bossHPBar;

    public TextMeshProUGUI bossHPNum;

    private Image image;

    //���Buff���
    //�����ȼ�
    public int playerFirePowerLevel { get; private set; }
    //����
    public float playerFireCold { get; private set; }

    //���伤�������
    public int laserCount { get; private set; }

    private void Awake()
    {

        //��ȡ��ҵ�ǰBuff���
        this.playerFirePowerLevel = FindObjectOfType<Player>().firePowerLevel;
        this.playerFireCold = FindObjectOfType<Player>().fireCold;

        SetBossHP();

        bossHPBar.maxValue = bossHP;
        bossHPBar.value = bossHP;

        //��ȡSlider�ڲ�Fill�Ӷ����Image���
        image = GameObject.Find("Fill").GetComponent<Image>();

        laserCount = 0;
    }

    //��ǰѪ��
    private void Update()
    {
        bossHPBar.value = bossHP;
        bossHPNum.text = bossHP.ToString();

        SetSliderColor();
        if (bossHP <= 0)
        {
            this.gameObject.SetActive(false);
            BossDead();
        }
    }

    public void GotShot()
    {
        bossHP--;
    }

    //�޸Ľ�������ɫ
    private void SetSliderColor()
    {

        //����Slider�ı�����ֵ/���ֵ��
        float progress = bossHPBar.value / bossHPBar.maxValue;

        if (progress < 0.66 && progress > 0.33)
        {
            //��������� 0.33 �� 0.66 ֮�䣬��Slider����ɫ����Ϊ��ɫ��3�������ֱ�Ϊ���̻ƣ�
            Color yellow = new Color(1f, 1f, 0f);
            image.color = yellow;
            if(laserCount == 0)
            {
                Lasing();
            }
        }
        else if (progress < 0.33 && progress > 0)
        {
            //��������� 0 �� 0.33 ֮�䣬��Slider����ɫ����Ϊ��ɫ
            Color red = new Color(1f, 0f, 0f);
            image.color = red;
            if (laserCount == 1)
            {
                Lasing();
            }
        }
    }

    //����BossѪ��
    public void SetBossHP()
    {
        //������ҵ����ٺͻ�������buff��ȷ��Boss��Ѫ����ȷ�����������ʲô״̬��Boss�����Դ��60������
        bossHP = Mathf.FloorToInt(playerFirePowerLevel * 60f / playerFireCold);
    }

    //ͨ��Boss����ֵ�ж���Ϸ�Ƿ���Ҫ����
    private void BossDead()
    {
        FindObjectOfType<PlayingManager>().GameOver(true);
    }

    //�жϷ��伤��
    public void Lasing()
    {
        laserCount += 1;
        FindObjectOfType<Boss>().IsMove(false);
        FindObjectOfType<Boss>().Lasing();
    }
}
