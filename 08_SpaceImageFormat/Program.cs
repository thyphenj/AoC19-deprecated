using System.IO;
using System.Linq;

namespace _08_SpaceImageFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            (int width, int height, string programText) theData;

            //theData = (3, 2, "123456789012");
            //theData = (2, 2, "0222112222120000");
            theData = (25, 6, File.ReadAllText("data.txt"));

            int layerSize = theData.width * theData.height;
            int numLayers = theData.programText.Length / layerSize;

            string[] layers = new string[numLayers];
            char[] image = "".PadLeft(layerSize, '2').ToCharArray();

            for (int i = 0; i < numLayers; i++)
            {
                layers[i] = theData.programText.Substring(i * layerSize, layerSize);
            }
            foreach (var layer in layers)
            {
                int a = layer.Count<char>(a => a == '0');
                int b = layer.Count<char>(a => a == '1');
                int c = layer.Count<char>(a => a == '2');

                System.Console.WriteLine($"{layer}  {a,3} {b,3} {c,3}");

                for (int i = 0; i < layerSize; i++)
                {
                    if (image[i] == '2')
                        image[i] = layer[i];
                }
            }
            string imageString = new string(image);

            System.Console.WriteLine($"\n{imageString}\n\n");
            imageString = imageString.Replace('0', ' ').Replace('1', 'X');
            for (int i = 0; i < theData.height; i++)
            {
                System.Console.WriteLine(imageString.Substring(i * theData.width, theData.width));
            }
        }
    }
}
