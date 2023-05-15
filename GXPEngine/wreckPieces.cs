using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using System.Collections;
public class Wreck : Sprite
{
    private float randomRotation;
    private Vector2 randomSpeed;
    public Wreck(string sprite, int sX, int sY) : base(sprite)
    {
        SetXY(sX, sY);
        randomRotation = Utils.Random(-100, 100) / 50;
        randomSpeed = Vector2.RandomUnitVector();
    }
    void Update()
    {
        rotation += 0.05f;
        x += randomSpeed.x;
        y += randomSpeed.y;
    }
}