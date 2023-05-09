using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;

public class Asteroid : Sprite
{
    public Vector2 velocity = new Vector2(Utils.Random(0, 10f), Utils.Random(0, 10f));
    public float mass = 0.5f;
    public Asteroid() : base("circle.png")
    {
        SetOrigin(width / 2, height / 2);
        SetXY(0, 0);
    }

    void Update()
    {
        x += velocity.x;
        y += velocity.y;
    }
}