﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Stitching;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace StitchingEmguCVConsoleApp
{
    class Program
    {



        static void Main(string[] args)
        {
            Console.WriteLine("Working through EMGU.CV Stitching Example");

            //Change this file path to the path where the images you want to stich are located
            string filesPath = @"E:\__WORKING_DATA_FIRSTROW"; //Environment.CurrentDirectory;// System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString() + @"/Images/";
            //string filetest = @"E:\__WORKING_DATA_FIRSTROW\00001.jpg";
            Console.WriteLine(filesPath);




            //Read in the image and store it as a mat object
            //Mat imgnow = CvInvoke.Imread(filetest, ImreadModes.AnyColor);
            //CvInvoke.Resize(imgnow, imgnow, new System.Drawing.Size(1000, 750), 0, 0, Inter.Linear);
            //CvInvoke.Imshow("test window", imgnow); //Show the image
            //CvInvoke.SetWindowProperty("test window", WindowPropertyFlags.FullScreen, 1f);
            //CvInvoke.WaitKey(0);  //Wait for the key pressing event
            //CvInvoke.DestroyWindow("test window"); //Destroy the window if key is pressed

            List<Mat> images = GetImages(filesPath);

            Mat output = StichImages(images);
            //CvInvoke.Resize(output, output, new System.Drawing.Size(1000, 750), 0, 0, Inter.Linear);
            CvInvoke.Imshow("stitch window", output); //Show the image
            //CvInvoke.SetWindowProperty("test window", WindowPropertyFlags.FullScreen, 1f);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow("stitch window"); //Destroy the window if key is pressed



        }

        /// <summary>
        /// Pass a path to a folder of images and read in all those images and store them in a list
        /// </summary>
        /// <param name="path">The path to where the folder where the images are kept</param>
        /// <returns>A list of Mat objects</returns>
        public static List<Mat> GetImages(string path)
        {
            //Store all paths to each individual file in the directory in an array
            string[] files = Directory.GetFiles(path);

            //Declare a list of Mat to store all images loaded in
            List<Mat> outputList = new List<Mat>();

            //For each item in the files array, read in the image and store it in the list
            foreach (var image in files)
            {
                //Read in the image and store it as a mat object
                Mat img = CvInvoke.Imread(image, ImreadModes.AnyColor);

                //Add the mat object to the list
                outputList.Add(img);
            }

            //Return the list of read in mat objects
            return outputList;
        }

        /// <summary>
        /// Stitch images together
        /// </summary>
        /// <param name="images">The list of images to stitch</param>
        /// <returns>A final stitched image</returns>
        public static Mat StichImages(List<Mat> images)
        {
            //Declare the Mat object that will store the final output
            Mat output = new Mat();

            //Declare a vector to store all images from the list
            VectorOfMat matVector = new VectorOfMat();

            //Push all images in the list into a vector
            foreach (Mat img in images)
            {
                matVector.Push(img);
            }

            //Declare a new stitcher
            Stitcher stitcher = new Stitcher(Stitcher.Mode.Scans);

            //Declare the type of detector that will be used to detect keypoints
            Brisk detector = new Brisk();

            //Here are some other detectors that you can try
            //ORBDetector detector = new ORBDetector();
            //KAZE detector = new KAZE();
            //AKAZE detector = new AKAZE();

            //Set the stitcher class to use the specified detector declared above
            stitcher.SetFeaturesFinder(detector);


        
            //Stitch the images together
            stitcher.Stitch(matVector, output);

            //Return the final stiched image
            return output;
        }
    }
}
