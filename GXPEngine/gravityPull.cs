﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using System.Collections;

public class Gravity : Sprite
{
    
    private float pullStrength;
    public Gravity(float size, int sX, int sY) : base("gravity.png", true)
    {
        SetOrigin(width / 2, height / 2);
        x = sX;
        y = sY;
        SetScaleXY(size);
        


    }
    void Update()
    {
        rotation -= 0.07f;
    }

    /*
    public Vector2 vecPull(Vector2 pos)
    {
        Vector2 pull;




        return pull;
    }
    */

    void OnCollision(GameObject Collider)
    {
        if(Collider is planet)
        {
            
        }
        else
        {
       
        }
        
    }
}
