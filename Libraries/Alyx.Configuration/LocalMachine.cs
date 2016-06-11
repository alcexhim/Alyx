using System;

using ApplicationFramework;

using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using Alyx.Linguistics;

namespace Alyx.Configuration
{
	public class LocalMachine : Machine
	{
		private static XMLDataFormat xdf = new XMLDataFormat ();

		protected override void LoadInternal ()
		{
			string[] basePaths = Application.GetBasePaths ();

			foreach (string basePath in basePaths)
			{
				if (!System.IO.Directory.Exists (basePath))
					continue;

				XMLDataFormat xdf = new XMLDataFormat ();
				string[] dirnames = System.IO.Directory.GetDirectories(basePath);
				foreach (string dirname in dirnames)
				{
					string[] xmlfiles = System.IO.Directory.GetFiles(dirname, "*.alyxml", System.IO.SearchOption.AllDirectories);
					foreach (string xmlfile in xmlfiles)
					{
						MarkupObjectModel xmlconf = new MarkupObjectModel();

						Document.Load(xmlconf, xdf, new FileAccessor(xmlfile));
						xmlconf.CopyTo(Markup);
					}
				}

			}
		}
	}
}

