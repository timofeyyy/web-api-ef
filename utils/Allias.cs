namespace app.utils
{
	public class Allias
	{
		private Allias(string value) { Value = value; }

		public string Value { get; private set; }

		public static Allias Trace { get { return new Allias("Trace"); } }
		public static Allias Debug { get { return new Allias("Debug"); } }
		public static Allias Info { get { return new Allias("Info"); } }
		public static Allias Warning { get { return new Allias("Warning"); } }
		public static Allias Error { get { return new Allias("Error"); } }

		public override string ToString()
		{
			return Value;
		}
	}
}
