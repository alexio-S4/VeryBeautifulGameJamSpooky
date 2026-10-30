using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int totalInteractionsCount = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void ResetStatics()
    {
        totalInteractionsCount = 0;
    }

    private void Awake()
    {
        totalInteractionsCount = 0;
    }
}