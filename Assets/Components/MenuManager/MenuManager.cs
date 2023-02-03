using System;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Containers")]
    [SerializeField] public GameObject mainMenu;
    public static EventHandler<EventArgs> StartGame;
    public void CloseAllMenus(){
        mainMenu.SetActive(false);
    }
    public void Btn_StartGame(){
        CloseAllMenus();
        StartGame?.Invoke(this, EventArgs.Empty);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
