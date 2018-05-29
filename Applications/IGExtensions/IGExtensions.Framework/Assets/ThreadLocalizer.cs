using System.Globalization;
using System.Threading;

namespace IGExtensions.Framework.Assets
{
    public static class ThreadLocalizer
    {
        #region Constructors
		/// <summary>
        /// Initializes the <see cref="ThreadLocalizer"/> class.
		/// </summary>
        static ThreadLocalizer()
		{
			CurrentLanguage = DefaultLanguage = CultureInfo.CurrentCulture.Name;
		}
		#endregion Constructors

		#region Public Properties
		/// <summary>
		/// Gets or sets the default language.
		/// </summary>
		/// <value>The default language.</value>
		public static string DefaultLanguage { get; set; }
		/// <summary>
		/// Gets or sets the current language.
		/// </summary>
		/// <value>The current language.</value>
		public static string CurrentLanguage { get; private set; }
		#endregion Public Properties

		#region Public Methods
		/// <summary>
        /// Localizes the current thread to specified language.
		/// </summary>
		/// <param name="language">The language locale to use.  Note: If this is null the default language locale will be used</param>
		public static void Localize(string language = null)
		{
			var resourceCulture = new CultureInfo(language ?? DefaultLanguage);
			var uiCulture = CultureInfo.CurrentUICulture;

			if (uiCulture.TwoLetterISOLanguageName == resourceCulture.TwoLetterISOLanguageName)
			{
				CurrentLanguage = uiCulture.Name;
			}
			else
			{
				CurrentLanguage = resourceCulture.Name;

				Thread.CurrentThread.CurrentUICulture = resourceCulture;
			}
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
		}
		#endregion Public Methods
    }
}