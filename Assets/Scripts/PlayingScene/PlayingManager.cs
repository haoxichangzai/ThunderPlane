using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// ������
/// 1.���� ����ÿ�����˻����Ӧ����
/// 2.�Ѷȵȼ� ÿ����10������һ�� ����1��
/// </summary>

public class PlayingManager : MonoBehaviour
{
    //��Һ����Ѫ�������ǳ����еĶ���
    public Player player;
    public PlayerHP playerHP;

    //��ҷ���
    public int score { get; private set; }
    //����UI��ʾ
    public TextMeshProUGUI scoreUI;

    //�Ѷȵȼ�
    public int difficutLevel { get; private set; }
    //�Ѷȵȼ�UI��ʾ
    public TextMeshProUGUI diffLevelUI;

    //Boss�Ƿ����
    public bool isBossSpawn { get; private set; }
    //BossѪ��
    public BossHP bossHP;

    //��Ϸ����ʱBoss�Ƿ�����
    public bool isBossDead { get; private set; }
    //��Ϸ�Ƿ��Ѿ�����
    public int isGameOver { get; private set; }

    //�Ծ�ʱ��
    public float gameDuration { get; private set; }
    //�Ծ�ʱ��UI��ʾ
    public TextMeshProUGUI gameDurationUI;

    //��Դ���
    public AudioSource audioSource { get; private set; }
    //�������飬0Ϊ��ͨ��1ΪBossս
    public AudioClip[] audioClipList;

    //��һ����ȼ�
    public int firePowerLevel { get; private set; }
    //������ٵȼ�
    public int fireSpeedLevel { get; private set; }
    //��һ����ȼ�UI��ʾ
    public TextMeshProUGUI firePowerLevelUI;
    //������ٵȼ�UI��ʾ
    public TextMeshProUGUI fireSpeedLevelUI;

    private void Awake()
    {
        ResetState();
        //��ȡ��Դ���
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //��ȡ��ͨ����
        audioSource.clip = audioClipList[0];
        //����
        audioSource.Play();
        //ѭ��
        audioSource.loop = true;
    }

    //����״̬
    public void ResetState()
    {
        SetScore(0);//���ַ���Ϊ0
        SetDifficutLevel(1);//�����Ѷȵȼ�Ϊ1��
        isBossSpawn = false;//Boss��δ����
        isBossDead = false;//Boss��δ����
        bossHP.gameObject.SetActive(false);//BossѪ������ʾ
        isGameOver = 0;//��Ϸδ����
        gameDuration = 0f;

        diffLevelUI.text = this.difficutLevel.ToString();
        scoreUI.text = this.score.ToString();

        FindObjectOfType<PlayerHP>().ResetState();
        FindObjectOfType<Player>().ResetState();

        PlayerPrefs.SetInt("Score", this.score);
        PlayerPrefs.SetInt("Level", this.difficutLevel);
        PlayerPrefs.SetInt("IsBossDead", this.isBossDead ? 1 : 0);
        PlayerPrefs.SetInt("GameDuration", Mathf.FloorToInt(this.gameDuration));
    }

    private void FixedUpdate()
    {
        //����������󣬷���ÿ����10���Ѷ�����һ��
        if (score > 0 && score % 10 == 0)
        {
            DifficultRise();
        }
        //������Ϸʱ��
        if(isGameOver == 0)
        {
            GameDurationRise();
        }
    }

    //���÷���
    private void SetScore(int score)
    {
        this.score = score;
    }

    //��������
    public void ScoreRise(int score)
    {
        SetScore(this.score + score);
        scoreUI.text = this.score.ToString();
    }

    //�����Ѷȷ�Χ
    private void SetDifficutLevel(int level)
    {
        this.difficutLevel = level;
        diffLevelUI.text = this.difficutLevel.ToString();
    }

    //�Ѷȵȼ� + 1
    public void DifficutLevelRise()
    {
        SetDifficutLevel(this.difficutLevel + 1);
    }

    //ÿ����10�ܵл����Ѷ�����
    //����100�ܵл���Boss����
    public void DifficultRise()
    {
        FindObjectOfType<EnemySpawn>().SpawnFaster();
        //����100�����ϵл���Boss����
        if (score >= 100 && !isBossSpawn)
        {
            isBossSpawn = true;

            audioSource.Stop();//��ֹͣ����
            audioSource.clip = audioClipList[1];//�л�ΪBossս����
            audioSource.Play();//����

            FindObjectOfType<EnemySpawn>().BossSpawn();
        }
        //����һ�֣���Ȼ��ɱ����һ������ǰ���������������Ѷ����ӷ���
        SetScore(this.score + 1);
    }

    //�ȵ�Boss�ƶ���ָ���߶Ⱥ�����ʾBossѪ��
    public void SetBossHPActive()
    {
        bossHP.gameObject.SetActive(true);
    }

    //��Ϸ����������ΪBoss�Ƿ�����������������Ϸ��Ӯ
    public void GameOver(bool isBossDead)
    {
        //ֹͣ��������
        audioSource.Stop();
        //��ֹ�ظ�����GameOver����
        this.isGameOver += 1;
        if (isGameOver == 1)//ֻ�е�һ�ε�������
        {
            this.isBossDead = isBossDead;
            if (isBossDead)//Boss���������ʤ��
            {
                FindObjectOfType<Boss>().BossDestroy();//�ݻ�Boss����
            }
            else//Boss���������
            {
                player.PlayerDestroy();//�ݻ���Ҷ���
            }
            Invoke(nameof(LoadOverScene), 2.0f);
        }
    }

    public void LoadOverScene()
    {
        //�жϵ�ǰ�����Ƿ�����Ϸ����
        if (SceneManager.GetActiveScene().name == "Playing")
        {
            PlayerPrefs.SetInt("Score", this.score);
            PlayerPrefs.SetInt("Level", this.difficutLevel);
            PlayerPrefs.SetInt("IsBossDead", this.isBossDead ? 1 : 0);
            PlayerPrefs.SetInt("GameDuration", Mathf.FloorToInt(this.gameDuration));
            SceneManager.LoadScene("GameOver");
        }
    }

    public void GameDurationRise()
    {
        this.gameDuration += Time.deltaTime;
        gameDurationUI.text = Mathf.FloorToInt(this.gameDuration).ToString();
    }

    public void SetFPLUI()
    {
        this.firePowerLevel = FindObjectOfType<Player>().firePowerLevel;
        firePowerLevelUI.text = "Lv " + this.firePowerLevel.ToString();
    }

    public void SetFSLUI()
    {
        this.fireSpeedLevel = FindObjectOfType<Player>().fireSpeedLevel;
        fireSpeedLevelUI.text = "Lv " + this.fireSpeedLevel.ToString();
    }

}
