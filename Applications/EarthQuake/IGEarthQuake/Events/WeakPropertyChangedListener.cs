using System;
using System.ComponentModel;

namespace IGShowcase.EarthQuake
{
	/// <summary>
	/// A utility class that enables registering for an event without creating a hard reference 
	/// to the listener, allowing the listener to be garbage collected if there are no other 
	/// references to is.
	/// </summary>
	public class WeakPropertyChangedListener
	{
		#region Private fields
		private readonly object _syncLock;
		private INotifyPropertyChanged _source;
		private WeakReference _weakListener;
		#endregion

		#region Constructors and factory methods.
		/// <summary>
		/// Initializes a new instance of the <see cref="WeakPropertyChangedListener"/> class.
		/// </summary>
		/// <param name="source">The source of the PropertyChanged event.</param>
		/// <param name="listener">The listener.</param>
		private WeakPropertyChangedListener(INotifyPropertyChanged source, IPropertyChangedListener listener)
		{
			_syncLock = new object();
			_source = source;
			_source.PropertyChanged += SourcePropertyChanged;
			_weakListener = new WeakReference(listener);
		}

		/// <summary>
		/// Creates a new WeakPropertyChangedListener if the source implements INotifyPropertyChanged
		/// and registers for the PropertyChanged event.
		/// </summary>
		/// <param name="source">The source INotifyPropertyChanged.</param>
		/// <param name="listener">The listener object.</param>
		/// <returns></returns>
		public static WeakPropertyChangedListener CreateIfNecessary(object source, IPropertyChangedListener listener)
		{
			INotifyPropertyChanged inpc = source as INotifyPropertyChanged;
			return inpc != null ? new WeakPropertyChangedListener(inpc, listener) : null;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Detaches the WeakPropertyChangedListener from the INotifyPropertyChanged.PropertyChanged event.
		/// </summary>
		public void Disconnect()
		{
			lock (_syncLock)
			{
				DisconnectInternal();
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Called when the source INotifyPropertyChanged.PropertyChanged is raised and forwards the 
		/// event to the listener if the listener is still alive. If the listener has been garbage
		/// collected, detaches from the source.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			object target = null;
			lock (_syncLock)
			{
				if (_weakListener != null)
				{
					target = _weakListener.Target;
					if (target == null)
					{
						DisconnectInternal();
					}
				}
			}
			if (target != null)
			{
				((IPropertyChangedListener)target).OnPropertyChanged(sender, e);
			}
		}

		/// <summary>
		/// Disconnects the source INotifyPropertyChanged.PropertyChanged event.
		/// </summary>
		private void DisconnectInternal()
		{
			if (_source != null)
			{
				_source.PropertyChanged -= SourcePropertyChanged;
				_source = null;
				_weakListener = null;
			}
		}
		#endregion
	}
}