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
            weights.Add(makeWeights(neurons[i], neurons[i - 1]));
        }
    }

    private double[,] makeWeights(int currentLayer, int lastLayer)
    {
        double[,] layerWeights = new double[currentLayer, lastLayer];
        for (int i = 0; i < currentLayer; i++)
        {
            for (int j = 0; j < lastLayer; j++)
            {
                layerWeights[i, j] = Random.Shared.NextDouble();
            }
        }
        return layerWeights;
    }

    public int DoThing(double[] input)
    {
        List<double> outputs = new List<double>();
        double[] acction = [];
        for (int i = 1; i < neurons.Length; i++)// for every layer
        {
            //double[] outputs = new double[neurons[i]];//list of outputs this layer //needs to be inputs next layer     
            if (i == 1) outputs = new List<double>(input); ;

            for (int j = 0; j < neurons[i]; j++)//for every neuron in layer
            {
                double output = 0;
                for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                {
                    output += outputs[k] * weights[i - 1][j, k];//* somthing to make betwen 0-1
                }
                outputs[j] = 1 / (1 + Math.Exp(output));//sigmoid or something to make betwen 0-1
            }
        }
        acction = outputs[..neurons[neurons.Length - 1]].ToArray();

        double largest = 0;
        int thingie = 0;
        int[][] options = [[0, 1], [1, 0], [-1, 0], [0, -1]];
        for (int i = 0; i < acction.Length; i++)
        {
            if (acction[i] > largest)
            {
                largest = acction[i];
                thingie = i;
            }
        }

        return thingie; //outputs[..neurons[neurons.Length-1]].ToArray();//dont care to clear the rest
    }

    //want the entirety of the above//same inputs and weigts

    //take in reward/punishment
    //reward/pusnish maybe future actions
    // to train// need to check wich weights had the most inpact
    // check next layer wich weigts had most inpact on previus

    public void Train(int reward, double[] input)
    {  //list of //list of neurons
        List<List<double>> outputs = new List<List<double>>();//wtf
        outputs.Add(new List<double>(input));
        double[] acction = [];
        for (int i = 1; i < neurons.Length; i++)// for every layer 
        {
            //double[] outputs = new double[neurons[i]];//list of outputs this layer //needs to be inputs next layer     

            for (int j = 0; j < neurons[i]; j++)//for every neuron in layer
            {
                double output = 0;
                for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                {
                    output += outputs[i - 1][k] * weights[i - 1][j, k];//* somthing to make betwen 0-1
                }
                outputs[i][j] = 1 / (1 + Math.Exp(output));//neuron value this layer
            }
        }
        acction = outputs[neurons.Length - 1][..neurons[neurons.Length - 1]].ToArray();

        double largest = 0;
        int thingie = 0;

        for (int i = 0; i < acction.Length; i++)
        {
            if (acction[i] > largest)
            {
                largest = acction[i];
                thingie = i;
            }
        }
        // check wich weigts conect to thingie
        //increse them proprtional to how much they helped

        List<List<double>> outputs2 = new List<List<double>>();//wtf
        outputs2[neurons.Length - 1] = new List<double>(outputs[neurons.Length - 1]);
        for (int i = neurons.Length - 1; i < 1; i++)// for every layer //go backwards
        {
            //double[] outputs = new double[neurons[i]];//list of outputs this layer //needs to be inputs next layer     
            if (i == neurons.Length - 1)
            {
                    //double delta = 0;//idk what delta is
                    for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                    {
                        //change weight value
                        weights[i - 1][thingie, k] *= 1+reward/100;
                        //delta = outputs2[i - 1][k] * weights[i - 1][thingie, k];//* somthing to make betwen 0-1
                    }
                    //outputs2[i][thingie] = 1 / (1 + Math.Exp(delta));//neuron value this layer
            }
            else
            {
                for (int j = 0; j < neurons[i]; j++)//for every neuron in layer
                {
                    double output = 0;
                    for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                    {
                        weights[i - 1][j, k] *= 1+reward/100;
                        //output += outputs2[i - 1][k] * weights[i - 1][j, k];//* somthing to make betwen 0-1
                    }
                    //outputs2[i][j] = 1 / (1 + Math.Exp(output));//neuron value this layer
                }
            }
        }
    }
}
