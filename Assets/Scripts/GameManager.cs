using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // define the state of the game
    public static int GAMESTATE_MENU = 0;//游戏菜单状态
    public static int GAMESTATE_PLAYING = 1;//游戏中状态 ... 
    public static int GAMESTATE_END = 2;//游戏结束状态


    public Transform firstBg;
    public int score = 0;
    public int GameState = GAMESTATE_MENU;

    public static GameManager _intance;

    private GameObject bird;

    void Awake()
    {
        _intance = this;
        bird = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameState == GameManager.GAMESTATE_MENU)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // transform state
                GameState = GAMESTATE_PLAYING;        

                EventCenter.GetInstance().Fire(EventName.EN_getLife);
            }
        }

        if (GameState == GameManager.GAMESTATE_END)
        {
            EventCenter.GetInstance().Fire(EventName.EN_gameOver);
        }
    }
}
