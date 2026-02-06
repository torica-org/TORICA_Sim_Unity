using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Landing : MonoBehaviour
{
    private GameObject Result;
    private GameObject SimpleResult;
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        Result = GameObject.Find("Result");
        SimpleResult = GameObject.Find("SimpleResult");
        canvas.enabled = false;

        Result.SetActive(false);
        SimpleResult.SetActive(false);
        MyGameManeger.instance.Landing = false;
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
        Result.SetActive(!MyGameManeger.instance.SettingActive & MyGameManeger.instance.Landing);
        SimpleResult.SetActive(!MyGameManeger.instance.SettingActive & MyGameManeger.instance.Landing);
    }
}
