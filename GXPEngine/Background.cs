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
    private Player player;
    public Background(String filename, Player palyer) : base(filename, 7, 1)
    {
        animationInterval = Utils.Random(5, 20) * 1000;
        player = palyer;
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
        x = player.x / 2;
        y = player.y / 2;
        
    }
}