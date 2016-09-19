using System;

using UniversalEditor.ObjectModels.Markup;

using Alyx.Linguistics;
using Alyx.Linguistics.Pronunciation;

using Alyx.Scripting;

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

		private PronunciationInformation mvarPronunciation = new PronunciationInformation ();
		public PronunciationInformation Pronunciation { get { return mvarPronunciation; } }

		private Script.ScriptCollection mvarScripts = new Script.ScriptCollection ();
		public Script.ScriptCollection Scripts { get { return mvarScripts; } }

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
			mvarInstances.Clear ();
			mvarLanguages.Clear ();
			mvarPronunciation.Clear ();
			mvarScripts.Clear ();
		}

		private void LoadV1AlyxTag(MarkupTagElement tagAlyx)
		{
			// Pronunciation common to all instances
			MarkupTagElement tagPronunciation = (tagAlyx.Elements ["Pronunciation"] as MarkupTagElement);
			if (tagPronunciation != null) {
				mvarPronunciation = PronunciationInformation.FromMarkup (tagPronunciation);

				foreach (PronunciationKey key in mvarPronunciation.PronunciationKeys) {
					foreach (PronunciationKeySyllable syllable in key.Syllables) {
						if (syllable.SyllableID != Guid.Empty) {
							syllable.Syllable = mvarPronunciation.Syllables [syllable.SyllableID];
						}
					}
				}
			}
			
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
		private void LoadV2AlyxTag(MarkupTagElement tagConfiguration)
		{
			if (tagConfiguration == null)
				return;
			if (!(tagConfiguration.XMLSchema == Schemas.Alyx && tagConfiguration.Name == "configuration"))
				return;

			MarkupTagElement tagScripts = (tagConfiguration.FindElementUsingSchema (Schemas.Alyx, "scripts") as MarkupTagElement);
			if (tagScripts != null) {
				foreach (MarkupElement elScript in tagScripts.Elements) {
					MarkupTagElement tagScript = (elScript as MarkupTagElement);
					if (tagScript == null)
						continue;
					if (!(tagScript.XMLSchema == Schemas.Alyx && tagScript.Name == "script"))
						continue;

					MarkupAttribute attID = tagScript.Attributes ["id"];
					if (attID == null)
						continue;
					
					Guid scriptId = new Guid (attID.Value);
					Script script = new Script (scriptId);

					MarkupAttribute attInheritsScriptID = tagScript.Attributes ["inheritsScriptId"];
					if (attInheritsScriptID != null) {
						script.InheritsScriptID = new Guid (attInheritsScriptID.Value);
					}

					MarkupTagElement tagParameters = (tagScript.FindElementUsingSchema (Schemas.Alyx, "parameters") as MarkupTagElement);
					if (tagParameters != null) {
						foreach (MarkupElement elParameter in tagParameters.Elements) {
							MarkupTagElement tagParameter = (elParameter as MarkupTagElement);
							if (tagParameter == null)
								continue;
							if (!(tagParameter.XMLSchema == Schemas.Alyx && tagParameter.Name == "parameter"))
								continue;

							MarkupAttribute attParameterID = tagParameter.Attributes ["id"];
							if (attParameterID == null)
								continue;

							ScriptParameter parm = new ScriptParameter ();
							parm.ID = new Guid (attParameterID.Value);

							script.Parameters.Add (parm);
						}
					}

					mvarScripts.Add (script);
				}
			}
		}


		private void ReloadConfigurationMarkup()
		{
			Clear ();

			foreach (MarkupElement el in Markup.Elements) {
				MarkupTagElement tag = (el as MarkupTagElement);
				if (tag == null)
					continue;
				if (tag.FullName == "Alyx") {
					LoadV1AlyxTag (tag);
				} else if (tag.XMLSchema == "urn:net.alcetech.schemas.Alyx" && tag.Name == "configuration") {
					LoadV2AlyxTag (tag);
				}
			}
		}
	}
}

