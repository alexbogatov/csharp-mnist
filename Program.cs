using System;

namespace csharp_mnist
{
    class Program
    {
        static void Main(string[] args)
        {
            var dh = new DataHandler();

            dh.ReadFeatureVector(@"D:\Projects\NN_CPP_Handwriting\ML_Nist\ML_Nist\Data\train-images.idx3-ubyte");
            dh.ReadFeatureLabels(@"D:\Projects\NN_CPP_Handwriting\ML_Nist\ML_Nist\Data\train-labels.idx1-ubyte");

            // UInt32 forward = 0x1A2B3C4D;
            // UInt32 reverse = DataHandler.ConvertToLittleEndean(forward);

            // Console.WriteLine($"forward={forward:X}");
            // Console.WriteLine($"reverse={reverse:X}");

            // Console.WriteLine("Hello World!");
        }
    }
}
