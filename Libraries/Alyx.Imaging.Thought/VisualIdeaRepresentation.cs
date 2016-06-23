using System;
using System.Collections.Generic;

using Alyx.Thought;

using UniversalEditor;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace Alyx.Imaging.Thought
{
	public class VisualIdeaRepresentation : IdeaRepresentation
	{

		private PictureObjectModel mvarPicture = null;
		public PictureObjectModel Picture { get { return mvarPicture; } set { mvarPicture = value; } }

		private int? mvarAverageHue = null;
		public int AverageHue
		{
			get 
			{
				if (mvarAverageHue == null) {
					// go through each pixel and calculate the average hue
					Dictionary<int, int> hues = new Dictionary<int, int> ();

					// only go through certain pixels to get a "feel" for the image without using too much CPU
					int xstep = (int)((double)mvarPicture.Width / 4);
					int ystep = (int)((double)mvarPicture.Height / 4);

					for (int x = 0; x < mvarPicture.Width; x += xstep) {
						for (int y = 0; y < mvarPicture.Height; y += ystep) {
							Color color = mvarPicture.GetPixel (x, y);
							if (!hues.ContainsKey (color.HueInt32)) {
								hues.Add (color.HueInt32, 0);
							}
							hues [color.HueInt32] ++;
						}
					}
				}
				return mvarAverageHue.Value;
			}
		}

	}
}

