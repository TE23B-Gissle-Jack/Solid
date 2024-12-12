using System;

namespace Snake;

public class FimbleWinter
{
    int e;

    double[,,] q;

    public FimbleWinter(int[] mapSize, int possibleActions,int trainChoCho)
    {
                            //state         //posible actions
        q = new double[mapSize[0],mapSize[1],possibleActions];
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
            // 3:   set the maximum number of steps per episode  T  // 
                                                                    // outside
            // 4:   for  e  =  1 , 2 , ..., E                       //

            // 5:   k⟵1

            // 6:   select a random initial state  s1

            // 7:   while goal state not reached and  k≤T

            // 8:  select a valid action  ak  at random

            // 9:  record the resulting state  sk+1 and corresponding reward  rk

            // 10: Q(sk,ak)⟵rk+maximumi∈Ω(sk+1)Q(sk+1,αi)

            // 11: k⟵k+1

            // 12:  end while
            // 13:  end for
    }

}
