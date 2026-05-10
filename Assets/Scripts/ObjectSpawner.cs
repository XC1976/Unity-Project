using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [Header("Prefabs")]

    public GameObject prefabCoin;
    public GameObject prefabObstacle;

    // Distance in front of the palyer where the coins will spawn
    public float distanceCreation = 50f;

    // Space between coins that are spawned
    public float spacingCoin = 8f;

    // Width  of the road that determines position of coins sideways

    public float widthCoin = 4f;

    private float _nextZ = 30f;

    // Next Z posiion of obstacles
    private float _nextObstacleZ = 60f; 

    // ========= Fetch players info ========= //

    private Transform _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_player == null) {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ========= Spawn coins ========== //

        if (_nextZ - _player.position.z < distanceCreation)
        {
            CreateCoin();
        }
        
        if (_nextObstacleZ - _player.position.z < distanceCreation)
        {
            CreateObstacle();
        }
    }

    void CreateCoin()
    {
        float newX = Random.Range(-widthCoin, widthCoin);

        Instantiate(prefabCoin, new Vector3(newX, 1.5f, _nextZ), Quaternion.Euler(90f, 0f, 0f));

        _nextZ += spacingCoin;
    }
    
    void CreateObstacle()
    {
        float newX = Random.Range(-widthCoin, widthCoin);

        Instantiate(prefabObstacle, new Vector3(newX, 0.24f, _nextObstacleZ), Quaternion.Euler(0f, 0f, 0f));

        _nextObstacleZ += spacingCoin;
    }
}
