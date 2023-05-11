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
public class planet : Sprite
{

    public planet(string sprite, float gravity, int sX, int sY, Pivot layer) : base(sprite)
    {
        SetOrigin(width / 2, height / 2);
        x = sX;
        y = sY;
        layer.AddChild(new Gravity(gravity, (int)x, (int)y));


    }
    void Update()
    {
        rotation += 0.05f;
    }
}
