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
    public Wreck(string sprite, float sX, float sY, bool isEnemy) : base(sprite)
    {
        SetScaleXY(0.1f);
        SetXY(sX, sY);
        if (isEnemy)
        {
            SetColor(0, 0.8f, 0);
        }
        randomRotation = Utils.Random(-100, 100)/20;
        randomSpeed = Vector2.RandomUnitVector();
    }
    void Update()
    {
        rotation += randomRotation;
        x += randomSpeed.x;
        y += randomSpeed.y;
    }
}