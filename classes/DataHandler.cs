using System;
using System.Collections.Generic;

public class DataHandler
{
	public List<Data> DataArray { get; private set; } = new List<Data>();
	public List<Data> TrainingData { get; private set; } = new List<Data>();
	public List<Data> TestData { get; private set;} = new List<Data>();
	public List<Data> ValidationData { get; private set;} = new List<Data>();

	public int NumClasses { get; private set;} = 0;
	public int FeatureVectorSize { get; private set;} = 0;

	// Dictionary<byte, uint> class_map;

	const double TRAINING_SET_PERCENT = 0.75;
	const double TEST_SET_PERCENT = 0.20;
	const double VALIDATION_SET_PERCENT = 0.05;

	public void ReadFeatureVector(string path)
	{
		UInt32 [] header = new UInt32[4];



	}

	public static UInt32 ConvertToLittleEndean(UInt32 value)
	{
		return ((UInt32)(value & 0x000000FF)) << 24 | ((UInt32)(value & 0x0000FF00)) << 8 | ((UInt32)(value & 0x00FF0000)) >> 8 |  ((UInt32)(value & 0xFF000000)) >> 24;
	}

}