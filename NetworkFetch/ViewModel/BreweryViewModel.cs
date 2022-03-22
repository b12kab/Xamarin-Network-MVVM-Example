using NetworkFetch.Model;
using NetworkFetch.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NetworkFetch.ViewModel
{
    public class BreweryViewModel : INotifyPropertyChanged
    {
        int page;
        bool hasData = true;
        public ICommand FetchCommand { get; private set; }

        public BreweryViewModel()
        {
            page = 1;
            FetchCommand = new Command(async () => await GetData());
            //LocationList = new RangeObservableCollection<Brewery>();
            LocationList = new ObservableCollection<Brewery>();
            RowCount = 0;
            State = "ohio";
            Info = "Press 'next page' to start";
        }

        async Task GetData()
        {
            try
            {
                if (!hasData)
                    return;

                var result = await OpenBreweryDb.GetBreweriesAsync(State, page);
                page++;
                Info = "Fetched " + result.Count + " items";
                //LocationList.InsertRange(LocationList.Count, result);
                AddRange(result);

                RowCount = LocationList.Count;
                if (result.Count == 0)
                {
                    hasData = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Get data error - exception: " + ex.Message);
                System.Diagnostics.Debug.Flush();
            }
        }

        public void AddRange(IEnumerable<Brewery> collection)
        {
            foreach (var i in collection) LocationList.Add(i);
        }

        /**********************************************************************/

        private string _state;
        public string State
        {
            get => _state;
            set => _state = value;
        }

        private string infoDescription;
        public string Info
        {
            get => infoDescription;
            set
            {
                infoDescription = value;
                NotifyPropertyChanged("Info");
            }
        }

        private int count;
        public int RowCount
        {
            get => count;
            set
            {
                count = value;
                NotifyPropertyChanged("RowCount");
            }
        }

        // https://stackoverflow.com/questions/670577/observablecollection-doesnt-support-addrange-method-so-i-get-notified-for-each
        //private RangeObservableCollection<Brewery> _locationList;
        //public RangeObservableCollection<Brewery> LocationList
        //private IList<Brewery> _locationList;
        //public IList<Brewery> LocationList
        //{
        //    get => _locationList;
        //    set
        //    {
        //        _locationList = value;
        //        NotifyPropertyChanged("LocationList");
        //    }
        //}

        public ObservableCollection<Brewery> LocationList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
