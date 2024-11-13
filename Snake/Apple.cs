using System;

namespace Snake;

public class Apple
{
    public int x;
    public int y;

    public void Eat(Player player)
    {
        player.Grow();
        NewApple(player);
    }
    public void NewApple(Player player)
    {
        int i = 0;
        while(player.segments.Any(segment => segment.x == x && segment.y == y)|| i == 0)
        {
            x = Random.Shared.Next(60);
            y = Random.Shared.Next(20);
            i=1;
        }
    }
}
