﻿using System.Diagnostics;

namespace AEAQuiz.Classes
{
    public class ImageService
    {
        private Random _random = new Random();

        public ImageSource GetRandomImageForCategory(int category)
        {

            int imageNumber = _random.Next(1, 4);

            string imagePath = $"cat_{category}_{imageNumber}.png";
            Debug.WriteLine(imagePath);
            return ImageSource.FromFile(imagePath);
        }
    }

}
