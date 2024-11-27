using Snake;

ConsoleColor[,] map = new ConsoleColor[60, 20];
ConsoleColor[,] lastMap = new ConsoleColor[60, 20];

Player player = new Player(1, 1);
int[] aapple = [1,1];

NewApple();
StartUp(map);
StartUp(lastMap);
for (int i = 0; i < 2; i++) player.Grow();

Console.CursorVisible = false;
while (true)
{
    //draw Apple
    map[aapple[0], aapple[1]] = ConsoleColor.DarkRed;

    player.move();
    player.Draw(map);

    //eat apple if head of snake at apple
    if (player.head.x == aapple[0] && player.head.y == aapple[1]) EatApple();

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
        aapple[1] = Random.Shared.Next(20);
        i = 1;
    }
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.SetCursorPosition(2 * aapple[0], aapple[1]);
    Console.Write("  ");
}