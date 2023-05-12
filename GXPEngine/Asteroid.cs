using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
public class Asteroid : Sprite
{
    public Vector2 velocity = new Vector2(Utils.Random(2f, 5f), Utils.Random(2f, 5f));
    public float mass = 1f;
    public Asteroid(string image) : base(image)
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