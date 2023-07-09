//
//  ClockMindScript.cs
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
namespace Alyx.Thought.MindScripts
{
	/// <summary>
	/// Runs a <see cref="MindScript" /> which responds to year, month, day, hour, minute, and second changes.
	/// </summary>
	public abstract class ClockMindScript : MindScript
	{
		protected virtual void OnYearChanged(EventArgs e)
		{
		}
		protected virtual void OnMonthChanged(EventArgs e)
		{
		}
		protected virtual void OnDayChanged(EventArgs e)
		{
		}
		protected virtual void OnHourChanged(EventArgs e)
		{
		}
		protected virtual void OnMinuteChanged(EventArgs e)
		{
		}

		protected virtual void OnSecondChanged(EventArgs e)
		{
		}

		private int _prevYear = DateTime.Now.Year;
		private int _prevMonth = DateTime.Now.Month;
		private int _prevDay = DateTime.Now.Day;
		private int _prevHour = DateTime.Now.Hour;
		private int _prevMinute = DateTime.Now.Minute;
		private int _prevSecond = DateTime.Now.Second;

		protected override void ExecuteInternal()
		{
			if (DateTime.Now.Year != _prevYear)
			{
				OnYearChanged(EventArgs.Empty);
				_prevYear = DateTime.Now.Year;
			}
			if (DateTime.Now.Month != _prevMonth)
			{
				OnMonthChanged(EventArgs.Empty);
				_prevMonth = DateTime.Now.Month;
			}
			if (DateTime.Now.Day != _prevDay)
			{
				OnDayChanged(EventArgs.Empty);
				_prevDay = DateTime.Now.Day;
			}
			if (DateTime.Now.Hour != _prevHour)
			{
				OnHourChanged(EventArgs.Empty);
				_prevHour = DateTime.Now.Hour;
			}
			if (DateTime.Now.Minute != _prevMinute)
			{
				OnMinuteChanged(EventArgs.Empty);
				_prevMinute = DateTime.Now.Minute;
			}
			if (DateTime.Now.Second != _prevSecond)
			{
				OnSecondChanged(EventArgs.Empty);
				_prevSecond = DateTime.Now.Second;
			}
		}
	}

}
