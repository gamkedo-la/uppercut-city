using UnityEngine;
using TMPro;

public class KnockdownCount : MonoBehaviour
{
    public SO_GameSession gameSession;
    public TextMeshProUGUI countText;
    private void FixedUpdate()
    {
        countText.text = gameSession.knockedDownCount.ToString();
    }
}
