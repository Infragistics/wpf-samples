using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class Account : ObservableModel
	{
 
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (_name != value)
				{
					_name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}

		private int _number;
		public int Number
		{
			get
			{
				return _number;
			}
			set
			{
				if (_number != value)
				{
					_number = value;
					this.OnPropertyChanged("Number");
				}
			}
		}

		private decimal? _balance;
		public decimal? Balance
		{
			get
			{
				return _balance;
			}
			set
			{
				if (_balance != value)
				{
					_balance = value;
					this.OnPropertyChanged("Balance");
				}
			}
		}

		private IEnumerable<Account> _accounts;
		public IEnumerable<Account> Accounts
		{
			get
			{
				return _accounts;
			}
			set
			{
				if (_accounts != value)
				{
					_accounts = value;
					this.OnPropertyChanged("Accounts");
				}
			}
		}
	}

}
