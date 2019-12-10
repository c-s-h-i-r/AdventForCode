using System.Collections.Generic;
using System.Linq;

namespace AdventForCode
{
    public class Day8
    {
        //        --- Day 8: Space Image Format ---
        //The Elves' spirits are lifted when they realize you have an opportunity to reboot one of their Mars rovers, 
        //and so they are curious if you would spend a brief sojourn on Mars. You land your ship near the rover.
        //When you reach the rover, you discover that it's already in the process of rebooting! 
        //It's just waiting for someone to enter a BIOS password.
        //The Elf responsible for the rover takes a picture of the password (your puzzle input) and sends it to you via the Digital Sending Network.
        //Unfortunately, images sent via the Digital Sending Network aren't encoded with any normal encoding; 
        //instead, they're encoded in a special Space Image Format.None of the Elves seem to remember why this is the case. 
        //They send you the instructions to decode it.
        //Images are sent as a series of digits that each represent the color of a single pixel.
        //The digits fill each row of the image left-to-right, then move downward to the next row,
        //filling rows top-to-bottom until every pixel of the image is filled.
        //Each image actually consists of a series of identically-sized layers that are filled in this way.
        //So, the first digit corresponds to the top-left pixel of the first layer,
        //the second digit corresponds to the pixel to the right of that on the same layer,
        //and so on until the last digit, which corresponds to the bottom-right pixel of the last layer.

        //For example, given an image 3 pixels wide and 2 pixels tall, the image data 123456789012 corresponds to the following image layers:
        //Layer 1: 123
        //         456

        //Layer 2: 789
        //         012

        private readonly string filePath;
        private readonly int width;
        private readonly int height;
        private int LayerSize => this.width * this.height;

        public Day8(string filePath, int pixelWidth, int pixelHeight)
        {
            this.filePath = filePath;
            this.width = pixelWidth;
            this.height = pixelHeight;
        }

        public int RunChallengePart1()
        {
            var layers = SeparateIntoLayers(Program.ReadInput(this.filePath, "\n")[0]);

            //To make sure the image wasn't corrupted during transmission, 
            //the Elves would like you to find the layer that contains the fewest 0 digits. 
            var minDigits = layers.Select(x => new
            {
                data = x,
                zeroCount = x.Count(x => x == '0')
            })
            .OrderBy(x => x.zeroCount).FirstOrDefault();

            //On that layer, what is the number of 1 digits multiplied by the number of 2 digits?
            var oneDigits = minDigits.data.Count(x => x == '1');
            var twoDigits = minDigits.data.Count(x => x == '2');
            return oneDigits * twoDigits;
        }
        public string RunChallengePart2()
        {
            //What message is produced after decoding your image?
            return ConvertToImage(SeparateIntoLayers(Program.ReadInput(this.filePath, "\n")[0]));
        }

        //Break up the string into chunks of widthxheight chunks
        public List<string> SeparateIntoLayers(string message)
        {
            var layers = new List<string>();
            for (var i = 0; i < message.Length; i += LayerSize)
            {
                layers.Add(message.Substring(i, LayerSize));
            }
            return layers;
        }

        private enum PixelColour
        {
            Black = 0,
            White = 1,
            Transparent = 2
        }
        public string ConvertToImage(List<string> layers)
        {
            //The image is rendered by stacking the layers and aligning the pixels with the same positions in each layer.
            //The digits indicate the color of the corresponding pixel: 0 is black, 1 is white, and 2 is transparent.
            //The layers are rendered with the first layer in front and the last layer in back.
            //So, if a given position has a transparent pixel in the first and second layers, 
            //a black pixel in the third layer,
            //and a white pixel in the fourth layer, 
            //the final image would have a black pixel at that position.
            var finalMessage = layers[0].ToList();

            var otherLayers = layers.Skip(1);

            for (var i = 0; i < LayerSize; i++)
            {
                foreach (var layer in otherLayers)
                {
                    if ((PixelColour)int.Parse(finalMessage[i].ToString()) != PixelColour.Transparent)
                    {
                        break;
                    }
                    // If we can see through to the next layer, continue
                    if ((PixelColour)int.Parse(layer[i].ToString()) != PixelColour.Transparent)
                    {
                        finalMessage[i] = layer[i];
                    }
                }
            }

            return string.Join("", finalMessage.Where(x => x != 2));
        }
    }
}
