using Snake;

int[,] map = new int[60, 20];
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

    map[apple.x,apple.y] = 2;

    map[player.head.x, player.head.y] = 1;
    if (player.segments.Count > 0)
    {
        foreach (Player.Segment segment in player.segments)
        {
            map[segment.x, segment.y] = 1;
        }
    }
    DisplayMap(map);

    player.move();
    if(player.head.x == apple.x && player.head.y == apple.y) apple.Eat(player);

    Thread.Sleep(200);
}




void StartUp(int[,] array)
{
    // Initialize all values in the map to 0
    for (int y = 0; y < array.GetLength(1); y++)
    {
        for (int x = 0; x < array.GetLength(0); x++)
        {
            array[x, y] = 0;
        }
    }
}

void DisplayMap(int[,] map)
{


    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            int pixel = map[x, y];


            if (pixel == 0)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                //Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("  "); // No pixel
            }
            else if(pixel == 1)
            {
                //Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write("  "); //pixel
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("  "); //pixel
            }
        }
        Console.BackgroundColor = ConsoleColor.Black; // no yellow/cyan console
        Console.WriteLine(); // Move to the next row
    }
}
