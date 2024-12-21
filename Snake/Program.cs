using Snake;
int e = 1;//ai randomnes

int mapSizeX = 10;
int mapSizeY = 10;

Console.CursorVisible = false;
Console.SetWindowSize(mapSizeX*2, mapSizeY);

int[,] map = new int[mapSizeX, mapSizeY];//not really used anymore
int[,] lastMap = new int[mapSizeX, mapSizeY];//not used anymore

double[] AiInput = new double[map.Length*2];
int reward=0;
Neural nn = new Neural([map.Length*2,20,4],0.9);//Neural nn = new Neural([map.Length,map.Length/2,map.Length/4,4],0.9);

Player player;
int[] aapple;

int acction = 0;

while (true)
{
    player = new Player(Random.Shared.Next(mapSizeX), Random.Shared.Next(mapSizeY));
    aapple = [1, 1];
    
    NewApple();
    StartUp(map);
    StartUp(lastMap);
    for (int i = 0; i < 0; i++) player.Grow();

    double lastDist = double.MaxValue;
    while (!player.dead && player.segments.Count < map.Length-1)
    {
        //double[] lAstInput = make1D(lastMap);
        AiInput = make1D(map,lastMap);

        int a = (player.head.x-aapple[0])*(player.head.x-aapple[0]);//a^2
        int b =(player.head.y-aapple[1])*(player.head.y-aapple[1]);//b^2
        double dist =Math.Sqrt(a+b);//c
        if (dist!=0)
        {
           if(dist>lastDist) reward=-1;
           else if(dist<lastDist) reward=1;
           else reward =0;
        }
        lastDist=dist;
                            //learns to take first option
        int[][] options = [[1,0],[0,1],[-1,0],[0,-1]];
        acction = AiControl();

        lastMap = map;
        player.move(map,options[acction]);//options[acction]//PlayerControl()
        player.Draw(map);

        //draw Apple
        map[aapple[0], aapple[1]] = 1;
        
        //eat apple if head of snake at apple
        if (player.head.x == aapple[0] && player.head.y == aapple[1]) EatApple(false);

        nn.Train(reward,AiInput,acction);

        Thread.Sleep(100);
        reward=0;
    }
    reward = -7;
    nn.Train(reward,AiInput,acction);
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

void EatApple(bool ai)
{
    player.Grow();
    if(ai)reward=10;
    NewApple();
}

void NewApple()
{
    int i = 0;
    while (player.segments.Any(segment => segment.x == aapple[0] && segment.y == aapple[1]) || i == 0)
    {
        aapple[0] = Random.Shared.Next(mapSizeX);
        aapple[1] = Random.Shared.Next(mapSizeY);

        //  aapple[0] = 4;
        //  aapple[1] = 2;

        i = 1;
    }
    map[aapple[0], aapple[1]] = 1;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.SetCursorPosition(2 * aapple[0], aapple[1]);
    Console.Write("  ");
}

double[] make1D(int[,] map,int[,]lastMap)
{
    List<double> stuff = [];
    foreach (int i in map)
    {
        stuff.Add(i);
    }
    foreach (int i in lastMap)
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
            return [0,0];
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


    
    int AiControl()
    {
        int acction = nn.DoThing(AiInput);

        if (Random.Shared.Next(e)==0)
        {
            acction = Random.Shared.Next(4);

            if (Random.Shared.Next(100)==0)e ++;
        }
        
        return acction;
    }