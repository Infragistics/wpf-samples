//-------------------------------------------------------------------------
// <copyright file="WeakPropertyChangedListener.cs" company="Infragistics">
//
// Copyright (c) 2010 Infragistics, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
//-------------------------------------------------------------------------

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