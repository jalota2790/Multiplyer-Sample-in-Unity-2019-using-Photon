using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Player Movement Speed
    /// </summary>
    [Header("Movement")]
    public float movementSpeed;

    /// <summary>
    /// Vertical Rotation Speed
    /// </summary>
    public float verticalRotationSpeed;

    /// <summary>
    /// Horizontal Rotation Speed
    /// </summary>
    public float horizontalRotationSpeed;

    /// <summary>
    /// Vision
    /// </summary>
    public Transform vision;

    /// <summary>
    /// Rigidbody
    /// </summary>
    Rigidbody rb;

    /// <summary>
    /// Camera
    /// </summary>
    public GameObject _camera;

    /// <summary>
    /// Bullets
    /// </summary>
    [Space, Header("Shooting")]
    public GameObject bullet;

    /// <summary>
    /// Gun Trnsform
    /// </summary>
    public Transform gunTansform;

    [Space]
    bool isControlledLocally;
    public bool IsControlledLocally
    {
        set
        {
            isControlledLocally = value;
            _camera.SetActive(value);
        }
    }
    public bool isAlive = true;
    public bool isGameFinished;

    [Space]
    float health = 100;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            if (healthBar)
                healthBar.value = value;

            health = value;

        }
    }

    public Slider healthBar;
    PhotonView pv;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        Health = health;
    }

    void Update()
    {
        if (isControlledLocally && isAlive && !isGameFinished)
        {
            HandleMovement();
            HandleRotation();
            HandleShoot();
        }
    }

    void HandleMovement()
    {
        transform.Rotate(transform.up * Input.GetAxis("Mouse X") * horizontalRotationSpeed * Time.deltaTime);
        vision.Rotate(-Vector3.right * Input.GetAxis("Mouse Y") * verticalRotationSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        rb.MovePosition(transform.position + transform.forward * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
    }

    void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        pv.RPC("RPC_CreateBullet", RpcTarget.AllBuffered, gunTansform.position, gunTansform.transform.rotation);
    }

    public void OnGetDamage(float damage)
    {
        if (isAlive && isControlledLocally)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
            }
            else
            {
                Health = 0;
                Die();
            }
        }
    }


    void Die()
    {
        if (isControlledLocally)
        {
            print(gameObject.name + "died");
            pv.RPC("RPC_PlayerDied", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);
            isAlive = false;
        }
    }

    [PunRPC]
    public void RPC_CreateBullet(Vector3 position, Quaternion rotation)
    {
        Instantiate(bullet, position, rotation);
    }

    [PunRPC]
    public void RPC_PlayerDied(Player player)
    {
        print("RPC_PlayerDied received from " + player.NickName);
        isGameFinished = true;
        if (player != PhotonNetwork.LocalPlayer)
        {
            print("Local player won");

        }
        else
        {
            print("Local player lost");
        }
    }
}
