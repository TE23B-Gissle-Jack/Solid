using System;

namespace Snake;

public class Neural
{
    List<double[,]> weights;
    int[] neurons;
    double learningRate;

    public Neural(int[] neu, double lr)
    {
        neurons = neu;
        learningRate = lr;
        weights = new List<double[,]>();
        
        for (int i = 1; i < neurons.Length; i++)
        {
            weights.Add(makeWeights(neurons[i],neurons[i-1]));
        }
    }

    private double[,] makeWeights(int currentLayer, int lastLayer)
    {
        double[,] layerWeights = new double[currentLayer,lastLayer];
        for (int i = 0; i < currentLayer; i++)
        {
            for (int j = 0; j < lastLayer; j++)
            {
                layerWeights[i,j] = Random.Shared.NextDouble();
            }
        }
        return layerWeights;
    }

    public double[] DoThing(double[] input)
    {
        List<double> outputs = new List<double>();
        double[] acction = [];
        for (int i = 1; i < neurons.Length; i++)// for every layer
        {   
            //double[] outputs = new double[neurons[i]];//list of outputs this layer //needs to be inputs next layer     
            if(i==1) outputs = new List<double>(input);;

            for (int j = 0; j < neurons[i]; j++)//for every neuron in layer
            {
                double output = 0;
                for (int k = 0; k < neurons[i-1]; k++)// for every neuron in last layer
                {
                    output += outputs[k]*weights[i-1][j,k];//* somthing to make betwen 0-1
                }
                outputs[j] = 1/(1+Math.Exp(output));//sigmoid or something to make betwen 0-1
            }
        }
        return outputs[..4].ToArray();
    }
}
