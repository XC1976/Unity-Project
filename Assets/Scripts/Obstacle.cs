using UnityEngine;

public class Obstacle : MonoBehaviour
{

    // Variable death sound
    public AudioClip deathSound;
    
    private Transform _player;
    private float _destroyBehind = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < _player.position.z - _destroyBehind) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision avec : " + other.gameObject.name + " | Tag : " + other.tag);

        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Play the death sound
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }

        GameManager.Instance.GameOver();
    }
}