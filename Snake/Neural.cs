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
                layerWeights[i, j] = Random.Shared.NextDouble() * 2 - 1;//Random.Shared.NextDouble();
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
                    output += outputs[k] * weights[i - 1][j, k];
                }
                outputs[j] = 1 / (1 + Math.Exp(-output));//sigmoid or something
            }
        }
        acction = outputs[..neurons[neurons.Length - 1]].ToArray();

        double largest = outputs[0];
        int thingie = 0;
        //int[][] options = [[0, 1], [1, 0], [-1, 0], [0, -1]];
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
    //reward/pusnish (maybe future actions)
    // to train// need to check wich weights had the most inpact
    // check next layer wich weigts had most inpact on previus

    public void Train(int reward, double[] input, int taken)
    {
        //List<double> outputs = new List<double>();
        double[] outputs = input;
        List<double[,]> weightsOutput = new List<double[,]>();
        List<double[]> remember = new List<double[]>();
        //double[] acction = new double[neurons.Length - 1];
        for (int i = 1; i < neurons.Length; i++)// for every layer
        {
            //double[] outputs = new double[neurons[i]];//list of outputs this layer //needs to be inputs next layer     
            //if (i == 1) outputs = input.ToArray();//new List<double>(input);

            List<double> ahahah = new List<double>();
            double[,] tttt = new double[neurons[i], neurons[i - 1]];
            for (int j = 0; j < neurons[i]; j++)//for every neuron in layer
            {
                outputs = new double[neurons[i - 1]];
                double output = 0;
                for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                {
                    double weigh = outputs[k] * weights[i - 1][j, k];
                    tttt[j, k] = weigh;
                    output += weigh;//* somthing to make betwen 0-1

                }
                ahahah.Add(output);//remeber.Add(output);// should be value of action taken neuron, and all other neurons, maybe
                outputs[j] = 1 / (1 + Math.Exp(-output));//sigmoid or something to make betwen 0-1
            }
            remember.Add(ahahah.ToArray());
            weightsOutput.Add(tttt);
        }
        remember.Reverse();// since counting backwards later

        // acction = outputs[..neurons[neurons.Length - 1]].ToArray();

        // check wich weigts conect to thingie
        //increse them proprtional to how much they helped

        double stuff = weightsOutput[0].Cast<double>().Sum();
        double stuff2 = weightsOutput[1].Cast<double>().Sum();

        double[] deltas = new double[neurons[neurons.Length - 1]];
        for (int i = neurons.Length - 1; i >= 1; i--)// for every layer //go backwards
        {
            //double[] outputs = new double[neurons[i]];//list of outputs this layer //needs to be inputs next layer     
            if (i == neurons.Length - 1)
            {
                for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                {
                    // hwo big part weightsOutput is of remeber and // wtf is remember
                    double procent;
                    if (remember[i - 1][taken]!=0)// cant divide by zero
                    {
                        procent = weightsOutput[i - 1][taken, k] / remember[i - 1][taken];
                    }
                    else
                    {
                        procent = weightsOutput[i - 1][taken, k] / double.MinValue;
                    }
                    //change weight value
                    if (reward < 0)
                    {

                    }
                    double change = reward * procent * (i / 4);
                    weights[i - 1][taken, k] += change;
                    //delta = outputs2[i - 1][k] * weights[i - 1][thingie, k];//* somthing to make betwen 0-1
                }
                //outputs2[i][thingie] = 1 / (1 + Math.Exp(delta));//neuron value this layer
            }
            else
            {
                for (int j = 0; j < neurons[i]; j++)//for every neuron in layer
                {
                    //double output = 0;
                    for (int k = 0; k < neurons[i - 1]; k++)// for every neuron in last layer
                    {
                        //double procent = weightsOutput[i - 1][j, k] / remember[i][j];
                        double procent;
                        if (remember[i - 1][taken]!=0)// cant divide by zero
                        {
                            procent = weightsOutput[i - 1][taken, k] / remember[i - 1][taken];
                        }
                        else
                        {
                            procent = weightsOutput[i - 1][taken, k] / double.MinValue;
                        }
                        //change weight value
                        double change = (reward * procent) * Math.Pow(2, Math.Abs(neurons.Length - i));
                        weights[i - 1][j, k] += change;
                        //output += outputs2[i - 1][k] * weights[i - 1][j, k];//* somthing to make betwen 0-1
                    }
                    //outputs2[i][j] = 1 / (1 + Math.Exp(output));//neuron value this layer
                }
            }
        }
    }
}
