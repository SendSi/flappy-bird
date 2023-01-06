using UnityEngine;
public class PipeUpOrDown : MonoBehaviour
{
    public AudioSource hitMusic;
    public AudioSource dieMusic;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitMusic.Play();
            dieMusic.Play();
            GameManager._intance.GameState = GameManager.GAMESTATE_END;
        }
    }

}
