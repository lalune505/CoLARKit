using UnityEngine;
using System.Collections;


public class MyLog : MonoBehaviour
{
    string myLog;

    public UnityEngine.UI.Text text;
    Queue myLogQueue = new Queue();

    void Start()
    {
        /*
        Debug.Log("Log1");
        Debug.Log("Log2");
        Debug.Log("Log3");
        Debug.Log("Log4");
        */
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void Update()
    {
        //StartCoroutine(TimerErase());
    }

    //private IEnumerator TimerErase()
    //{
    //    yield return new WaitForSeconds(5);
    //    myLog = "";
    //}

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    void OnGUI()
    {
        text.text = myLog;
    }
}
