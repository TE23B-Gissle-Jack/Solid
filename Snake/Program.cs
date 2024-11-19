using Snake;

ConsoleColor[,] map = new ConsoleColor[60, 20];
ConsoleColor[,] lastMap = new ConsoleColor[60, 20];

Player player = new Player(1, 1);
Apple apple = new Apple();
apple.NewApple(player);
StartUp(map);
StartUp(lastMap);
for(int i = 0; i<2;i++)player.Grow();

Console.CursorVisible = false;
while (true)
{
    //StartUp(map);
    
    //draw Apple
    map[apple.x,apple.y] = ConsoleColor.DarkRed;

    //DisplayMap(map);

    player.move();
    player.Draw(map);

    //eat apple if head of snake at apple
    if(player.head.x == apple.x && player.head.y == apple.y) apple.Eat(player);

    Thread.Sleep(200);
}

void StartUp(ConsoleColor[,] array)
{
    // Give entire array a "Value"
    for (int y = 0; y < array.GetLength(1); y++)
    {
        for (int x = 0; x < array.GetLength(0); x++)
        {
            array[x, y] = ConsoleColor.Black;
        }
    }
}

/*void DisplayMap(ConsoleColor[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            if (lastMap[x,y] != map[x,y])
            {
                Console.BackgroundColor = map[x, y];
                Console.SetCursorPosition(2*x, y);
                Console.Write("  ");
            }
            lastMap[x,y] = map[x,y];
        }
    }
    Console.BackgroundColor = ConsoleColor.Black; // make rest of console black
}*/