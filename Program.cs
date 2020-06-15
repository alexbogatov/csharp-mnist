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

            dh.SplitData();
            dh.CountClasses();

            KNN knn = new KNN()
            {
                TrainingData = dh.TrainingData,
                TestData = dh.TestData,
                ValidationData = dh.ValidationData
            };

            double performance = 0.0;
            double best_performance = 0.0;

            int best_k = 1;

            for (int k = 1; k <= 4; k++)
            {
                knn.SetK(k);
                performance = knn.ValidatePerformance();

                if (k == 1)
                {
                    best_performance = performance;
                }
                else
                {
                    if (performance > best_performance)
                    {
                        best_performance = performance;
                        best_k = k;
                    }
                }
            }

            knn.SetK(best_k);
            knn.TestPerformance();

            // uint forward = 0x1A2B3C4D;
            // uint reverse = DataHandler.ConvertToLittleEndean(forward);

            // Console.WriteLine($"forward={forward:X}");
            // Console.WriteLine($"reverse={reverse:X}");

            // Console.WriteLine("Hello World!");
        }
    }
}
