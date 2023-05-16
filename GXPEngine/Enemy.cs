using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
public class Enemy : AnimationSprite
{
    private float health = 100;
    private float attachedToRopeSegment;
    private float ropesClimbed = 0;
    private float timer = 0;
    private Vector2 oldPosition;
    private Vector2 newPosition;
    private Vector2 velocity;
    private float mass = 1.95f;
    private float collisionTimer = 0;
    private float animationTimer = 0;
    private float damageTimer = 0;
    private bool damaged = false;
    private float damageCooldown = 15;
    public Enemy(int sX, int sY) : base("enemy.png", 4, 1)
    {
        scale = 0.25f;
        SetOrigin(width / 2, height / 2);

        SetXY(sX, sY);
    }
    void Update()
    {
        ClimbRope();
        AttachToRope();
        Rotate();
        CollisionTimer();
        CheckHP();
        Animation();
        damagedTimer();
    }
    void OnCollision(GameObject other)
    {
        if (other is Planet)
        {
          //  Console.WriteLine("collide");
            Planet planet = (Planet)other;
            Vector2 main;
            Vector2 pos;
            float angleX;

            main.x = x;
            main.y = y;

            pos.x = planet.x;
            pos.y = planet.y;

            pos = pos - main;

            if (pos.Length() < planet.width / 2 + width / 2)
            {
                main = -velocity;

                if (pos.GetAngleDegrees() > main.GetAngleDegrees())
                {
                    angleX = pos.GetAngleDegrees() - main.GetAngleDegrees();
                    main.SetAngleDegrees(angleX);
                }
                else
                {
                    angleX = pos.GetAngleDegrees() - pos.GetAngleDegrees();
                    main.SetAngleDegrees(angleX);
                
                }

                MyGame myGame = (MyGame)game;
                List<GameObject> children = myGame.ropeLayer.GetChildren();
                
                foreach (GameObject child in children)
                {
                    if (child is Rope)
                    {
                        Rope rope = (Rope)child;

                        // Bounce calculation:
                        rope.additionalVelocity = main*1f;
                        
                    }
                }

                //Damage:
                CalculateDamage(velocity);

                //Start timer:
                collisionTimer = 750;

                //Sound:
                SoundChannel alienHit = new Sound("Alien_Hit.wav").Play();
                SoundChannel alienScream = new Sound("cat_scream.wav").Play();
            }
        }
        if (other is Gravity)
        {
            //  Console.WriteLine("collide");
            Gravity planet = (Gravity)other;
            Vector2 main;
            Vector2 pos;
            float angleX;

            main.x = x;
            main.y = y;

            pos.x = planet.x;
            pos.y = planet.y;

            pos = pos - main;

            if (pos.Length() < planet.width / 2 + width / 2)
            {
                pos = pos.Normalized();

                MyGame myGame = (MyGame)game;
                List<GameObject> children = myGame.ropeLayer.GetChildren();

                foreach (GameObject child in children)
                {
                    if (child is Rope)
                    {
                        Rope rope = (Rope)child;

                        // Bounce calculation:
                        rope.additionalVelocity += pos * 0.1f;

                    }
                }
            }
        }

        if (other is Asteroid && collisionTimer == 0)
        {
            Asteroid asteroid = (Asteroid)other;

            //Calculate CoM:
            Vector2 asteroidMomentum = asteroid.velocity.GetMomentum(asteroid.mass);
            Vector2 enemyMomentum = velocity.GetMomentum(mass);
            Vector2 centerOfMass = (asteroidMomentum + enemyMomentum) / (asteroid.mass + mass);

            //Apply bounce to asteroid:
            asteroid.velocity = centerOfMass - 1 * (asteroid.velocity - centerOfMass);

            //Apply Bounce to Rope:
            
            //Find rope:
            MyGame myGame = (MyGame)game;
            List<GameObject> children = myGame.ropeLayer.GetChildren();

            foreach (GameObject child in children)
            {
                if (child is Rope)
                {
                    Rope rope = (Rope)child;

                    // Bounce calculation:
                    rope.additionalVelocity = (centerOfMass - 1 * (enemyMomentum - centerOfMass))/3;
                }
            }



            //Damage:
            CalculateDamage(centerOfMass);

            //Start timer:
            collisionTimer = 750;

            //Sound:
            SoundChannel alienHit = new Sound("Alien_Hit.wav").Play();
            SoundChannel alienScream = new Sound("cat_scream.wav").Play();
        }
    }
    void ClimbRope()
    {
        timer += Time.deltaTime;
        if (timer >= 2000)
        {
            ropesClimbed++;
            timer = 0;
        }

        MyGame myGame = (MyGame)game;
        List<GameObject> children = myGame.ropeLayer.GetChildren();

        foreach (GameObject child in children)
        {
            if (child is Rope)
            {
                Rope rope = (Rope)child;
                if (ropesClimbed == rope.segmentList.Count)
                {
                    myGame.LevelOver("GameOver");
                }
            }
        }
    }
    void AttachToRope()
    {
        //Find rope:
        MyGame myGame = (MyGame)game;
        List<GameObject> children = myGame.ropeLayer.GetChildren();

        foreach (GameObject child in children)
        {
            if (child is Rope)
            {
                // Set rope segment to which enemy needs to be attached:
                Rope rope = (Rope)child;
                attachedToRopeSegment = rope.segmentList.Count - 1 - ropesClimbed;
                // Set XY to rope segment XY:
                for (int i = rope.segmentList.Count - 1; i >= 0; i--)
                {
                    if (i == attachedToRopeSegment)
                    {
                        // Save old position:
                        oldPosition = new Vector2(x, y);

                        // Change position:
                        Vector2 position = rope.segmentList[i];
                        if (i != 0 && rope.segmentList[i].y > rope.segmentList[i - 1].y) x = position.x - 50;
                        else x = position.x + 50;
                        y = position.y;

                        // Save new position:
                        newPosition = new Vector2(x, y);

                        // Save velocity:
                        velocity = newPosition - oldPosition;
                    }
                }
            }
        }
    }
    void Rotate()
    {
        //Find rope:
        MyGame myGame = (MyGame)game;
        List<GameObject> children = myGame.ropeLayer.GetChildren();

        foreach (GameObject child in children)
        {
            if (child is Rope)
            {
                // Set rope segment to which enemy needs to be attached:
                Rope rope = (Rope)child;
                attachedToRopeSegment = rope.segmentList.Count - 1 - ropesClimbed;

                // Set XY to rope segment XY:
                for (int i = rope.segmentList.Count - 1; i >= 0; i--)
                {
                    if (i != 0 && i == attachedToRopeSegment)
                    {
                        Vector2 vec = rope.segmentList[i] - rope.segmentList[i-1];
                        float angle = vec.GetAngleDegrees();
                        rotation = angle - 90;
                    }
                }
            }
        }
    }
    void CalculateDamage(Vector2 CoM)
    {
        if (!damaged)
        {
            health -= CoM.Length();
            damaged = true;
            SetColor(0.9f, 0, 0);
        }
    }
    void CollisionTimer()
    {
        if (collisionTimer > 0) collisionTimer -= Time.deltaTime;
        if (collisionTimer < 0) collisionTimer = 0;
    }

    void CheckHP()
    {
        if (health <= 0)
        {
            MyGame myGame = (MyGame)game;
            
            SoundChannel enemyDies = new Sound("Enemy_Explodes.wav").Play();
            float random;
            string png;
            for (int i = 0; i < 10; i++)
            {
                random = Utils.Random(1, 4);
                switch (random)
                {
                    case (1):
                        png = "wreck1.png";
                        myGame.pickupLayer.LateAddChild(new Wreck(png, x, y, true));
                        break;
                    case (2):
                        png = "wreck2.png";
                        myGame.pickupLayer.LateAddChild(new Wreck(png, x, y, true));
                        break;
                    case (3):
                        png = "wreck3.png";
                        myGame.pickupLayer.LateAddChild(new Wreck(png, x, y, true));
                        break;
                    case (4):
                        png = "wreck4.png";
                        myGame.pickupLayer.LateAddChild(new Wreck(png, x, y, true));
                        break;
                }


            }
            Destroy();
            
            myGame.LevelOver("WinScreen");
        }
    }

    void Animation()
    {
        animationTimer += Time.deltaTime;

        if (animationTimer <= 200)
        {
            animationTimer = 0;
            NextFrame();
        }
    }
    void damagedTimer()
    {
        if (damaged)
        {
            damageTimer++;

            if(damageTimer > damageCooldown)
            {
                damaged = false;
                SetColor(1f, 1f, 1f);
            }
        }

    }
}