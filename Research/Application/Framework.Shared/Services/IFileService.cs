using System;
using System.IO;
using System.Threading.Tasks;

namespace Infragistics.Framework
{
    /// <summary>
	/// Define an API for loading and saving a text file. Reference this interface
	/// in the common code, and implement this interface in the app projects for
	/// iOS, Android and WinPhone. Remember to use the 
	///     [assembly: Dependency (typeof (IFileService_IMPLEMENTATION_CLASSNAME))]
	/// attribute on each of the implementations.
	/// </summary>
	public interface IFileService
    {
        Task SaveTextAsync(string filename, string text);
        Task<string> LoadTextAsync(string filename);
        bool FileExists(string filename);
        Stream CreateFileStream(string filename);

        //StreamReader CreateReaderStream(string filename);
        //StreamWriter CreateWriterStream(string filename);
        //StreamWriter CreateTextStream(string filename);
    }
}
