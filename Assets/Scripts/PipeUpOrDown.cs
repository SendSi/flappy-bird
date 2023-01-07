using UnityEngine;
public class PipeUpOrDown : MonoBehaviour
{
    public AudioSource hitMusic;
    public AudioSource dieMusic;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && GameMgr._intance.GameState == GameMgr.gameState_playing)
        {
            if (hitMusic != null)
            {
                hitMusic.Play();
                dieMusic.Play();
            }
            GameMgr._intance.GameState = GameMgr.gameState_end;
        }
    }

}
