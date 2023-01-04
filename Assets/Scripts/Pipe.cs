﻿using UnityEngine;
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
        // how to random a number
        float pos_y = Random.Range(-0.4f, -0.1f);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, pos_y, this.transform.localPosition.z);

    }

    void OnTriggerExit(Collider other)
    {//OnTriggerEnter OnTriggerStay OnTiggerExit
        if (other.tag == "Player")
        {
            // plus scroe
            aSource.Play();
            GameManager._intance.score++;

            //Debug.Log("---------"+ GameManager._intance.score);
            EventDispatcher.Instance.FireEvent(EventNullType.EVENT_SCORE, new EventNull { intValue = GameManager._intance.score });//发布事件，并传送值
        }
    }



}
