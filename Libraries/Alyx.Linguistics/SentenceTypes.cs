using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Linguistics
{
	public static class SentenceTypes
	{
		private static SentenceType mvarDeclarative = new SentenceType(new Guid("{29F7837F-3865-4716-8D87-1E11FD57A7E8}"));
		/// <summary>
		/// Expresses a statement.
		/// </summary>
		public static SentenceType Declarative { get { return mvarDeclarative; } }
		private static SentenceType mvarImperative = new SentenceType(new Guid("{1B13AE97-CF7B-4379-9529-F101A232A79C}"));
		/// <summary>
		/// Expresses a command, telling someone to do something. If done strongly, may be
		/// considered both imperative and exclamatory.
		/// </summary>
		public static SentenceType Imperative { get { return mvarImperative; } }
		private static SentenceType mvarInterrogative = new SentenceType(new Guid("{F9D08DF1-F64C-49F1-A115-180A6C6117B1}"));
		/// <summary>
		/// Expresses a question, commonly used to request information.
		/// </summary>
		public static SentenceType Interrogative { get { return mvarInterrogative; } }
		private static SentenceType mvarExclamatory = new SentenceType(new Guid("{4CB83D3F-CD90-46E4-96F5-60A3CDF077B1}"));
		/// <summary>
		/// Expresses an exclamation, generally a more emphatic form of statement expressing
		/// emotion.
		/// </summary>
		public static SentenceType Exclamatory { get { return mvarExclamatory; } }
	}
}
