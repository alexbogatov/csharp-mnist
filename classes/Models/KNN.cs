using System;
using System.Collections.Generic;

public class KNN
{
	public int K { get; private set; }

	List<Data> Neighbours;
	public List<Data> TrainingData { get; set; }
	public List<Data> TestData { get; set; }
	public List<Data> ValidationData { get; set; }

	public KNN(int k)
	{
		this.K = k;
	}
	public KNN() { }
	~KNN() { }

	public void SetK(int k)
	{
		this.K = k;
	}

	void FindKNearest(Data query_point)
	{
		Neighbours = new List<Data>();

		double min = double.MaxValue;
		double min_prev = min;

		int index = 0;

		for (int i = 0; i < this.K; i++)
		{
			if (i == 0)
			{
				for (int j = 0; j < TrainingData.Count; j++)
				{
					double distance = CalculateDistance(query_point, TrainingData[j]);

					TrainingData[j].Distance = distance;

					if (distance < min)
					{
						min = distance;
						index = j;
					}
				}
			}
			else
			{
				for (int j = 0; j < TrainingData.Count; j++)
				{
					double distance = TrainingData[i].Distance;

					if ((distance > min_prev) && (distance < min))
					{
						min = distance;
						index = j;
					}
				}
			}
			Neighbours.Add(TrainingData[index]);

			min_prev = min;

			min = double.MaxValue;
		}
	}

	int Predict()
	{
		var class_frequencies = new Dictionary<int, int>();;

		foreach(var neighbour in Neighbours)
		{
			if (class_frequencies.ContainsKey(neighbour.Label))
			{
				class_frequencies[neighbour.Label]++;
			}
			else
			{
				class_frequencies[neighbour.Label] = 1;
			}
		}

		int best = 0;
		int max = 0;

		foreach(var pair in class_frequencies)
		{
			if (pair.Value > max)
			{
				max = pair.Value;
				best = pair.Key;
			}
		}

		Neighbours.Clear();

		return best;
	}

	double CalculateDistance(Data query_point, Data input)
	{
		if (query_point.FeatureVector.Length != input.FeatureVector.Length)
		{
			throw new Exception("ERROR: Vector Size Mismatch");
		}

		double distance = 0;

		for (int i = 0; i < query_point.FeatureVector.Length; i++)
		{
			distance += Math.Pow(query_point.FeatureVector[i] - input.FeatureVector[i], 2);
		}
		distance = Math.Sqrt(distance);

		return distance;
	}

	public double ValidatePerformance()
	{
		double current_performance = 0;
		int count = 0;
		int data_index = 0;

		foreach (var query_point in ValidationData)
		{
			FindKNearest(query_point);

			int prediction = Predict();

			if (prediction == query_point.Label)
			{
				count++;
			}
			data_index++;

			c.w("[{0} -> {1}] Performance = {2:P3}% ({3:N0} / {4:N0})\n", prediction, query_point.Label, (double)count / (double)data_index, count, data_index);
		}

		current_performance = (double)count / (double)ValidationData.Count;

		c.w("Validation Performance (for K = {0}): {1:P3}\n", K, current_performance);

		return current_performance;
	}
	public double TestPerformance()
	{
		int count = 0;

		foreach (var query_point in TestData)
		{
			FindKNearest(query_point);

			int prediciton = Predict();

			if (prediciton == query_point.Label)
			{
				count++;
			}
		}

		double current_performance = (double)count / (double)TestData.Count;

		return current_performance;
	}
}