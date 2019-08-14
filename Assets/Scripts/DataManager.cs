using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    /// <summary>
    /// Player Name
    /// </summary>
    public string playerName;

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
