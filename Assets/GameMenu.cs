using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public static GameMenu _instance;
    public Text nowScore;
    public Text highScore;
    public Text playScore;
    public Image startTexture;
    public Image bgTexture;

    public void SetUIShow(bool isShow)
    {
        nowScore.gameObject.SetActive(isShow);
        highScore.gameObject.SetActive(isShow);
        bgTexture.gameObject.SetActive(isShow);
        startTexture.gameObject.SetActive(isShow);

    }

    void Awake()
    {
        _instance = this;
        SetUIShow(false);

    }
    private void Start()
    {
        EventDispatcher.Instance.RegisterEvent(EventNullType.EVENT_SCORE, ScoreUpdate);
    }

    private void ScoreUpdate(EventArgs e)
    {
        EventNull args = e as EventNull;
        //Debug.LogError(args.intValue);
        playScore.text = "当前:" + args.intValue.ToString();//接受值
    }

    public void UpdateScore(float nowScore)
    {
        float highScore = PlayerPrefs.GetFloat("score", 0);

        if (nowScore > highScore)
        {
            highScore = nowScore;
        }

        PlayerPrefs.SetFloat("score", highScore);

        this.nowScore.text ="当前:"+ nowScore.ToString();
        this.highScore.text = "历史最高:" + highScore.ToString();

        if (Input.GetMouseButtonDown(0) && GameManager._intance.GameState == GameManager.GAMESTATE_END)
        {
            Rect rect = new Rect(0, 0, Screen.width, Screen.height);
            Vector3 mousePos = Input.mousePosition;
            if (mousePos.x > rect.x &&
                mousePos.x < (rect.x + rect.width) &&
                mousePos.y > rect.y &&
                mousePos.y < (rect.y + rect.height))
            {
                //Application.LoadLevel(0);
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.UnregisterEvent(EventNullType.EVENT_SCORE, ScoreUpdate);
    }

}
