using PdfSharp.Fonts;

namespace app.PdfReport
{
	public class FontResolver : IFontResolver
	{

		public FontResolver(string fontPath)
		{
			this.fontPath = fontPath;
		}

		private readonly string fontPath;

		public string DefaultFontName => "Arial";

		public byte[] GetFont(string faceName)
		{
			return File.ReadAllBytes(fontPath);
		}

		public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
		{
			return new FontResolverInfo("Arial");
		}
	}
}
