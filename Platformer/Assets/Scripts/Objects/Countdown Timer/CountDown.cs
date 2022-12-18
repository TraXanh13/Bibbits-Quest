using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private float countDownTimer;
    [SerializeField] private Text timeRemaining;

    // Update is called once per frame
    void Update()
    {
        if(countDownTimer > 0){
            countDownTimer -= Time.deltaTime;
        }


        timeRemaining.text = countDownTimer.ToString("n2");
    }
}
