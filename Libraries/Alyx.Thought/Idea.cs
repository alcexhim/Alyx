using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alyx.Thought
{
	public class Idea
	{
		public class IdeaCollection
			: System.Collections.ObjectModel.Collection<Idea>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } }

		private IdeaRepresentation.IdeaRepresentationCollection mvarRepresentations = new IdeaRepresentation.IdeaRepresentationCollection();
		public IdeaRepresentation.IdeaRepresentationCollection Representations { get { return mvarRepresentations; } }

		private Idea.IdeaCollection mvarRelatedIdeas = new Idea.IdeaCollection();
		public Idea.IdeaCollection RelatedIdeas { get { return mvarRelatedIdeas; } }

		private Emotion.EmotionCollection mvarEmotions = new Emotion.EmotionCollection();
		/// <summary>
		/// A collection of <see cref="Emotion" />s associated with this <see cref="Idea" />.
		/// </summary>
		public Emotion.EmotionCollection Emotions { get { return mvarEmotions; } }

		public Idea(Guid id, IdeaRepresentation[] representations = null, Idea[] relatedIdeas = null)
		{
			mvarID = id;
			if (representations != null)
			{
				foreach (IdeaRepresentation rep in representations)
				{
					mvarRepresentations.Add(rep);
				}
			}
			if (relatedIdeas != null)
			{
				foreach (Idea idea in relatedIdeas)
				{
					mvarRelatedIdeas.Add(idea);
				}
			}
		}

		public override string ToString()
		{
			if (mvarRepresentations.Count > 0)
			{
				return mvarRepresentations[0].ToString();
			}
			return base.ToString();
		}
	}
}
