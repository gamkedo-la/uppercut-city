using System;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    public static EventHandler<EventArgs> StartGame;
    public void Btn_StartGame(){
        StartGame?.Invoke(this, EventArgs.Empty);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
