﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMonsterTouchEvent : MonoBehaviour
{
    [Header("WalkingSpeed"), Range(0, 20f)]
    public float Speed = 0.1f;
    private bool Walking = false;
    private bool TimerOn = false;
    private float Time_f = 0f;
    private Transform MonsterPossition;
    private Animator MonsterANI;
    private GameObject SkyGround;
    private void Awake()
    {
        MonsterANI = GetComponent<Animator>();
        SkyGround = GameObject.Find("SkyGround");
        MonsterPossition = transform;
    }
    //在場景開始時導入物件
    private void Update()
    {
        StartWalking();
        StopWalking();
        Timer();
        if (Time_f >= 1 )
        {
            SceneManager.LoadScene("BaseLevel");
            Debug.Log("Load Level");
        }
    }
    //持續執行 StartWalking, StopWalking, Timer
    private void StartWalking()
    {
        if (Walking == true)
        {
            MonsterPossition.transform.Translate(0, 0, Speed);
        }
    }
    //當Walking=true 怪物向前移動
    private void StopWalking()
    {
        if (Walking == false)
        {
            MonsterPossition.transform.Translate(0, 0, 0);
            MonsterANI.SetBool("Walk", false);
        }
    }
    //當Walking=false 怪物停止移動
    public void StartButtonPress()
    {
        SkyGround.SetActive(false);
        MonsterANI.SetBool("Walk", true);
        Walking = true;
        Debug.Log("Start Wlking");
    }
    //按下開始鈕時，天空地板消失，Monster開始Walk動畫
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name=="MonsterStop")
        {
            Walking = false;
            MonsterANI.SetTrigger("Attack");
            Time_f = 0f;
            TimerOn = true;
            Debug.Log("Timer On");
        }
    }
    //碰到MonsterStop時，停止走路，攻擊一次，Timer開始記時
    private void Timer()
    {
        if (TimerOn == true)
        {
            Time_f += Time.deltaTime;
            Debug.Log("Timer_f=" + Time_f);
        }
    }
    //記時器

}
