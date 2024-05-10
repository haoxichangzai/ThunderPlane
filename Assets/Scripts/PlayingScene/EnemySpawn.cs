using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// 1.������������겢���ɵ���
/// 2.��������
/// 3.����Boss
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    //����Ԥ����
    public GameObject Enemy;
    //���˳�����ȴ
    public float spawnCold { get; private set; }
    //���˳�����ȴ��ʱ��
    public float spawnColdCount { get; private set; }

    //�����������ٶ�
    public float maxSpawnCold { get; private set; }
    //�Ѷ�����ʱ�����ٶ�����
    public float SpawnColdRise { get; private set; }

    //�洢��ͼ��СXֵ
    public float mapMinX { get; private set; }
    //�洢��ͼ���Xֵ
    public float mapMaxX { get; private set; }

    //BossԤ����
    public GameObject Boss;

    //�����������
    private Vector3 randomX;

    private void Awake()
    {
        //���˳�����ȴʱ��
        spawnCold = 2.0f;
        spawnColdCount = 2.0f;
        maxSpawnCold = 1.0f;
        SpawnColdRise = 0.2f;
        //�õ���ͼ����������Сֵ
        mapMinX = FindObjectOfType<Utils>().getMapMinX();
        mapMaxX = FindObjectOfType<Utils>().getMapMaxX();
    }

    private void FixedUpdate()
    {
        EnemySpawnCold();
    }

    //������ȴ��
    private void EnemySpawnCold()
    {
        if (spawnColdCount >= spawnCold)
        {
            Instantiate(Enemy, new Vector3(GetRandomX().x, transform.position.y, transform.position.z),transform.rotation);
            spawnColdCount = 0;
        }
        else
        {
            spawnColdCount += Time.deltaTime;
        }
    }

    //���һ������ĺ�������
    private Vector3 GetRandomX()
    {
        randomX.x = Random.Range(mapMinX + 0.3f, mapMaxX - 0.3f);
        return randomX;
    }

    //�����ٶȼӿ죬���ΪmaxSpawnCold
    public void SpawnFaster()
    {
        if (spawnCold > maxSpawnCold)
        {
            spawnCold -= SpawnColdRise;
            FindObjectOfType<PlayingManager>().DifficutLevelRise();
        }
    }

    //Boss����
    public void BossSpawn()
    {
        Instantiate(Boss, transform.position, transform.rotation);
    }
}
