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
    private Vector2 dragVelocity;
    Boolean isMoving = false;
    public Player() : base("triangle.png")
    {
        SetOrigin(width/2, height/2);
        x = game.width/2;
        y = game.height/2;
    }

    void Update()
    {
        RotateShip();
        UpdatePosition();
    }

    void RotateShip()
    {
        //Rotate left if A is pressed
        if (Input.GetKey(Key.A))
        {
            velocity.RotateDegrees(-5);
            rotation -= turnSpeed;
        }

        //Rotate right if D is pressed
        if (Input.GetKey(Key.D))
        {
            velocity.RotateDegrees(5);
            rotation += turnSpeed;
        }
    }

    void UpdatePosition()
    {
        if (Input.GetKey(Key.W))
        {
            x += velocity.x;
            y += velocity.y;
            isMoving = true;
        }

        if (Input.GetKeyUp(Key.W))
        {
            isMoving = false;
            dragVelocity = velocity;
        }

        if (isMoving == false && dragVelocity.Length() <= 0.1f)
        {
            dragVelocity *= 0.9f;
            x += velocity.x;
            y += velocity.y;
        }
    }
}