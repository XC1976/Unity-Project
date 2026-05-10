using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // SINGLETON : An object with a single instance that initiates itself, impossible to create other instance

    // To use a game manager from a public script : <nameSingleton>.Instance.<functionName>()
    // specify static makes it a single instance, accessible everywhere without any linking
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI textScore;   // affichage du score en cours

    // UI Game Over
    public TextMeshProUGUI textFinalScore;
    public GameObject PanelGameOver;
    
    // ========= Private ========= //
    private int _score = 0;
    private bool _isGameOver = false;
    
    // ========= Les fonctions ========= //

    // Start before the execution of the first frame
    private void Awake()
    {
        Instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PanelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Restart the scene (game) if the r key is pressed
        if(Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame) {
            // Unfreeze the scene, we froze it in the GameOver() function
            Time.timeScale = 1f;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Function
    public void AddScore()
    {
        _score++;
        textScore.text = "Score : " + _score;
    }

    public void GameOver() {

        // Ensures the game over can be triggered only once per scene since it switches to true
        // if the player loses
        if (_isGameOver)
        {
            return;
        }

        _isGameOver = true;

        // Freeze the game so the user is unable to move
        Time.timeScale = 0f;

        PanelGameOver.SetActive(true);
        textFinalScore.text = "Votre score est : " + _score;
    }
}