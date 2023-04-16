using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
public class GameSystem : MonoBehaviour
{
    public delegate void GameSystemEvent();
    public SO_GameSession gameSession;
    public TimeProvider timeProvider;
    private Animator masterStateMachine;
    public SO_GameType gameType; // set a default, can be modified by menu
    
    private void Awake()
    {
        // make new game session SO_GameSession for temp objects
        masterStateMachine = GetComponent<Animator>();
        MenuManager.acceptCharacters += NewGameSession;
        MenuManager.rematch += Rematch;
        MenuManager.gameSessionEnd += EndGameSession;
        Smb_Gs_BeginNewMatch.onStateEnter += ResetRounds;
        Smb_Gs_BeginNewMatch.onStateEnter += ResetRoundTime;
        SO_FighterConfig.onFighterDown += FighterDown;
    }
    public void JoinNewPlayer(PlayerInput playerInput)
    {
        playerInput.uiInputModule = playerInput.GetComponent<InputSystemUIInputModule>();
        playerInput.uiInputModule.actionsAsset = playerInput.actions;
    }
    public void Rematch()
    {
        masterStateMachine.SetTrigger("Rematch");
    }
    public void NewGameSession()
    {
        masterStateMachine.SetBool("GameInSession", true);
    }
    public void FighterDown()
    {
        masterStateMachine.SetTrigger("Knockdown");
    }
    public void EndGameSession()
    {
        masterStateMachine.SetTrigger("EndGameSession");
        masterStateMachine.SetBool("FightersToCorner", false);
        masterStateMachine.SetBool("GameInSession", false);
        masterStateMachine.SetBool("MatchLive", false);
        masterStateMachine.SetBool("EndOfMatch", false);
        masterStateMachine.ResetTrigger("Rematch");
        masterStateMachine.ResetTrigger("Knockdown");
    }
    public void ResetRounds()
    {
        gameSession.currentRound = 0;
        gameSession.totalRounds = gameType.numberOfRounds;
    }
    public void ResetRoundTime()
    {
        gameSession.roundTime = gameType.roundTime;
        gameSession.restTime = gameType.restTime;
    }
}
