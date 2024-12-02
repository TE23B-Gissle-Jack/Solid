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

    int[] direction = [1, 0];
    int[] nextDirection;
    public void move(int[,] map)
    {
        int[] last = [head.x, head.y];

        if (nextDirection != null)
        {
            direction = nextDirection; // change direction based on last input
            nextDirection = null;
            if (Console.KeyAvailable)  // Check if a key is pressed
            {
                nextDirection = Inputs();//store next input
            }
        }
        else if (Console.KeyAvailable)  // Check if a key is pressed
        {
            direction = Inputs(); // change direction based on current input

            if (Console.KeyAvailable)  // Check if a key is pressed
            {
                nextDirection = Inputs(); //store next input
            }
        }
        while (Console.KeyAvailable) // clear qued up inputs exept stored
        {
            Console.ReadKey(intercept: true);
        }
        head.x += direction[0];
        head.y += direction[1];

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
        //here just cuz
        map[last[0], last[1]] = 0;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(2 * last[0], last[1]);
        Console.Write("  ");
    }

    //Not Really... But really now
    public void Draw(int[,] map)
    {
        //Assigne Colors at player positoion
        map[this.head.x, this.head.y] = 1;
        Console.BackgroundColor = ConsoleColor.Green;   //map[this.head.x, this.head.y];
        Console.SetCursorPosition(2 * this.head.x, this.head.y);
        Console.Write("  ");

        if (this.segments.Count > 0)
        {
            foreach (Player.Segment segment in this.segments)
            {
                map[segment.x, segment.y] = 1;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(2 * segment.x, segment.y);
                Console.Write("  ");
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
