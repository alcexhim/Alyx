using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public class ISubjectCollection
		: System.Collections.ObjectModel.Collection<ISubject>
	{

	}

	/// <summary>
	/// Marker interface to represent that a particular object can be used as a subject.
	/// </summary>
	public interface ISubject
	{
	}
}
