using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
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
		byte[] bytes = File.ReadAllBytes(path);

		c.w("Reading input file header\tOK\r\n");

		uint [] header = new UInt32[4];
		int header_size = header.Length * sizeof(uint);

		for (int i = 0; i < 4; i++)
		{
			header[i] = ConvertToLittleEndean(BitConverter.ToUInt32(bytes, i * 4));
		}

		int n_images = (int)header[1];

		int image_width = (int)header[2];
		int image_height = (int)header[3];

		int image_size = image_width * image_height;

		c.w($"header.n_images\t\t\t{n_images:N0}\r\n");
		c.w($"header.image_width\t\t{image_width} px\r\n");
		c.w($"header.image_height\t\t{image_height} px\r\n");
		c.w($"header.image_size\t\t{image_size} b\r\n");

		for (int i = 0; i < n_images; i++)
		{
			var index = header_size + (i * image_size);

			var image_bytes = new byte[image_size];

			Buffer.BlockCopy(bytes, index, image_bytes, 0, image_size);

			DataArray.Add(new Data()
			{
				FeatureVector = image_bytes
			});

			SaveImage(image_bytes, image_width, image_height, $"D:\\Projects\\Git\\csharp-mnist\\csharp-mnist\\images\\{i}.png");
		}

		c.w($"\r\nSuccessfully read {DataArray.Count:N0} vectors.\r\n\r\n");
	}

	public void SaveImage(byte[] bytes, int width, int height, string filename)
	{
		var bitmap = new Bitmap(width, height);

		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				byte b = bytes[y * width + x];

				var color = Color.FromArgb(b, b, b);

				bitmap.SetPixel(x, y, color);
			}
		}

		bitmap.Save(filename, ImageFormat.Png);
		bitmap.Dispose();
	}

	public void ReadFeatureLabels(string path)
	{
		byte[] bytes = File.ReadAllBytes(path);

		c.w("Reading label file header\tOK\r\n");

		uint [] header = new UInt32[2];
		int header_size = header.Length * sizeof(uint);

	}

	public static UInt32 ConvertToLittleEndean(UInt32 value)
	{
		return ((UInt32)(value & 0x000000FF)) << 24 | ((UInt32)(value & 0x0000FF00)) << 8 | ((UInt32)(value & 0x00FF0000)) >> 8 |  ((UInt32)(value & 0xFF000000)) >> 24;
	}

}