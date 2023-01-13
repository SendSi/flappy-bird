using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour
{

    AudioSource aSource;
    void Start()
    {
        RandomGeneratePosition();
        aSource = this.GetComponent<AudioSource>();
    }

    public void RandomGeneratePosition()
    {
        float pos_y = Random.Range(-0.4f, -0.1f);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, pos_y, this.transform.localPosition.z);
    }

    void OnTriggerExit(Collider other)
    {//OnTriggerEnter OnTriggerStay OnTiggerExit
        if (other.tag == "Player")
        {
            // plus scroe
            aSource.Play();
            GameMgr._intance.score++;
            EventCenter.GetInstance().Fire(EventName.EN_updateScore, GameMgr._intance.score);      
        }
    }



}
