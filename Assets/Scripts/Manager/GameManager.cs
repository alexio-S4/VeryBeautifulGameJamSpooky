using UnityEngine;

public class GameManagerCOPYPauline : MonoBehaviour
{
    public static int totalInteractionsCount = 0;

    // Petite astuce pour réinitialiser le compteur au lancement si besoin
    private void Awake()
    {
        totalInteractionsCount = 0;
    }
}