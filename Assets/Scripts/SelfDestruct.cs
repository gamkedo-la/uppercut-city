using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Range(0.1f, 30f)] public float selfDestructTime;
    private void OnEnable() 
    {
        Destroy(gameObject, selfDestructTime);    
    }
}
