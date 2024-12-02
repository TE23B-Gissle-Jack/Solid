using Snake;

int[,] map = new int[20, 10];//not really used anymore
int[,] lastMap = new int[20, 10];//not used anymore

Player player = new Player(0, 0);
int[] aapple = [1, 1];

NewApple();
StartUp(map);
StartUp(lastMap);
for (int i = 0; i < 2; i++) player.Grow();

Console.CursorVisible = false;
Console.SetWindowSize(40, 10);
while (true)
{
    //draw Apple
    map[aapple[0], aapple[1]] = 2;

    player.move(map);
    player.Draw(map);

    int[] lAstInput = make1D(lastMap);
    int[] input = make1D(map);

    //eat apple if head of snake at apple
    if (player.head.x == aapple[0] && player.head.y == aapple[1]) EatApple();

    Thread.Sleep(200);
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
        aapple[0] = Random.Shared.Next(20);
        aapple[1] = Random.Shared.Next(10);
        i = 1;
    }
    map[aapple[0],aapple[1]] = 2;
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.SetCursorPosition(2 * aapple[0], aapple[1]);
    Console.Write("  ");
}

int[] make1D(int[,] map)
{
    List<int> stuff =[];
    foreach(int i in map)
    {
        stuff.Add(i);
    }
    return stuff.ToArray();
}