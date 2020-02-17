using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 move_velocity;
    private float losecontrol_countdown;
    public float maximum_speed;
    public float maximum_acceleration;
    private void Start()
    {
        maximum_acceleration = 0.5f;
        maximum_speed = 2;
        losecontrol_countdown = 0;
        move_velocity = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (losecontrol_countdown == 0)
        {
            Vector3 mouse_position = Input.mousePosition;
            mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);

            Vector2 direction = new Vector2(
                mouse_position.x - transform.position.x,
                mouse_position.y - transform.position.y
                );
            //Debug.Log(move_velocity.sqrMagnitude);
            if (move_velocity.sqrMagnitude < (maximum_speed*maximum_speed))
            {
                move_velocity += direction * maximum_acceleration;
                if (move_velocity.sqrMagnitude >= (maximum_speed * maximum_speed))
                {
                    //Debug.Log(move_velocity.normalized);
                    move_velocity = move_velocity.normalized*maximum_speed;
                    
                }
            }
            else
            {
                move_velocity += direction.normalized * maximum_acceleration;
                move_velocity = move_velocity.normalized * maximum_speed;
            }
        }
        else {
            losecontrol_countdown--;
            move_velocity = move_velocity * 0.9f;
        }
        rb.MovePosition(rb.position + move_velocity * Time.fixedDeltaTime);
        transform.up = move_velocity;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("food")) {
            if (maximum_speed >= 3)
            {
                maximum_speed+=0.15f;
            }
            else
            {
                maximum_speed = maximum_speed * 1.05f;
            }
            maximum_acceleration = maximum_speed / 4;
        }
        else if(other.gameObject.CompareTag("wall")){
            Vector2 detect_vector = new Vector2(
                other.gameObject.transform.position.x - transform.position.x,
                other.gameObject.transform.position.y - transform.position.y
                );
            float k = (detect_vector.x * move_velocity.x + detect_vector.y * move_velocity.y) / (move_velocity.sqrMagnitude);
            //Debug.Log(k);
            if (k <= 1)
            {
                move_velocity.x *= -1;
                move_velocity.y *= -1;
                if (move_velocity.sqrMagnitude >= 6)
                {
                    //Debug.Log(move_velocity.sqrMagnitude);
                    maximum_speed *= 0.9f;
                    //Debug.Log("123");
                    maximum_acceleration = maximum_speed / 4;
                }
                losecontrol_countdown = 20;

            }
            else {
                Destroy(other.gameObject);
            }
        }
    }
}
