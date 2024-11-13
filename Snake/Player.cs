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

    void Die()
    {
        Environment.Exit(0);
    }

    int[] Inputs()
    {
        var keyInfo = Console.ReadKey(intercept: true); // intercept: true prevents the key from being written to the console

        // Get the key that was pressed
        ConsoleKey key = keyInfo.Key;

        // Handle specific key presses
        if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
        {
            Console.WriteLine("Up arrow key pressed.");
            return [0, -1];
        }
        else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
        {
            Console.WriteLine("Down arrow key pressed.");
            return [0, 1];
        }
        else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
        {
            Console.WriteLine("Left arrow key pressed.");
            return [-1, 0];
        }
        else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
        {
            Console.WriteLine("Right arrow key pressed.");
            return [1, 0];
        }
        else
        {
            Console.WriteLine($"Key {key} was pressed.");
            return [0, 0];
        }
    }

    public class Segment()
    {
        public int x;
        public int y;
    }
}
