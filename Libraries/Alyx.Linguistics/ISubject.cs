using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class ISubjectCollection
		: System.Collections.ObjectModel.Collection<ISubject>
	{

		public void AddRange(ISubject[] items)
		{
			foreach (ISubject item in items)
			{
				Add(item);
			}
		}
	}

	/// <summary>
	/// Marker interface to represent that a particular object can be used as a subject.
	/// </summary>
	public interface ISubject
	{
	}
}
