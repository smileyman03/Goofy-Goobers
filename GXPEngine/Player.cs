using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

public class Player : Sprite
{
    private float maxVelocity = 15f;
    private float turnSpeed = 5f;
    private Vector2 velocity = new Vector2(0, 1f);
    private float angle = 270;
    private Vector2 fResult;
    private float mass = 1.5f;
    private Boolean lostControl = false;
    private float timer = 0;
    private Vector2 ropeAttachPoint;

    public Player() : base("spaceship.png")
    {
        SetOrigin(width / 2, height / 2);
        scale = 0.075f;
        x = game.width/2;
        y = game.height/2;
        ropeAttachPoint = new Vector2(game.width / 2, game.height - (height / 2));
    }
    public Vector2 AddForce(Vector2 vec)
    {
        return fResult += vec;
    }
    void Update()
    {
        RotateShip();
        UpdateVelocity();
        UpdatePosition();

        if (lostControl) LoseControl();
    }
    private void RotateShip()
    {
        if (!lostControl)
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
    }
    private void UpdateVelocity()
    {
        if (!lostControl)
        {
            if (Input.GetKey(Key.W))
            {
                //Set our angle:
                velocity.SetAngleDegrees(angle);

                //Set fResult
                if (fResult.Length() < maxVelocity)
                {
                    fResult += velocity;
                }
            }
        }
    }
    private void UpdatePosition()
    {
        //Drag:
        fResult *= .95f;

        //Update our position:
        x += fResult.x * Time.deltaTime / 16;
        y += fResult.y * Time.deltaTime / 16;
    }
    void OnCollision(GameObject other)
    {
        if (other is Asteroid)
        {
            Asteroid asteroid = (Asteroid)other;

            //Calculate CoM:
            Vector2 asteroidMomentum = asteroid.velocity.GetMomentum(asteroid.mass);
            Vector2 playerMomentum = velocity.GetMomentum(mass);
            Vector2 centerOfMass = (asteroidMomentum + playerMomentum) / (asteroid.mass + mass);

            //Apply bounce to asteroid
            asteroid.velocity = centerOfMass - 1 * (asteroid.velocity - centerOfMass);

            //Apply bounce to player
            fResult = centerOfMass - 1 * (fResult - centerOfMass);

            lostControl = true;
            timer = 0;
        }
    }
    void LoseControl()
    {
        timer += Time.deltaTime;
        if (timer >= 1000) lostControl = false;
    }
}