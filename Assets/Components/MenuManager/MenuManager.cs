using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.UI;
public class MenuManager : MonoBehaviour
{
    private GameObject defaultMenuFocus;
    [Header("Menu Containers")]
    public GameObject mainMenu;
    public GameObject homeMenu;
    public GameObject characterSetupMenu;
    public GameObject inGameHud;
    public GameObject matchEnd;
    public GameObject pauseMenu;
    public GameObject displayMessage;
    [Header("Home Menu Items")]
    public GameObject btn_MatchSetup;
    [Header("Matc End Items")]
    public GameObject btn_Rematch;
    [Header("Display Message Items")]
    public GameObject roundBeginPanel;
    public TextMeshProUGUI displayMessageText;
    [Header("Pause Menu Items")]
    public GameObject btn_Resume;
    [Header("Character Setup Items")]
    public GameObject btn_CharacterAccept;
    public static EventHandler setupMatch;
    public static GameSystem.GameSystemEvent acceptCharacters;
    public static Smb_MatchLive.MatchLiveUpdate resumeGame;
    public static GameSystem.GameSystemEvent beginNewGameSession;
    private void Awake() {
        defaultMenuFocus = btn_MatchSetup;
        PlayerController.newPlayerJoined += HandleNewPlayer;
        StateFightersToCorners.onStateEnter += HandleBetweenRoundEnter;
        Smb_MatchLive.onGamePaused += HandleGamePaused;
        Smb_MatchLive.onGameResume += HandleGameResume;
        Smb_MatchLive.onStateEnter += HandleGameResume;
        Smb_Gs_EndOfMatch.onStateMachineEnter += EndOfMatchMenu;
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
    public void Btn_MatchSetup()
    {
        CloseAllMenus();
        mainMenu.SetActive(true);
        characterSetupMenu.SetActive(true);
        defaultMenuFocus = btn_CharacterAccept;
        FocusControllersOnButton(btn_CharacterAccept);
    }
    public void Btn_CharacterAccept()
    {
        Debug.Log($"MenuManager: Btn_CharacterAccept");
        CloseAllMenus();
        inGameHud.SetActive(true);
        // change player's allegiance flag
        // Set playercontroller to modify FighterInput SO
        acceptCharacters?.Invoke();
        beginNewGameSession?.Invoke();
    }
    public void Btn_Resume(){
        CloseAllMenus();
        inGameHud.SetActive(true);
        FocusControllersOnButton(btn_Resume);
        resumeGame?.Invoke();
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
    private void HandleBetweenRoundEnter()
    {
        CloseAllMenus();
        inGameHud.SetActive(true);
        // turn messages panel on
        displayMessage.SetActive(true);
        roundBeginPanel.SetActive(true);
        // show message: Round #, countdown time
    }
    private void EndOfMatchMenu()
    {
        CloseAllMenus();
        mainMenu.SetActive(true);
        matchEnd.SetActive(true);
        defaultMenuFocus = btn_Rematch;
        FocusControllersOnButton(btn_Rematch);
    }
    private void HandleGamePaused()
    {
        CloseAllMenus();
        FocusControllersOnButton(btn_Resume);
        pauseMenu.SetActive(true);
    }
    private void HandleGameResume()
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
