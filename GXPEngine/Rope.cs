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

public class Rope : GameObject
{
    private float ropeLength;
    private float lineSegmentSize = 0.25f;
    private Vector2 startPos;
    Player player;
    List<Vector2> segmentList = new List<Vector2>();
    List<Vector2> oldsegmentList = new List<Vector2>();
    EasyDraw drawingSpace;
    public Rope(float Length, Player pPlayer) : base(false)
    {
        player = pPlayer;
        drawingSpace = new EasyDraw(1080, 1920, false);
        ropeLength = Length;

        startPos.x = player.ropeAttachPoint.x;
        startPos.y = player.ropeAttachPoint.y;
        
        //creates all segments of the line
        for (int i = 0; i < ropeLength; i++)
        {
            segmentList.Add(startPos);
            oldsegmentList.Add(startPos);
            startPos.y -= lineSegmentSize;
            Console.WriteLine(startPos);
        }
    }

    void Update()
    {
        linePhysics();
        contraints();
        drawLine();
    }

    void linePhysics()
    {
        for (int i = 0; i < ropeLength; i++)
        {
            Vector2 firstSegment = segmentList[i];
            segmentList[0] = firstSegment;
            Vector2 Velocity = firstSegment - oldsegmentList[i];
            oldsegmentList[i] = firstSegment;
            firstSegment += Velocity / 1.001f;
            segmentList[i] = firstSegment;
        }

    }

    void contraints()
    {
        for (int i = 0; i < 50; i++)
        {
            contraining();
        }
    }

    void contraining()
    {
        Vector2 firstSegment = segmentList[0];
        firstSegment.x = player.ropeAttachPoint.x;
        firstSegment.y = player.ropeAttachPoint.y;
        segmentList[0] = firstSegment;
        for (int i = 0; i < ropeLength - 1; i++)
        {
            Vector2 firstSeg = segmentList[i];
            Vector2 secondSeg = segmentList[i + 1];
            float dist = (firstSeg - secondSeg).Length();
            float error = Mathf.Abs(dist - ropeLength);
            Vector2 changeDir = new Vector2(0, 0);

            if (dist > ropeLength)
            {
                changeDir = (firstSeg - secondSeg).Normalized();
            }
            else if (dist < ropeLength)
            {
                changeDir = (secondSeg - firstSeg).Normalized();
            }
            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg -= changeAmount * 0.5f;
                segmentList[i] = firstSeg;
                secondSeg += changeAmount * 0.5f;
                segmentList[i + 1] = secondSeg;
            }
            else
            {
                secondSeg += changeAmount;
                segmentList[i + 1] = secondSeg;
            }
        }
    }

    public void drawLine()
    {

        Vector2 oldVector;
        Vector2 newVector;
        for (int i = 1; i < ropeLength; i++)
        {

            Console.WriteLine(segmentList[0]);
            oldVector = segmentList[i - 1];
            newVector = segmentList[i];
            Gizmos.DrawLine(oldVector.x, oldVector.y, newVector.x, newVector.y);

        }
    }
}