using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    //ʤ����ʧ��UI
    public TextMeshProUGUI[] WinOrLose;

    //Boss�Ƿ�����
    public bool isBossDead { get; private set; }

    //�Ѷȵȼ�UI
    public TextMeshProUGUI levelText;
    //�Ѷȵȼ�
    public int level { get; private set; }

    //����UI
    public TextMeshProUGUI scoreText;
    //����
    public int score { get; private set; }

    //��Ϸʱ��UI
    public TextMeshProUGUI gameDurationText;
    //��Ϸʱ��
    public int gameDuration { get; private set; }

    //��Դ���
    public AudioSource audioSource;
    //�������飬0Ϊʤ����1Ϊʧ��
    public AudioClip[] audioClipList;

    //�����ʼ��ϷUI��ʾ
    public TextMeshProUGUI playAgain;
    //��˸���
    public float blinkCold { get; private set; }
    //��˸��ʱ
    public float blinkColdCount { get; private set; }

    private void Start()
    {
        //��ȡ��Դ���
        audioSource = this.gameObject.GetComponent<AudioSource>();

        //��PlayerPrefs�õ���Ϸ����
        this.score = PlayerPrefs.GetInt("Score");
        this.level = PlayerPrefs.GetInt("Level");
        this.isBossDead = PlayerPrefs.GetInt("IsBossDead") == 1 ? true : false;
        this.gameDuration = PlayerPrefs.GetInt("GameDuration");
        
        MissionState();
        SetText();

        //����
        audioSource.Play();
        //ѭ��
        audioSource.loop = true;

        blinkCold = 0.2f;
        blinkColdCount = 0f;
    }

    private void FixedUpdate()
    {
        LoadGameScene();

        TimeCount();
        TextBlink();
    }

    //��ʾʤ������ʧ��
    public void MissionState()
    {
        if (isBossDead)
        {
            WinOrLose[0].gameObject.SetActive(false);
            audioSource.clip = audioClipList[0];
        }
        else {
            WinOrLose[1].gameObject.SetActive(false);
            audioSource.clip = audioClipList[1];
        }
    }

    //��ʾ��Ϸ����UI
    public void SetText()
    {
        levelText.text = "Level: " + level.ToString();
        scoreText.text = "Score: " + score.ToString();
        gameDurationText.text = "Time: " + gameDuration.ToString() + " s";
    }

    //������Ϸ����
    public void LoadGameScene()
    {
        if (Input.anyKeyDown)
        {
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("Playing");
            }
        }
    }

    public void TextBlink()
    {
        if (blinkColdCount >= blinkCold)
        {
            playAgain.alpha = 255;
        }
        else
        {
            playAgain.alpha = 0;
        }
    }

    public void TimeCount()
    {
        blinkColdCount += Time.deltaTime;
        if (Mathf.FloorToInt(blinkColdCount) == 1)
        {
            blinkColdCount = 0f;
        }
    }
}
