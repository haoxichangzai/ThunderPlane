using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// ������
/// 1.�ƶ�
/// 2.����
/// 3.������ʱ����PlayerHP��
/// 4.�����к�����޵�
/// 5.�ݻٱ�����
/// </summary>
public class Player : MonoBehaviour
{
    //�õ��ӵ���Ԥ����
    public GameObject bullet;
    //�����ȴ
    public float fireCold { get; private set; }
    //�����ȴ��ʱ��
    public float fireColdCount { get; private set; }

    //��ǰ������Ⱦ��
    public SpriteRenderer playerSpr { get; private set; }
    //�޵о�����Ⱦ��
    public SpriteRenderer unHurtSpr { get; private set; }

    //�Ƿ��޵�״̬
    public bool unHurt { get; private set; }

    //�����ȼ�
    public int firePowerLevel { get; private set; }
    //���ٵȼ�
    public int fireSpeedLevel { get; private set; }

    //��Դ���
    private AudioSource audioSource;
    //��Ч���� 0 Buffed ; 1 UnHurt
    public AudioClip[] audioClips;

    //�õ���ըԤ����
    public GameObject PlayerBoom;

    private void Awake()
    {
        ResetState();
        unHurt = false;
        playerSpr = GetComponent<SpriteRenderer>();
        unHurtSpr = GameObject.Find("UnHurt").GetComponent<SpriteRenderer>();
        //��ȡ��Դ���
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //�Ȳ�����
        audioSource.Stop();
        //ѭ��
        audioSource.loop = true;
    }

    public void ResetState()
    {
        firePowerLevel = 1;
        fireSpeedLevel = 1;
        fireColdCount = 0f;
        SetFireCold(0.3f);
    }

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Playing")
        {
            //ͨ����ȴ�������ӵ�
            FireCold();
        }

        UnHurtAnim();
    }

    //�����ӵ�
    private void PlayerFire()
    {
        //ʵ�����ӵ��������ֱ�Ϊ��ʵ��������Ϸ���󣬶���λ�ã�����Ƕ�
        if (this.firePowerLevel == 1)
        {
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);//����
        }
        else if (this.firePowerLevel == 2)
        {
            Instantiate(bullet, new Vector3(transform.position.x - 0.3f, transform.position.y, transform.position.z), transform.rotation);//����
            Instantiate(bullet, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), transform.rotation);//����
        }
        else if (this.firePowerLevel == 3)
        {
            Instantiate(bullet, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.2f, transform.position.z), transform.rotation);//����
            Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);//����
            Instantiate(bullet, new Vector3(transform.position.x + 0.5f, transform.position.y - 0.2f, transform.position.z), transform.rotation);//����
        }

    }

    //�ӵ���ȴ������ȴʱ�䵽���ܷ����ӵ�
    private void FireCold()
    {
        if (fireColdCount >= fireCold)
        {
            PlayerFire();
            fireColdCount = 0;
        }
        else
        {
            fireColdCount += Time.deltaTime;
        }
    }

    //������
    private void GotShot()
    {
        //�жϵ�ǰ�Ƿ��޵�״̬���ǵĻ����أ����Ǿͱ����
        if (!unHurt)
        {
            FindObjectOfType<PlayerHP>().GotShot();//�ȿ�Ѫ
            OnUnHurt();//���޵�
            Invoke(nameof(OffUnHurt), 1.0f);//3���ȡ���޵�
        }
        else
        {
            return;
        }
    }

    //��������ȴ��ֵ
    public void SetFireCold(float time)
    {
        this.fireCold = time;
    }

    //��ұ��ݻ�
    public void PlayerDestroy()
    {
        Instantiate(PlayerBoom, transform.position, transform.rotation);//��ը����
        this.gameObject.SetActive(false);
    }

    //�趨Ϊ�޵�״̬
    public void OnUnHurt()
    {
        this.unHurt = true;

        audioSource.clip = audioClips[1];
        audioSource.loop = true;
        audioSource.Play();
    }

    //ȡ���޵�״̬
    public void OffUnHurt()
    {
        this.unHurt = false;
        //ֹͣ����
        audioSource.Stop();
    }

    //�����Ƿ��޵�״̬�����޵ж���
    public void UnHurtAnim()
    {
        if (unHurt)
        {
            playerSpr.enabled = false;
            unHurtSpr.enabled = true;
        }
        else
        {
            playerSpr.enabled = true;
            unHurtSpr.enabled = false;
        }
    }

    public void FirePowerUpgrade()
    {
        if (this.firePowerLevel < 3)
        {
            firePowerLevel += 1;
            FindObjectOfType<PlayingManager>().SetFPLUI();

            audioSource.clip = audioClips[0];
            audioSource.loop = false;
            audioSource.Play();
        }
        else
        {
            return;
        }
    }

    public void FireSpeedUpgrade()
    {
        if(this.fireSpeedLevel < 3)
        {
            fireSpeedLevel += 1;
            SetFireCold(this.fireCold -0.1f);
            FindObjectOfType<PlayingManager>().SetFSLUI();

            audioSource.clip = audioClips[0];
            audioSource.loop = false;
            audioSource.Play();
        }
        else
        {
            return;
        }
    }
}
