using UnityEngine;

public class FollowBird : MonoBehaviour
{
    private GameObject bird;
    private Transform birdTransform;

    void Start()
    {
        bird = GameObject.FindGameObjectWithTag("Player");
        birdTransform = bird.transform;
    }

    void Update()
    {
        if (GameMgr._intance.GameState == GameMgr.gameState_playing)
        {
            Vector3 birdPos = birdTransform.position;
            float y = birdPos.y - 3.5088f;
            if (y > 2.4f)
            {
                y = 2.4f;
            }
            if (y < -2.4f)
            {
                y = -2.4f;
            }
            this.transform.position = new Vector3(birdPos.x + 3.63223f, y, -10);
        }
    }
}
