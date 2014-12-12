using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HueIlluminator.Models;
using Q42.HueApi;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using HueIlluminator.Psycommu;
using Microsoft.TeamFoundation.MVVM;

namespace HueIlluminator
{
    class HueIlluminatorModel : ViewModelBase
    {
        private PsycommuReceiver _receiver;

        private HueBridge _selectedBridge;

        public ObservableCollection<HueBridge> Bridges
        {
            get;
            private set;
        }

        public HueBridge SelectedBridge
        {
            get
            {
                return _selectedBridge;
            }
            set
            {
                if (_selectedBridge != value)
                {
                    _selectedBridge = value;
                    RaisePropertyChanged("SelectedBridge");
                    if (value != null)
                    {
                        if (value.IsInitialized)
                        {
                            _receiver.Target = value;
                        }
                    }
                }
            }
        }

        public HueIlluminatorModel()
        {
            _receiver = new PsycommuReceiver();

            this.Bridges = new ObservableCollection<HueBridge>();
            findBridges();
        }

        async void findBridges()
        {
            var locator = new HttpBridgeLocator();
            var bridgeIPs = await locator.LocateBridgesAsync(TimeSpan.FromMinutes(3.0));
            foreach (var ip in bridgeIPs)
            {
                Debug.WriteLine(ip);
                this.Bridges.Add(new HueBridge(ip));
            }
        }
    }
}
