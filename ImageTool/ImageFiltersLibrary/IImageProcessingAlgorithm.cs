﻿using ImageFiltersLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace ImageFiltersLibrary
{
    public interface IImageProcessingAlgorithm
    {
        /// <summary>
        /// Loads the input image file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        TransformedBitmap LoadInputImage(string filename, out string errorMessage);

        /// <summary>
        /// Sets the current effect
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        bool SetEffects(Effects effect);

        /// <summary>
        /// Get options for particular effect
        /// </summary>
        /// <param name="effect"></param>
        /// <returns></returns>
        IList<AlgorithmOption> GetOptions(Effects effect);

        /// <summary>
        /// Apply the algorithm selected
        /// </summary>
        /// <param name="algorithmParameter"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        BitmapSource ApplyEffect(List<AlgorithmParameter> algorithmParameter, out string errorMessage);

        /// <summary>
        /// Gets the the image in original size
        /// </summary>
        /// <param name="algorithmParameter"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        BitmapSource ApplyEffectOnOriginalDimensions(List<AlgorithmParameter> algorithmParameter, out string errorMessage);
    }
}
