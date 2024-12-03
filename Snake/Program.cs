﻿using Snake;

Console.CursorVisible = false;
Console.SetWindowSize(120, 30);

int[,] map = new int[60, 30];//not really used anymore
int[,] lastMap = new int[60, 30];//not used anymore

double[] input;
Neural nn = new Neural([map.Length,map.Length/4,4],0.9);//Neural nn = new Neural([map.Length,map.Length/2,map.Length/4,4],0.9);

Player player;
int[] aapple;

while (true)
{
    player = new Player(30, 15);
    aapple = [1, 1];
    
    NewApple();
    StartUp(map);
    StartUp(lastMap);
    for (int i = 0; i < 2; i++) player.Grow();

    while (!player.dead)
    {
        //draw Apple
        map[aapple[0], aapple[1]] = 2;

        double[] lAstInput = make1D(lastMap);
        input = make1D(map);

        int a = (player.head.x-aapple[0])*(player.head.x-aapple[0]);//a^2
        int b =(player.head.y-aapple[1])*(player.head.y-aapple[1]);//b^2
        double dist =Math.Sqrt(a+b);//c
        double rewardDist = 1/dist;

        player.move(map,AiControl());
        player.Draw(map);

        
        //eat apple if head of snake at apple
        if (player.head.x == aapple[0] && player.head.y == aapple[1]) EatApple();

        Thread.Sleep(200);
    }
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Clear();
}

void StartUp(int[,] array)
{
    // Give entire array a "Value"
    for (int y = 0; y < array.GetLength(1); y++)
    {
        for (int x = 0; x < array.GetLength(0); x++)
        {
            array[x, y] = 0;
        }
    }
}

void EatApple()
{
    player.Grow();
    NewApple();
}

void NewApple()
{
    int i = 0;
    while (player.segments.Any(segment => segment.x == aapple[0] && segment.y == aapple[1]) || i == 0)
    {
        aapple[0] = Random.Shared.Next(60);
        aapple[1] = Random.Shared.Next(30);
        i = 1;
    }
    map[aapple[0], aapple[1]] = 2;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.SetCursorPosition(2 * aapple[0], aapple[1]);
    Console.Write("  ");
}

double[] make1D(int[,] map)
{
    List<double> stuff = [];
    foreach (int i in map)
    {
        stuff.Add(i);
    }
    return stuff.ToArray();
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

    int[] PlayerControl()
    {
        int[] direction = player.vel;
         if (player.nextDirection != null)
        {
            direction = player.nextDirection; // change direction based on last input
            player.nextDirection = null;
            if (Console.KeyAvailable)  // Check if a key is pressed
            {
                player.nextDirection = Inputs();//store next input
            }
        }
        else if (Console.KeyAvailable)  // Check if a key is pressed
        {
            direction = Inputs(); // change direction based on current input

            if (Console.KeyAvailable)  // Check if a key is pressed
            {
                player.nextDirection = Inputs(); //store next input
            }
        }
        while (Console.KeyAvailable) // clear qued up inputs exept stored
        {
            Console.ReadKey(intercept: true);
        }
        return direction;
    }

    int[] AiControl()
    {
        double[] acction = nn.DoThing(input);
        //Console.WriteLine(acction[0] + " " + acction[1]);

        double largest=0;
        int thingie = 0;
        int[][] options = [[0,1],[1,0],[-1,0],[0,-1]];
        for (int i = 0; i < acction.Length; i++)
        {
            if (acction[i]>largest)
            {
                largest = acction[i];
                thingie = i;
            }
        }  
        //Console.WriteLine(largest+"   "+ thingie);
        return options[thingie]; 
    }