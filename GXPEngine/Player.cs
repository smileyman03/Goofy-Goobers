using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Player : Sprite
{
    private float turnSpeed = 3;
    private float moveSpeed = 5;
    public Player() : base("triangle.png")
    {
        SetOrigin(width/2, height/2);
        x = game.width/2;
        y = game.height/2;
    }

    void Update()
    {
        if (Input.GetKey(Key.A))
        {
            rotation -= turnSpeed;
        }
        if (Input.GetKey(Key.D))
        {
            rotation += turnSpeed;
        }
        // Check if the W and S keys are currently pressed.
        // If so, move the space ship relative to its current orientation:
        if (Input.GetKey(Key.W))
        {
            Move(0, -moveSpeed); // move forward (=up)
        }
    }
}