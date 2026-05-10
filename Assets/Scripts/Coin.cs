using UnityEngine;

public class Piece : MonoBehaviour
{
    // Variable that stores the mp3 coin sound
    public AudioClip coinSound;
    
    public float vitesseRotation = 150f; // Vitesse de rotation en degrés par seconde
    public float destroyBehindBy = 5f;

    private GameObject _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    // Update is called once per frame
    void Update() {
        // tourner la piece sur elle meme , on utilise la fonction Rotate de transform

        transform.Rotate(0f, vitesseRotation * Time.deltaTime, 0f, Space.World);

        // Destroy the object if it's behind the player > 5
        if (_player != null && transform.position.z < _player.transform.position.z - destroyBehindBy)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // Play the coin sound
        if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        // Add score
        GameManager.Instance.AddScore();

        // Destroy the object
        Destroy(gameObject);
    }
}
