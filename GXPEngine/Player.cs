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
    private Vector2 velocity = new Vector2(0, 0.5f);
    private float angle = 270;
    private Vector2 fResult;
    private float mass = 1.5f;
    private Boolean lostControl = false;
    private Boolean isBoosting = false;
    private float timer = 0;
    public Vector2 ropeAttachPoint;
    private float fuelCount = 30000f;
    private float fuelConsumptionRate = 1f;
    private float boostTimer = 0;
    private float health = 100;
    private float shieldTimer = 0;
    private Boolean hasShield = false;
    private float animationTimer = 0;
    private float collisionCooldownTimer = 0;
    ShipShield shield;
    private SoundChannel spaceshipSound = new Sound("spaceshipSounds.wav").Play();
    private SoundChannel shieldSound;
    public Player(float sX, float sY) : base("spaceship.png", 4, 1)
    {
        SetOrigin(width / 2, height / 2);
        scale = 0.1f;
        x = game.width/2;
        y = game.height/2;
        SetXY(sX, sY);
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
        FuelConsumption();
        Timers();
        DoSounds();
        UpdateHUD();

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
        if (!lostControl && Input.GetKey(Key.W) && fuelCount > 0)
        {
            //Set our angle:
            velocity.SetAngleDegrees(angle);

            //Set fResult
            if (fResult.Length() < maxVelocity)
            {
                fResult += velocity;
            }

            if (isBoosting)
            {
                fResult += velocity;
            }
        }


        //out of bounce fix
        Vector2 outofbounce = new Vector2(0, 0);
        MyGame myGame = (MyGame)game;
        if (x < 0)
        {
            outofbounce.x += 1;
            
        }else if(x > myGame.width){
            outofbounce.x -= 1;
        }
        if(y < 0)
        {
            outofbounce.y += 1;
        }else if(y > myGame.height)
        {
            outofbounce.y -= 1;
        }
        fResult += outofbounce;


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

            if (collisionCooldownTimer <= 0)
            {
                collisionCooldownTimer = 500;
                lostControl = true;
                timer = 0;

                //damage:
                if (!hasShield)
                {
                    health -= 25;

                    //lose on 0 hp:
                    if (health <= 0)
                    {
                        death();
                        
                    }
                }
                else
                {
                    hasShield = false;
                    shield.LateDestroy();
                    shieldSound.Stop();
                    shieldSound = new Sound("Shield_Down.wav").Play();
                }
            }
            SoundChannel playerHit = new Sound("Player_Hit.wav").Play();
        }

        if (other is Planet)
        {
            
            Planet planet = (Planet)other;
            Vector2 main;
            Vector2 pos;
            float angleX;
            main.x = x;
            main.y = y;

            pos.x = planet.x;
            pos.y = planet.y;

            pos = pos - main;
            
            if(pos.Length() < planet.width / 2 + width / 2)
            {
                main = -fResult;
                if (pos.GetAngleDegrees() > main.GetAngleDegrees())
                {
                    angleX = pos.GetAngleDegrees() - main.GetAngleDegrees();
                    main.SetAngleDegrees(main.GetAngleDegrees() + angleX * 2);
                }
                else
                {
                    angleX = pos.GetAngleDegrees() - pos.GetAngleDegrees();
                    main.SetAngleDegrees(main.GetAngleDegrees() - angleX * 2);
                }

                fResult = main;

                if (collisionCooldownTimer <= 0)
                {
                    collisionCooldownTimer = 500;
                    lostControl = true;
                    timer = 0;

                    //damage:
                    if (!hasShield)
                    {
                        health -= 25;

                        //lose on 0 hp:
                        if (health <= 0)
                        {
                            death();
                        }
                    }
                    else
                    {
                        hasShield = false;
                        shield.LateDestroy();
                        shieldSound.Stop();
                        shieldSound = new Sound("Shield_Down.wav").Play();
                    }
                }
                SoundChannel playerHit = new Sound("Player_Hit.wav").Play();
            }
        }

        if (other is Gravity)
        {
            Gravity pull = (Gravity)other;
            Vector2 center;
            Vector2 ship;

            ship.x = x;
            ship.y = y;

            center.x = pull.x;
            center.y = pull.y;

            

            if (ship.DistanceTo(center) < pull.width / 2 + width / 2)
            {
                //Console.WriteLine("collide");
                fResult += (center - ship).Normalized() * 0.1f *pull.pullStrength;
              
            }
            
        }

        // pickups:
        if (other is BoostPickup)
        {
            isBoosting = true;
            fuelConsumptionRate = 2f;
            boostTimer = 2000f;

            //Delete pickup:
            other.LateDestroy();

            //Sound:
            SoundChannel boostSound = new Sound("Boost.wav").Play();
        }

        if (other is RepairPickup)
        {
            health += 25;
            if (health > 100) health = 100;

            //Delete pickup:
            other.LateDestroy();

            //Sound:
            SoundChannel repairSound = new Sound("Repair.wav").Play();
        }

        if (other is ShieldPickup)
        {
            hasShield = true;
            shieldTimer = 5000;

            shield = new ShipShield();
            LateAddChild(shield);

            //Delete pickup:
            other.LateDestroy();

            //Sound:
            shieldSound = new Sound("Shield.wav").Play();
        }

        if (other is FuelPickup)
        {
            fuelCount += 15000;
            if (fuelCount > 30000) fuelCount = 30000;

            //Delete pickup:
            other.LateDestroy();
        }
    }
    private void LoseControl()
    {
        timer += Time.deltaTime;
        if (timer >= 1000) lostControl = false;
    }
    private void FuelConsumption()
    {
        if ((Input.GetKey(Key.W) || Input.GetKey(Key.A) || Input.GetKey(Key.D)) && fuelCount > 0 && !lostControl)
        {
            fuelCount -= Time.deltaTime * fuelConsumptionRate;
            //Console.WriteLine("fuel: " + fuelCount);
        }

        // lose on 0 fuel:
        if (fuelCount <= 0)
        {
            MyGame myGame = (MyGame)game;
            myGame.LevelOver("GameOver");
        }
    }
    private void Timers()
    {
        //Collision timer:
        if (collisionCooldownTimer != 0)
        {
            collisionCooldownTimer -= Time.deltaTime;
        }
        //Shield timer:
        if (shieldTimer != 0)
        {
            shieldTimer -= Time.deltaTime;
        }

        else
        {
            if (hasShield) shield.LateDestroy();
            hasShield = false;
            shieldTimer = 0;
            if (shieldSound != null)
            {
                shieldSound.Stop();
                shieldSound = new Sound("Shield_Down.wav").Play();
            }
        }

        //Boost timer:
        if (boostTimer != 0)
        {
            boostTimer -= Time.deltaTime;
        }

        else
        {
            isBoosting = false;
            boostTimer = 0;
            fuelConsumptionRate = 1;
        }

        //Ship animation:
        if ((Input.GetKey(Key.W) || Input.GetKey(Key.A) || Input.GetKey(Key.D)) && fuelCount > 0)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= 200)
            {
                NextFrame();
                animationTimer = 0;
            }
        }
    }

    private void DoSounds()
    {
        if ((Input.GetKey(Key.W) || Input.GetKey(Key.A) || Input.GetKey(Key.D)) && fuelCount > 0 && !lostControl && !spaceshipSound.IsPlaying) spaceshipSound = new Sound("spaceshipSounds.wav").Play();
    }

    void death()
    {
        int random;
        string png;
        MyGame myGame = (MyGame)game;
        for (int i = 0; i < 10; i++)
        {
            random = Utils.Random(1, 4);
            switch (random)
            {
                case (1):
                    png = "wreck1.png";
                    myGame.pickupLayer.LateAddChild(new Wreck(png, x, y, false));
                    break;
                case (2):
                    png = "wreck2.png";
                    myGame.pickupLayer.LateAddChild(new Wreck(png, x, y,false));
                    break;
                case (3):
                    png = "wreck3.png";
                    myGame.pickupLayer.LateAddChild(new Wreck(png, x, y, false));
                    break;
                case (4):
                    png = "wreck4.png";
                    myGame.pickupLayer.LateAddChild(new Wreck(png, x, y,false));
                    break;

            }
            

        }
        LateDestroy();
    }

    private void UpdateHUD()
    {
        HUD.UpdateHUD(health, fuelCount);
    }
}