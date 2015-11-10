using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Thought
{
	public abstract class IdeaRepresentation
	{
		public class IdeaRepresentationCollection
			: System.Collections.ObjectModel.Collection<IdeaRepresentation>
		{

		}

	}
}
