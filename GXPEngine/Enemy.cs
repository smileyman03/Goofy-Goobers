using System;
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
    public Enemy() : base("enemy.png", 4, 1)
    {
        scale = 0.25f;
        SetOrigin(width / 2, height / 2);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2000)
        {
            ropesClimbed++;
            timer = 0;
        }

        AttachToRope();
    }

    void OnCollision(GameObject other)
    {
        if (other is Asteroid)
        {

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
                        Vector2 position = rope.ropePositions[i];
                        Console.WriteLine("rope x: " + position.x + " rope y: " + position.y);
                        x = position.x;
                        y = position.y;
                    }
                }
            }
        }
    }
}