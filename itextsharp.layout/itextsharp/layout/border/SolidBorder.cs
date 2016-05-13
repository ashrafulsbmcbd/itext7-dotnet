/*
$Id$

This file is part of the iText (R) project.
Copyright (c) 1998-2016 iText Group NV
Authors: Bruno Lowagie, Paulo Soares, et al.

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License version 3
as published by the Free Software Foundation with the addition of the
following permission added to Section 15 as permitted in Section 7(a):
FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
OF THIRD PARTY RIGHTS

This program is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
or FITNESS FOR A PARTICULAR PURPOSE.
See the GNU Affero General Public License for more details.
You should have received a copy of the GNU Affero General Public License
along with this program; if not, see http://www.gnu.org/licenses or write to
the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
Boston, MA, 02110-1301 USA, or download the license from the following URL:
http://itextpdf.com/terms-of-use/

The interactive user interfaces in modified source and object code versions
of this program must display Appropriate Legal Notices, as required under
Section 5 of the GNU Affero General Public License.

In accordance with Section 7(b) of the GNU Affero General Public License,
a covered work must retain the producer line in every PDF that is created
or manipulated using iText.

You can be released from the requirements of the license by purchasing
a commercial license. Buying such a license is mandatory as soon as you
develop commercial activities involving the iText software without
disclosing the source code of your own applications.
These activities include: offering paid services to customers as an ASP,
serving PDFs on the fly in a web application, shipping iText with a closed
source product.

For more information, please contact iText Software Corp. at this
address: sales@itextpdf.com
*/
using iTextSharp.Kernel.Pdf.Canvas;

namespace iTextSharp.Layout.Border
{
	/// <summary>Draws a solid border around the element it's set to.</summary>
	public class SolidBorder : iTextSharp.Layout.Border.Border
	{
		/// <summary>Creates a SolidBorder with the specified width and sets the color to black.
		/// 	</summary>
		/// <param name="width">width of the border</param>
		public SolidBorder(float width)
			: base(width)
		{
		}

		/// <summary>Creates a SolidBorder with the specified width and the specified color.</summary>
		/// <param name="color">color of the border</param>
		/// <param name="width">width of the border</param>
		public SolidBorder(iTextSharp.Kernel.Color.Color color, float width)
			: base(color, width)
		{
		}

		public override int GetType()
		{
			return iTextSharp.Layout.Border.Border.SOLID;
		}

		public override void Draw(PdfCanvas canvas, float x1, float y1, float x2, float y2
			, float borderWidthBefore, float borderWidthAfter)
		{
			float x3 = 0;
			float y3 = 0;
			float x4 = 0;
			float y4 = 0;
			Border.Side borderSide = GetBorderSide(x1, y1, x2, y2);
			switch (borderSide)
			{
				case Border.Side.TOP:
				{
					x3 = x2 + borderWidthAfter;
					y3 = y2 + width;
					x4 = x1 - borderWidthBefore;
					y4 = y1 + width;
					break;
				}

				case Border.Side.RIGHT:
				{
					x3 = x2 + width;
					y3 = y2 - borderWidthAfter;
					x4 = x1 + width;
					y4 = y1 + borderWidthBefore;
					break;
				}

				case Border.Side.BOTTOM:
				{
					x3 = x2 - borderWidthAfter;
					y3 = y2 - width;
					x4 = x1 + borderWidthBefore;
					y4 = y1 - width;
					break;
				}

				case Border.Side.LEFT:
				{
					x3 = x2 - width;
					y3 = y2 + borderWidthAfter;
					x4 = x1 - width;
					y4 = y1 - borderWidthBefore;
					break;
				}
			}
			canvas.SetFillColor(color);
			canvas.MoveTo(x1, y1).LineTo(x2, y2).LineTo(x3, y3).LineTo(x4, y4).LineTo(x1, y1)
				.Fill();
		}

		public override void DrawCellBorder(PdfCanvas canvas, float x1, float y1, float x2
			, float y2)
		{
			canvas.SaveState().SetStrokeColor(color).SetLineWidth(width).MoveTo(x1, y1).LineTo
				(x2, y2).Stroke().RestoreState();
		}
	}
}
