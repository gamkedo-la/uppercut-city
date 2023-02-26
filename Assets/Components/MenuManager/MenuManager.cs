using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
public class MenuManager : MonoBehaviour
{
    [Header("Menu Systems")]
    [SerializeField] SO_MenuManagement sm_MainMenu;
    [Header("Menu Containers")]
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject homeMenu;
    [SerializeField] public GameObject characterSetupMenu;
    [SerializeField] public GameObject inGameHud;
    [SerializeField] public GameObject displayMessage;
    [Header("Home Menu Items")]
    [SerializeField] public GameObject btn_MatchSetup;
    [Header("Display Message Items")]
    [SerializeField] public TextMeshProUGUI displayMessageText;
    [Header("Character Setup Items")]
    [SerializeField] public GameObject btn_CharacterAccept;
    public static EventHandler<EventArgs> setupMatch;
    public static EventHandler<EventArgs> acceptCharacters;
    private void Awake() {
        PlayerController.newPlayerJoined += HandleNewPlayer;
        sm_MainMenu.currentlyActiveItem = btn_MatchSetup;
        SO_FighterStatus.onZeroHealth += HandleZeroHealth;
    }
    public void CloseAllMenus(){
        mainMenu.SetActive(false);
        homeMenu.SetActive(false);
        inGameHud.SetActive(false);
        displayMessage.SetActive(false);
        characterSetupMenu.SetActive(false);
    }
    public void Btn_Home(){
        CloseAllMenus();
        mainMenu.SetActive(true);
        homeMenu.SetActive(true);
        FocusControllersOnButton(btn_MatchSetup);
    }
    public void Btn_MatchSetup(){
        CloseAllMenus();
        mainMenu.SetActive(true);
        characterSetupMenu.SetActive(true);
        FocusControllersOnButton(btn_CharacterAccept);
        setupMatch?.Invoke(this, EventArgs.Empty);
        Debug.Log("Match setup invoked");
    }
    public void Btn_CharacterAccept()
    {
        CloseAllMenus();
        inGameHud.SetActive(true);
        // change player's allegiance flag
        // Set playercontroller to modify FighterInput SO
        // trigger state machine
        acceptCharacters?.Invoke(this, EventArgs.Empty);
        Debug.Log($"Accept Characters: {acceptCharacters}");
    }
    public void SetDefaultMenuFocus()
    {
        foreach (MultiplayerEventSystem es in FindObjectsOfType<MultiplayerEventSystem>())
        {
            es.firstSelectedGameObject = btn_MatchSetup;
            FocusControllersOnButton(btn_MatchSetup);
        }
    }
    public void FocusControllersOnButton(GameObject focus)
    {
        foreach (MultiplayerEventSystem es in FindObjectsOfType<MultiplayerEventSystem>())
        {
            es.SetSelectedGameObject(focus);
        }
    }
    private void HandleNewPlayer(object sender, System.EventArgs e){
        SetDefaultMenuFocus();
    }
    private void HandleZeroHealth(object sender, System.EventArgs e)
    {
        CloseAllMenus();
        displayMessageText.text = "knockout!!";
        inGameHud.SetActive(true);
        displayMessage.SetActive(true);
    }
}
