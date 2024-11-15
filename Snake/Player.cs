using System;

namespace Snake;

public class Player(int x, int y)
{
    public List<Segment> segments = [];
    public Segment head = new Segment() { x = x, y = y };

    public void Grow()
    {
        if (segments.Count > 0) segments.Add(new Segment() { x = segments.Last().x, y = segments.Last().y });
        else segments.Add(new Segment() { x = head.x, y = head.y });
    }

    int[] newt = [0, 1];
    public void move()
    {
        int[] last = [head.x, head.y];

        if (Console.KeyAvailable)  // Check if a key is pressed
        {
            newt = Inputs();
        }
        head.x += newt[0];
        head.y += newt[1];

        if (segments.Count > 0)
        {
            foreach (Segment part in segments)
            {
                if ((part.x == head.x) && (part.y == head.y))
                {
                    Die();
                }
                int[] store = [part.x, part.y];
                part.x = last[0];
                part.y = last[1];
                last = store;
            }
        }
    }

    //Not Really
    public void Draw(ConsoleColor[,] map)
    {
        //Assigne Colors at player positoion
        map[this.head.x, this.head.y] = ConsoleColor.Green;
        if (this.segments.Count > 0)
    {
        foreach (Player.Segment segment in this.segments)
        {
            map[segment.x, segment.y] = ConsoleColor.Green;
        }
    }
    }

    void Die()//Die
    {
        Environment.Exit(0);
    }

    int[] Inputs()
    {   
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Get key pressed and prevent it from being writen

        ConsoleKey key = keyInfo.Key;// Get the key that was pressed

        // Handle specific key presses
        if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
        {
            return [0, -1]; //direction player is going. So going -1 in Y here / going up
        }
        else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
        {
            return [0, 1];
        }
        else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
        {
            return [-1, 0];
        }
        else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
        {
            return [1, 0];
        }
        else
        {
            return [0, 0];
        }
    }

    public class Segment()
    {
        public int x;
        public int y;
    }
}
