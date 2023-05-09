using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

public class Player : Sprite
{
    private float turnSpeed = 5f;
    private Vector2 velocity = new Vector2(0, -10);
    public Player() : base("triangle.png")
    {
        SetOrigin(width/2, height/2);
        x = game.width/2;
        y = game.height/2;
    }

    void Update()
    {
        Console.WriteLine("velocity: " + velocity.x + " " + velocity.y);
        if (Input.GetKey(Key.A))
        {
            velocity.RotateDegrees(-5);
            rotation -= turnSpeed;
        }
        if (Input.GetKey(Key.D))
        {
            velocity.RotateDegrees(5);
            rotation += turnSpeed;
        }
        // Check if the W and S keys are currently pressed.
        // If so, move the space ship relative to its current orientation:
        if (Input.GetKey(Key.W))
        {
            x += velocity.x;
            y += velocity.y;
        }
    }
}