using System;

namespace Snake;

public class FimbleWinter
{
    int e;

    double[,,] q;

    public FimbleWinter(int[] mapSize, int possibleActions,int trainChoCho)
    {
                            //state         //posible actions
        q = new double[mapSize[0],mapSize[1],possibleActions];// might need last frame
        // 1:   initialize  Q
        q = initilizeQ(q);
        // 2:   set the number of episodes (E)
        e = trainChoCho;
    }

    private double[,,] initilizeQ(double[,,] q)
    {
         double[,,] layerWeights = new double[,,]{};
        for (int x = 0; x < layerWeights.GetLength(0); x++)
        {
            for (int y = 0; y < layerWeights.GetLength(1); y++)
            {
                for (int z = 0; z < layerWeights.GetLength(2); z++)
                {
                    layerWeights[x, y, z] = Random.Shared.NextDouble();
                }
            }
        }
        return layerWeights;
    }

    public void DoThing(double[,] state)
    {
            // 3:   set the maximum number of steps per episode  T
            int t = 100;
            // 4:   for  e  =  1 , 2 , ..., E
            for(int i = 0;i<e;i++)
            {
                // 5:   k⟵1
                int k = 1;

                // 6:   select a random initial state  s1
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //player = new Player(random.shard.next(mapSizeX), random.shard.next(mapSizeY));
                //NewApple()


                // 7:   while goal state not reached and  k≤T
                while(k<=t/*&&!player.dead && player.segments.Count < map.Length-1*/)
                {
                    // 8:  select a valid action  ak  at random
                    //^^^^^^^^^^^^^^^^^^^^^^^^^^
                    // acction = random.shared.next(4);
                    // player.move(map,options[acction]);//options[acction]//PlayerControl()^


                    // 9:  record the resulting state  sk+1 and corresponding reward  rk
                    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                    // if (dist!=0)
                    // {
                    // if(dist>lastDist) reward=-1;
                    // else if(dist<lastDist) reward=1;
                    // else reward =0;
                    // }
                    // else reward = 10;
                    //if die reward = -5

                    // 10: Q(sk,ak) = rk+maximumi∈Ω(sk+1)Q(sk+1,αi)
                    //q[map]
                }
            }

            // 11: k⟵k+1

            // 12:  end while
            // 13:  end for
    }

}
