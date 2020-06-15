using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;

public class Data
{
    public byte[] FeatureVector { get; set; }
    public byte Label { get; set; } = 0;
    public double Distance { get; set; }

	public void SaveImage(/*byte[] bytes, int width, int height, */string filename)
	{
        int width = 28;
        int height = 28;

		var bitmap = new Bitmap(width, height);

		for (var y = 0; y < height; y++)
		{
			for (var x = 0; x < width; x++)
			{
				byte b = FeatureVector[y * width + x];

				var color = Color.FromArgb(b, b, b);

				bitmap.SetPixel(x, y, color);
			}
		}

		bitmap.Save(filename, ImageFormat.Png);
		bitmap.Dispose();
	}

}

