using System;
using UnityEngine;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Containers")]
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject homeMenu;
    [SerializeField] public GameObject characterSetupMenu;
    public static EventHandler<EventArgs> StartGame;
    public void CloseAllMenus(){
        mainMenu.SetActive(false);
    }
    public void Btn_StartGame(){
        CloseAllMenus();
        StartGame?.Invoke(this, EventArgs.Empty);
    }
}
