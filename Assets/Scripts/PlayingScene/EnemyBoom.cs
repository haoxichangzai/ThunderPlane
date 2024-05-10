using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�з�ս����ը
public class EnemyBoom : MonoBehaviour
{
    //��Դ���
    private AudioSource audioSource;
    private void Start()
    {
        //��ȡ��Դ���
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //����
        audioSource.Play();
        //��ѭ��
        audioSource.loop = false;
        Invoke(nameof(Destroy), 0.2f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
