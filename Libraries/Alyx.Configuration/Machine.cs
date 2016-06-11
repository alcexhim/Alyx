using System;

using UniversalEditor.ObjectModels.Markup;

using Alyx.Linguistics;

namespace Alyx.Configuration
{
	public abstract class Machine
	{
		private MarkupObjectModel mvarMarkup = new MarkupObjectModel ();
		public MarkupObjectModel Markup { get { return mvarMarkup; } }

		private Instance.InstanceCollection mvarInstances = new Instance.InstanceCollection ();
		public Instance.InstanceCollection Instances { get { return mvarInstances; } }

		private Language.LanguageCollection mvarLanguages = new Language.LanguageCollection ();
		public Language.LanguageCollection Languages { get { return mvarLanguages; } }

		protected abstract void LoadInternal();
		public void Load()
		{
			LoadInternal ();

			ReloadConfigurationMarkup ();
		}

		/// <summary>
		/// Clears the configuration for this machine.
		/// </summary>
		public void Clear()
		{
			mvarLanguages.Clear ();
		}

		private void ReloadConfigurationMarkup()
		{
			Clear ();

			MarkupTagElement tagAlyx = (Markup.Elements["Alyx"] as MarkupTagElement);
			if (tagAlyx != null)
			{
				// Languages, common to all instances
				MarkupTagElement tagLanguages = (tagAlyx.Elements["Languages"] as MarkupTagElement);
				if (tagLanguages != null)
				{
					foreach (MarkupElement elLanguage in tagLanguages.Elements)
					{
						Language language = Language.FromMarkup (elLanguage as MarkupTagElement);
						if (language != null)
							mvarLanguages.Add (language);
					}
				}

				MarkupTagElement tagInstances = (tagAlyx.Elements ["Instances"] as MarkupTagElement);
				if (tagInstances != null)
				{
					foreach (MarkupElement elInstance in tagInstances.Elements)
					{
						Instance inst = Instance.FromMarkup (elInstance as MarkupTagElement);
						foreach (Language lang in mvarLanguages) {
							inst.Languages.Add (lang);
						}
						if (inst != null)
							mvarInstances.Add (inst);
					}
				}
			}
		}
	}
}

