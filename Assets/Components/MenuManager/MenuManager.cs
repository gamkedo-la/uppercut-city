using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
public class MenuManager : MonoBehaviour
{
    private GameObject defaultMenuFocus;
    [Header("Menu Containers")]
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject homeMenu;
    [SerializeField] public GameObject characterSetupMenu;
    [SerializeField] public GameObject inGameHud;
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] public GameObject displayMessage;
    [Header("Home Menu Items")]
    [SerializeField] public GameObject btn_MatchSetup;
    [Header("Display Message Items")]
    [SerializeField] public TextMeshProUGUI displayMessageText;
    [Header("Pause Menu Items")]
    [SerializeField] public GameObject btn_Resume;
    [Header("Character Setup Items")]
    [SerializeField] public GameObject btn_CharacterAccept;
    public static EventHandler setupMatch;
    public static EventHandler acceptCharacters;
    public static EventHandler resumeGame;
    private void Awake() {
        defaultMenuFocus = btn_MatchSetup;
        PlayerController.newPlayerJoined += HandleNewPlayer;
        SO_FighterStatus.onZeroHealth += HandleZeroHealth;
        Smb_MatchLive.onGamePaused += HandleGamePaused;
        Smb_MatchLive.onGameResume += HandleGameResume;
    }
    public void CloseAllMenus(){
        mainMenu.SetActive(false);
        homeMenu.SetActive(false);
        inGameHud.SetActive(false);
        pauseMenu.SetActive(false);
        displayMessage.SetActive(false);
        characterSetupMenu.SetActive(false);
    }
    public void Btn_Home(){
        CloseAllMenus();
        mainMenu.SetActive(true);
        homeMenu.SetActive(true);
        defaultMenuFocus = btn_MatchSetup;
        FocusControllersOnButton(btn_MatchSetup);
    }
    public void Btn_MatchSetup(){
        CloseAllMenus();
        mainMenu.SetActive(true);
        characterSetupMenu.SetActive(true);
        defaultMenuFocus = btn_CharacterAccept;
        FocusControllersOnButton(btn_CharacterAccept);
    }
    public void Btn_CharacterAccept()
    {
        CloseAllMenus();
        inGameHud.SetActive(true);
        // change player's allegiance flag
        // Set playercontroller to modify FighterInput SO
        acceptCharacters?.Invoke(this, EventArgs.Empty);
    }
    public void Btn_Resume(){
        CloseAllMenus();
        inGameHud.SetActive(true);
        FocusControllersOnButton(btn_Resume);
        resumeGame?.Invoke(this, EventArgs.Empty);
    }
    public void SetDefaultMenuFocus()
    {
        foreach (MultiplayerEventSystem es in FindObjectsOfType<MultiplayerEventSystem>())
        {
            es.firstSelectedGameObject = defaultMenuFocus;
        }
        FocusControllersOnButton(defaultMenuFocus);
    }
    public void FocusControllersOnButton(GameObject focus)
    {
        foreach (MultiplayerEventSystem es in FindObjectsOfType<MultiplayerEventSystem>())
        {
            es.firstSelectedGameObject = focus;
            es.SetSelectedGameObject(focus);
        }
    }
    private void HandleNewPlayer()
    {
        SetDefaultMenuFocus();
    }
    private void HandleGamePaused(object sender, System.EventArgs e)
    {
        CloseAllMenus();
        FocusControllersOnButton(btn_Resume);
        pauseMenu.SetActive(true);
    }
    private void HandleGameResume(object sender, System.EventArgs e)
    {
        CloseAllMenus();
        inGameHud.SetActive(true);
    }
    private void HandleZeroHealth(object sender, System.EventArgs e)
    {
        CloseAllMenus();
        displayMessageText.text = "knockout!!";
        inGameHud.SetActive(true);
        displayMessage.SetActive(true);
    }
}
