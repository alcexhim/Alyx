//
//  EspeakEngine.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
namespace Alyx.Speech.Synthesis.Engines.Espeak
{
	public class EspeakEngine : ExternalSoftwareSynthesisEngine
	{
		protected override string ApplicationPath => "espeak";

		private static SynthesisEngineReference _ser = null;
		public override SynthesisEngineReference MakeReference()
		{
			if (_ser == null)
			{
				_ser = new SynthesisEngineReference(typeof(EspeakEngine));
			}
			return _ser;
		}

		private Voice _Default = null;
		public EspeakEngine()
		{
			_Default = new Voice("Default Voice");
			Voice = _Default;
		}

		public override void SetVoiceInternal(Voice voice)
		{
			// do nothing, Espeak only supports one voice
		}

		protected override Voice[] GetVoicesInternal()
		{
			return new Voice[] { _Default };
		}
	}
}
