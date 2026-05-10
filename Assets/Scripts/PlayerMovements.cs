using UnityEngine;
using UnityEngine.InputSystem;

// Our character goes forward automatically and moves left and right with Q and D


[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Vitesse")]
    public float speedForward;
    public float acceleration;
    public float maxSpeed;
    public float speedSideways = 10f;

    //
    private bool _maxSpeed = false;

    [Header("Road")]

    // Limite de la route, le personnage ne peut pas sortir de cette limite de la route
    public float limitX = 5f;

    // Keyboard input (-1 left | 0 nothing | +1 right)
    private float _inputX = 0f;

    // Our character controller
    private CharacterController _cc;

    // Function move the player 
    void MovePlayer()
    {

        // Calculate lateral movement of the player (calculate distance that on the left or the right)
        float movementX = speedSideways * Time.deltaTime * _inputX;

        // Calculate new X
        float newX = transform.position.x + movementX;

        // Limit x to not fall from the map
        float clampedX = Mathf.Clamp(newX, -limitX, limitX);

        // New position
        float deltaX = clampedX - transform.position.x;

        // Move the player
        _cc.Move(new Vector3(deltaX, 0f, speedForward * Time.deltaTime));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        Debug.Log(Random.Range(-5f, 5f));
    }

    // Update is called once per frame
    void Update()
    {

        // Get player input

        var kb = Keyboard.current;

        //  Logic to put -1 if left key is pressed, 1 if the right key is pressed
        // if(kb != null) {
        //     _inputX = (kb.dKey.isPressed || kb.rightArrowKey.isPressed ? 1f : 0f)
        //     - (kb.qKey.isPressed || kb.leftArrowKey.isPressed ? 1f : 0f);
        // }

        // More optimized logic
        if (kb != null)
        {
            // If "d" key or "left arrow" is pressed
            if (kb.dKey.isPressed || kb.rightArrowKey.isPressed)
            {
                _inputX = 1f;
            }
            else if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)
            {
                _inputX = -1f;
            }
            else
            {
                _inputX = 0f;
            }
        }

        // Mouvement du joueur
        MovePlayer();
    }

    void FixedUpdate() {
        // _maxSpeed is used as a kill switch to avoid testing for no reason if the max speed has already been reached
        if (_maxSpeed == false && speedForward <= maxSpeed)
        {
            speedForward += acceleration;
        }

        // Detect if the max speed has been reached
        // _maxSpeed = true makes the test above less ressource intensive
        if(speedForward >= maxSpeed) {
            speedForward = maxSpeed;
            _maxSpeed = true;
        }
    }
}