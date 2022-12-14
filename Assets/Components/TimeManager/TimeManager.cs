using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TimeProvider liveFightTime;
    public TimeProvider replayTime;
    public TimeProvider menuTime;
    private void FixedUpdate()
    {
        liveFightTime.fixedDeltaTime = Time.fixedDeltaTime * liveFightTime.timeScale;
        replayTime.fixedDeltaTime = Time.fixedDeltaTime * replayTime.timeScale;
        liveFightTime.time += Time.fixedDeltaTime * liveFightTime.timeScale;
        replayTime.time += Time.fixedDeltaTime * replayTime.timeScale;
    }
    void Update()
    {
        liveFightTime.deltaTime = Time.deltaTime * liveFightTime.timeScale;
        replayTime.deltaTime = Time.deltaTime * replayTime.timeScale;
    }
}
