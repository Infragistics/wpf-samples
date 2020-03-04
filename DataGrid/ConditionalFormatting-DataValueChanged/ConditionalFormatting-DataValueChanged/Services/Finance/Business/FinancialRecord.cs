using Prism.Mvvm;

namespace FinancialDataGrid.Services.Finance.Business
{
    public class FinancialRecord : BindableBase
    {
        private string _category;
        public string Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private string _contract;
        public string Contract
        {
            get { return _contract; }
            set { SetProperty(ref _contract, value); }
        }

        private string _settlement;
        public string Settlement
        {
            get { return _settlement; }
            set { SetProperty(ref _settlement, value); }
        }

        private string _region;
        public string Region
        {
            get { return _region; }
            set { SetProperty(ref _region, value); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private double _openPrice;
        public double OpenPrice
        {
            get { return _openPrice; }
            set { SetProperty(ref _openPrice, value); }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private double _change;
        public double Change
        {
            get { return _change; }
            set { SetProperty(ref _change, value); }
        }

        private double _changePercent;
        public double ChangePercent
        {
            get { return _changePercent; }
            set { SetProperty(ref _changePercent, value); }
        }

        private double _buy;
        public double Buy
        {
            get { return _buy; }
            set { SetProperty(ref _buy, value); }
        }

        private double _sell;
        public double Sell
        {
            get { return _sell; }
            set { SetProperty(ref _sell, value); }
        }

        private double _spread;
        public double Spread
        {
            get { return _spread; }
            set { SetProperty(ref _spread, value); }
        }

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set { SetProperty(ref _volume, value); }
        }

        private double _highD;
        public double HighD
        {
            get { return _highD; }
            set { SetProperty(ref _highD, value); }
        }

        private double _lowD;
        public double LowD
        {
            get { return _lowD; }
            set { SetProperty(ref _lowD, value); }
        }

        private double _highY;
        public double HighY
        {
            get { return _highY; }
            set { SetProperty(ref _highY, value); }
        }

        private double _lowY;
        public double LowY
        {
            get { return _lowY; }
            set { SetProperty(ref _lowY, value); }
        }

        private double _startY;
        public double StartY
        {
            get { return _startY; }
            set { SetProperty(ref _startY, value); }
        }

        private string _indGrou;
        public string IndGrou
        {
            get { return _indGrou; }
            set { SetProperty(ref _indGrou, value); }
        }

        private string _indSect;
        public string IndSect
        {
            get { return _indSect; }
            set { SetProperty(ref _indSect, value); }
        }

        private string _indSubg;
        public string IndSubg
        {
            get { return _indSubg; }
            set { SetProperty(ref _indSubg, value); }
        }

        private string _secType;
        public string SecType
        {
            get { return _secType; }
            set { SetProperty(ref _secType, value); }
        }

        private string _issuerN;
        public string IssuerN
        {
            get { return _issuerN; }
            set { SetProperty(ref _issuerN, value); }
        }

        private string _moodys;
        public string Moodys
        {
            get { return _moodys; }
            set { SetProperty(ref _moodys, value); }
        }

        private string _fitch;
        public string Fitch
        {
            get { return _fitch; }
            set { SetProperty(ref _fitch, value); }
        }

        private string _dBRS;
        public string DBRS
        {
            get { return _dBRS; }
            set { SetProperty(ref _dBRS, value); }
        }

        private string _collatT;
        public string CollatT
        {
            get { return _collatT; }
            set { SetProperty(ref _collatT, value); }
        }

        private string _curncy;
        public string Curncy
        {
            get { return _curncy; }
            set { SetProperty(ref _curncy, value); }
        }

        private string _security;
        public string Security
        {
            get { return _security; }
            set { SetProperty(ref _security, value); }
        }

        private string _sector;
        public string Sector
        {
            get { return _sector; }
            set { SetProperty(ref _sector, value); }
        }

        private double _cUSIP;
        public double CUSIP
        {
            get { return _cUSIP; }
            set { SetProperty(ref _cUSIP, value); }
        }

        private string _ticker;
        public string Ticker
        {
            get { return _ticker; }
            set { SetProperty(ref _ticker, value); }
        }

        private double _cpn;
        public double Cpn
        {
            get { return _cpn; }
            set { SetProperty(ref _cpn, value); }
        }

        private string _maturity;
        public string Maturity
        {
            get { return _maturity; }
            set { SetProperty(ref _maturity, value); }
        }

        private double _kRD_3YR;
        public double KRD_3YR
        {
            get { return _kRD_3YR; }
            set { SetProperty(ref _kRD_3YR, value); }
        }

        private double _zV_SPREAD;
        public double ZV_SPREAD
        {
            get { return _zV_SPREAD; }
            set { SetProperty(ref _zV_SPREAD, value); }
        }

        private double _kRD_5YR;
        public double KRD_5YR
        {
            get { return _kRD_5YR; }
            set { SetProperty(ref _kRD_5YR, value); }
        }

        private double _kRD_1YR;
        public double KRD_1YR
        {
            get { return _kRD_1YR; }
            set { SetProperty(ref _kRD_1YR, value); }
        }
    }

}
