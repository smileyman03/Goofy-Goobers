﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public Enemy() : base("enemy.png", 4, 1)
    {
        scale = 0.25f;
        SetOrigin(width / 2, height / 2);
    }
    void Update()
    {
        ClimbRope();
        AttachToRope();
        CollisionTimer();
        CheckHP();
    }
    void OnCollision(GameObject other)
    {
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
                    rope.additionalVelocity = centerOfMass - 1 * (enemyMomentum - centerOfMass);
                }
            }

            //Damage:
            CalculateDamage(centerOfMass);

            //Start timer:
            collisionTimer = 750;
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
                if (ropesClimbed > rope.ropePositions.Count)
                {
                    //Do Game over;
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
                attachedToRopeSegment = rope.ropePositions.Count - 1 - ropesClimbed;

                // Set XY to rope segment XY:
                for (int i = rope.ropePositions.Count - 1; i >= 0; i--)
                {
                    if (i == attachedToRopeSegment)
                    {
                        // Save old position:
                        oldPosition = new Vector2(x, y);

                        // Change position:
                        Vector2 position = rope.ropePositions[i];
                        x = position.x;
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
    void CalculateDamage(Vector2 CoM)
    {
        health -= CoM.Length() / 2f;
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
            //Do Victory
        }
    }
}