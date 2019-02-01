using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UFrame.Executor;

public class TestExecutor : MonoBehaviour 
{

    
    //SerialExecutor executors = new SerialExecutor();
    ParallelExecutor executors = new ParallelExecutor();
    public Button btnPlay;
    public Button btnPause;
    public Button btnStop;

	// Use this for initialization
	void Start () 
    {
        btnPlay = transform.Find("Button_Play").GetComponent<Button>();
        btnPause = transform.Find("Button_Pause").GetComponent<Button>();
        btnStop = transform.Find("Button_Stop").GetComponent<Button>();

        btnPlay.onClick.AddListener(OnBtnPlay);
        btnPause.onClick.AddListener(OnBtnPause);
        btnStop.onClick.AddListener(OnBtnStop);



        ExecutorDuration exetest1 = new ExecutorDuration(1000);
        executors.AddExecutor(exetest1);
        ExecutorDuration exetest2 = new ExecutorDuration(2000);
        executors.AddExecutor(exetest2);
        ExecutorDuration exetest3 = new ExecutorDuration(3000);
        executors.AddExecutor(exetest3);
	}
	
	// Update is called once per frame
	void Update () {
        executors.ControlTick((long)(Time.deltaTime * 1000));

	}

    void OnBtnPlay()
    {
        executors.Play();
        
    }

    void OnBtnPause()
    {
        executors.Pause();
        
    }

    void OnBtnStop()
    {
        executors.Stop();
        
    }

    
}
