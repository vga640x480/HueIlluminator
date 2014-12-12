using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q42.HueApi.Models;
using Q42.HueApi;
using Microsoft.TeamFoundation.MVVM;
using System.Windows.Input;
using System.Diagnostics;

namespace HueIlluminator.Models
{
    public class HueLight : ViewModelBase
    {
        HueClient client;
        Light light;

        public HueBridge Bridge
        {
            get;
            private set;
        }

        public string ID
        {
            get
            {
                return light.Id;
            }
        }

        public State State
        {
            get
            {
                return light.State;
            }
        }

        public bool On
        {
            get
            {
                return light.State.On;
            }
            set
            {
                if (light.State.On != value)
                {
                    light.State.On = value;
                    RaisePropertyChanged("On");
                }
            }
        }

        public ICommand ToggleOnOffCommand
        {
            get;
            private set;
        }

        public async void turnOn(bool on)
        {
            var command = new LightCommand();
            command.On = on;
            command.SetColor("38b48b");
            var result = await client.SendCommandAsync(command, new List<string>{ light.Id });
            if (!result.HasErrors())
            {
                light.State.On = on;
            }
        }

        void toggleOnOff()
        {
            turnOn(light.State.On);
        }

        public HueLight(HueClient client, Light light)
        {
            this.ToggleOnOffCommand = new RelayCommand(param => this.toggleOnOff());
            this.client = client;
            this.light = light;
        }

        async void init()
        { 
            
        }


    }
}
