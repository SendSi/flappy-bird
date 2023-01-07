using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour
{
    // define the state of the game
    public static int gameState_start = 0;//游戏开始
    public static int gameState_playing = 1;//游戏中 
    public static int gameState_end = 2;//游戏结束
    public static int gameState_menu = 3;//游戏菜单

    public Transform firstBg;
    public int score = 0;
    public int GameState = gameState_start;

    public static GameMgr _intance;

    private GameObject bird;

    void Awake()
    {
        _intance = this;
        bird = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameState == gameState_start)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameState = gameState_playing;
                EventCenter.GetInstance().Fire(EventName.EN_getLife);
            }
        }

        if (GameState == gameState_end)
        {
            GameState = gameState_menu;
            EventCenter.GetInstance().Fire(EventName.EN_gameOver);        
        }
    }
}
