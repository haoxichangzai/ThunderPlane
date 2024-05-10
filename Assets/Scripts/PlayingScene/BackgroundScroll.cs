using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʵ��ԭ��
/// ����ͼƬWrap Mode����Ϊ Repeat
/// �½�һ��Material�����ļ������ʵ�Shader����ΪTexture
/// �Ѳ��ʸ���ͼƬ��Sprite Renderer
/// ͨ�����Ĳ����е�offSet����ʵ�ֱ����Ĺ���
/// </summary>
public class BackgroundScroll : MonoBehaviour
{
    [Tooltip("�ƶ��ٶ�"), Range(0.01f, 1f)]

    public float moveSpeed;
    private SpriteRenderer render;

    //��ʼ��
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    //��ʼ��
    void Update()
    {
        BgScroll();
    }
    /// <summary>
    /// ����ͼƬ���ظ�����
    /// </summary>
    public void BgScroll()
    {
        //ͼƬģʽ������Ϊrepeat��ͨ�������޸Ĳ����е�offset����ʵ�ֹ���
        //���������xֵ�����������yֵ
        render.material.mainTextureOffset += new Vector2(0, moveSpeed * Time.deltaTime);
    }
}
