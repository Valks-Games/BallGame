using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1000f;

    private float adjSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        adjSpeed = speed * 1000f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float moveHorz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorz * adjSpeed * Time.deltaTime, moveVert * adjSpeed * Time.deltaTime);

        rb.AddForce(movement);
    }
}
