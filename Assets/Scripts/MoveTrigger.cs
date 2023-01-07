using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    public Transform currentBg;
    public Pipe pipe1;
    public Pipe pipe2;

    public void OnTriggerEnter(Collider other)
    {   
        if (other.tag == "Player")
        {           
            Transform firstBg = GameMgr._intance.firstBg;
            // 2. move
            currentBg.position = new Vector3(firstBg.position.x + 12, currentBg.position.y, currentBg.position.z);

            GameMgr._intance.firstBg = currentBg;     

            pipe1.RandomGeneratePosition();
            pipe2.RandomGeneratePosition();
        }
    }

}
