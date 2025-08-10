using Steamworks;
using UnityEngine;

public class SteamGameManager : MonoBehaviour
{

    private string TRAINING_STEAM_ID = "TRAINING";
    private string FIRST_BOSS_STEAM_ID = "FIRST_BOSS";

    public static SteamGameManager instance;
    private void Awake()
    {
        if (!SteamAPI.Init())
        {
            Debug.LogError("SteamAPI init failed!");
            return;
        }
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        SteamAPI.RunCallbacks();
    }

    private void OnDestroy()
    {
        SteamAPI.Shutdown();
    }

    public void UnlockAchievement(string id)
    {
        string achievementId = GetAchievement(id);
        if (!string.IsNullOrEmpty(achievementId))
        {
            SteamUserStats.SetAchievement(achievementId);
            SteamUserStats.StoreStats();
        }
    }

    private string GetAchievement(string id)
    {
        if (id != null)
        {
            switch (id)
            {
                case "end examen":
                    return TRAINING_STEAM_ID;
                case "Defeat The sorcerer":
                    return FIRST_BOSS_STEAM_ID;
                default:
                    return null;
            }
        }
        return null;
    }

    public void ClearAchievement(string id)
    {
        SteamUserStats.ClearAchievement(id);
        SteamUserStats.StoreStats();
    }

}
