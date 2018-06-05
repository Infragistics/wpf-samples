namespace IGTrading.Assets.LocalizedResources
{
	public class LocalizedResources
	{
		#region Private Variables
		private static Strings strings = new Strings();
		#endregion Private Variables

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="LocalizedResources"/> class.
		/// </summary>
		public LocalizedResources()
		{
		}
		#endregion Constructors

		#region Public Properties
		/// <summary>
		/// Gets the strings.
		/// </summary>
		/// <value>The strings.</value>
		public Strings Strings
		{
			get { return strings; }
		}
         
		#endregion Public Properties
	}
}
