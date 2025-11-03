using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class prohibitedArea : MonoBehaviour
{
    private GameObject Result;
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        Result = GameObject.Find("Result");
    }

    void OnCollisionEnter(Collision collision)
    {
        MyGameManeger.instance.Landing = true;
        MyGameManeger.instance.SettingMode = 0;
        canvas.enabled = true;
        Time.timeScale=(float)Convert.ToInt32(!MyGameManeger.instance.SettingActive & !MyGameManeger.instance.Landing);
    }

    // Update is called once per frame
    void Update()
    {
        //Result.SetActive(/*!MyGameManeger.instance.SettingActive & */MyGameManeger.instance.Landing);
    }
}
