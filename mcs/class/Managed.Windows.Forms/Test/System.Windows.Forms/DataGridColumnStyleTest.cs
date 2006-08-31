// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2006 Novell, Inc. (http://www.novell.com)
//
// Author:
//	Chris Toshok <toshok@ximian.com>
//
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using NUnit.Framework;
using System.Data;

namespace MonoTests.System.Windows.Forms
{
	class StylePoker : DataGridColumnStyle
	{
		public StylePoker ()
		{
		}

		public StylePoker (PropertyDescriptor p) : base (p)
		{
		}

		public void DoCheckValidDataSource (CurrencyManager value)
		{
			CheckValidDataSource (value);
		}

		protected override void Abort (int rowNum) { }
		protected override bool Commit (CurrencyManager dataSource, int rowNum) { return false; }
		protected override void Edit (CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly,   string instantText,  bool cellIsVisible) { } 
		protected override int GetMinimumHeight () { return 0; }

		protected override int GetPreferredHeight (Graphics g, object value) { return 0; }

		protected override Size GetPreferredSize (Graphics g,  object value) { return Size.Empty; }
		protected override void Paint (Graphics g, Rectangle bounds, CurrencyManager source, int rowNum) { }
		protected override void Paint (Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight) { }
	}

	[TestFixture]
	public class DataGridColumnStyleTest
	{
		private bool eventhandled;

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void CheckValidDataSource_nullSource ()
		{
			StylePoker p = new StylePoker ();
			p.DoCheckValidDataSource (null);
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void CheckValidDataSource_emptyMappingName ()
		{
			StylePoker p = new StylePoker ();
			string[] arr = new string[] { "hi", "bye" };
			BindingContext bc = new BindingContext ();

			p.DoCheckValidDataSource ((CurrencyManager)bc[arr]);
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void CheckValidDataSource_invalidMappingName ()
		{
			StylePoker p = new StylePoker ();
			string[] arr = new string[] { "hi", "bye" };
			BindingContext bc = new BindingContext ();

			p.MappingName = "foo";

			p.DoCheckValidDataSource ((CurrencyManager)bc[arr]);
		}
	}
}
