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
    private float maxVelocity = 100f;
    private float turnSpeed = 5f;
    private Vector2 velocity = new Vector2(0, 1f);
    private float angle = 270;
    Vector2 fResult;


    public Player() : base("triangle.png")
    {
        SetOrigin(width/2, height/2);
        x = game.width/2;
        y = game.height/2;
    }

    void Update()
    {
        RotateShip();
        UpdateVelocity();
        UpdatePosition();
    }

    void RotateShip()
    {
        //Rotate left if A is pressed
        if (Input.GetKey(Key.A))
        {
            angle -= 5;
            rotation -= turnSpeed;
        }

        //Rotate right if D is pressed
        if (Input.GetKey(Key.D))
        {
            angle += 5;
            rotation += turnSpeed;
        }
    }

    void UpdateVelocity()
    {
        if (Input.GetKey(Key.W))
        {
            //Set our angle:
            velocity.SetAngleDegrees(angle);

            //Set fResult
            if (fResult.Length() < maxVelocity) fResult += velocity;
        }
    }

    void UpdatePosition()
    {
        //Drag:
        fResult *= .95f;

        //Update our position:
        x += fResult.x;
        y += fResult.y;
    }
}