using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameCotroller : MonoBehaviour
{
    /// <summary>
    /// SpawnPosition
    /// </summary>
    public Transform[] spawnPosition;

    /// <summary>
    /// Health Bar
    /// </summary>
    public Slider healthBar;

    /// <summary>
    /// Win Panel
    /// </summary>
    public GameObject winPanel;

    /// <summary>
    /// Lost Panel
    /// </summary>
    public GameObject lostPanel;

    /// <summary>
    /// Game Instance
    /// </summary>
    public static GameCotroller Instance;


    void Start()
    {
        Instance = this;
        GameObject player = PhotonNetwork.Instantiate("Player", spawnPosition[Random.Range(0, spawnPosition.Length)].position, Quaternion.identity);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.healthBar = healthBar;
        playerController.IsControlledLocally = true;
    }

   public void GameWon()
    {
        winPanel.SetActive(true);
    }

    public void GameLost()
    {
        lostPanel.SetActive(true);
    }

    public void DisconnectAndRestart()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }
}
