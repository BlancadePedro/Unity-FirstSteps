using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Capturar input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float moveY = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            moveY = -1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            moveY = 1;
        }

        // Mover usando Rigidbody (respeta física)
        Vector3 movement = new Vector3(moveX, moveY, moveZ) * speed;
        GetComponent<Rigidbody>().linearVelocity = movement;

        /*
        // Cambio de color
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rend.material.color = new Color(Random.value, Random.value, Random.value);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rend.material.color = Color.white;
        }*/
    }
}