using System;

namespace Snake;

public class Player(int x, int y)
{
    public List<Segment> segments = [];
    public Segment head = new Segment() { x = x, y = y };

    public bool dead = false;

    public void Grow()
    {
        if (segments.Count > 0) segments.Add(new Segment() { x = segments.Last().x, y = segments.Last().y });
        else segments.Add(new Segment() { x = head.x, y = head.y });
    }

    public int[] vel = [1, 0];
    public int[] nextDirection;
    public void move(int[,] map, int[] direction)
    {
        int[] last = [head.x, head.y];

        if(direction!=vel&&direction!=null)vel=direction;

        try
        {
            head.x += vel[0];
            head.y += vel[1];
        }
        catch
        {
            dead = Die();
        }
        
        if (segments.Count > 0)
        {
            foreach (Segment part in segments)
            {
                if ((part.x == head.x) && (part.y == head.y))
                {
                    dead = Die();
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
        try
        {
            map[this.head.x, this.head.y] = 1;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(2 * this.head.x, this.head.y);
            Console.Write("  ");
        }
        catch
        {
            dead = Die();
        }
        

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

    public bool Die()//Die
    {
        return true;
        //Environment.Exit(0);
    }

   

    public class Segment()
    {
        public int x;
        public int y;
    }
}
