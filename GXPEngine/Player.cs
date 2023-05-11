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

public class Player : AnimationSprite
{
    private float maxVelocity = 15f;
    private float turnSpeed = 2.5f;
    private Vector2 velocity = new Vector2(0, 0.75f);
    private float angle = 270;
    private Vector2 fResult;
    private float mass = 1.25f;
    private Boolean lostControl = false;
    private float timer = 0;
    private float animationTimer = 0;
    public Vector2 ropeAttachPoint;
    private float fuelCount = 20000f;
    private float fuelConsumptionRate = 1f;
    public Player() : base("spaceship.png", 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        scale = 0.1f;
        x = game.width/2;
        y = game.height/2;

        // Rope attach point:
        ropeAttachPoint = new Vector2(x, y + (height / 2));
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
        AnimateShip();
        FuelConsumption();

        if (lostControl) LoseControl();
    }
    private void RotateShip()
    {
        if (!lostControl)
        {
            //Rotate left if A is pressed
            if (Input.GetKey(Key.A) && fuelCount > 0)
            {
                angle -= 2.5f;
                rotation -= turnSpeed;
                ropeAttachPoint.RotateAroundDegrees(new Vector2(x, y), -2.5f);
            }

            //Rotate right if D is pressed
            if (Input.GetKey(Key.D) && fuelCount > 0)
            {
                angle += 2.5f;
                rotation += turnSpeed;
                ropeAttachPoint.RotateAroundDegrees(new Vector2(x, y), 2.5f);
            }
        }
    }
    private void UpdateVelocity()
    {
        if (!lostControl)
        {
            //Set our angle:
            velocity.SetAngleDegrees(angle);

            if (Input.GetKey(Key.W) && fuelCount > 0)
            {
                //Set fResult
                if (fResult.Length() < maxVelocity)
                {
                    fResult += velocity;
                }
            }

            if (Input.GetKey(Key.SPACE) && fuelCount > 0)
            {
                fResult += velocity * 1.5f;
                fuelConsumptionRate = 2.5f;
            }
        }

        if (!Input.GetKey(Key.SPACE) || lostControl)
        {
            fuelConsumptionRate = 1f;
        }
    }
    private void UpdatePosition()
    {
        //Drag:
        fResult *= .975f;

        //Update our position:
        x += fResult.x * Time.deltaTime / 16f;
        y += fResult.y * Time.deltaTime / 16f;

        // Update rope position:
        ropeAttachPoint.x += fResult.x * Time.deltaTime / 16f;
        ropeAttachPoint.y += fResult.y * Time.deltaTime / 16f;
    }
    void OnCollision(GameObject other)
    {
        if (other is Asteroid)
        {
            Asteroid asteroid = (Asteroid)other;

            //Calculate CoM:
            Vector2 asteroidMomentum = asteroid.velocity.GetMomentum(asteroid.mass);
            Vector2 playerMomentum = fResult.GetMomentum(mass);
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
    void AnimateShip()
    {
        if ((Input.GetKey(Key.W) || Input.GetKey(Key.A) || Input.GetKey(Key.D) || Input.GetKey(Key.SPACE)) && fuelCount > 0)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= 200)
            {
                NextFrame();
                animationTimer = 0;
            }
        }
    }
    void FuelConsumption()
    {
        if ((Input.GetKey(Key.W) || Input.GetKey(Key.A) || Input.GetKey(Key.D) || Input.GetKey(Key.SPACE)) && fuelCount > 0 && !lostControl)
        {
            fuelCount -= Time.deltaTime * fuelConsumptionRate;
            Console.WriteLine("Fuel: " + fuelCount);
        }
    }
}