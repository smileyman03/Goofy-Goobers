using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
public class Background : AnimationSprite
{
    private float timer = 0;
    private Boolean doAnimation = true;
    private float animationInterval;
    private float nextFrameTimer = 0;
    public Background(String filename) : base(filename, 7, 1)
    {
        animationInterval = Utils.Random(5, 20) * 1000;
    }

    void Update()
    {
        if (!doAnimation)
        {
            timer += Time.deltaTime;
            if (timer > animationInterval)
            {
                doAnimation = true;
                animationInterval = Utils.Random(5, 20) * 1000;
            }
        }

        if (doAnimation)
        {
            nextFrameTimer += Time.deltaTime;
            if (nextFrameTimer >= 150)
            {
                NextFrame();
                nextFrameTimer = 0;
                timer = 0;
                if (currentFrame == 6) doAnimation = false;
            }
        }
    }
}