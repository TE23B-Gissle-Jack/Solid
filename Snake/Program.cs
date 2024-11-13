using Snake;

ConsoleColor[,] map = new ConsoleColor[60, 20];
//int[] snake = [1, 1];

Player player = new Player(1, 1);
Apple apple = new Apple();
apple.NewApple(player);
StartUp(map);
//map[snake[0], snake[1]] = 1;
for(int i = 0; i<0;i++)player.Grow();

while (true)
{
    Console.Clear();
    StartUp(map);

    map[apple.x,apple.y] = ConsoleColor.DarkRed;

    map[player.head.x, player.head.y] = ConsoleColor.Green;
    if (player.segments.Count > 0)
    {
        foreach (Player.Segment segment in player.segments)
        {
            map[segment.x, segment.y] = ConsoleColor.Green;
        }
    }
    DisplayMap(map);

    player.move();
    if(player.head.x == apple.x && player.head.y == apple.y) apple.Eat(player);

    Thread.Sleep(200);
}




void StartUp(ConsoleColor[,] array)
{
    // Initialize all values in the map to 0
    for (int y = 0; y < array.GetLength(1); y++)
    {
        for (int x = 0; x < array.GetLength(0); x++)
        {
            array[x, y] = ConsoleColor.Black;
        }
    }
}

void DisplayMap(ConsoleColor[,] map)
{


    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            Console.BackgroundColor = map[x,y];
            Console.Write("  "); // No pixel
        }
        Console.WriteLine(); // Move to the next row
    }
    Console.BackgroundColor = ConsoleColor.Black; // make rest of bgc black
}
