using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Containers")]
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject homeMenu;
    [SerializeField] public GameObject characterSetupMenu;
    [Header("Home Menu Items")]
    [SerializeField] public GameObject btn_MatchSetup;
    [Header("Character Setup Items")]
    [SerializeField] public GameObject btn_FormatSetup;
    public static EventHandler<EventArgs> setupMatch;
    public void CloseAllMenus(){
        mainMenu.SetActive(false);
        characterSetupMenu.SetActive(false);
    }
    public void Btn_MatchSetup(){
        CloseAllMenus();
        characterSetupMenu.SetActive(true);
        FocusControllersOnButton(btn_MatchSetup);
        setupMatch?.Invoke(this, EventArgs.Empty);
    }
    public void FocusControllersOnButton(GameObject focus)
    {
        foreach (MultiplayerEventSystem es in FindObjectsOfType<MultiplayerEventSystem>())
        {
            es.firstSelectedGameObject = focus;
        }
    }
}
