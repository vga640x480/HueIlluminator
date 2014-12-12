using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q42.HueApi;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.TeamFoundation.MVVM;
using System.Windows.Input;

namespace HueIlluminator.Models
{
    public class HueBridge
    {

        static readonly string APP_NAME = "HueIlluminator#TS@SONY";
        // APP_KEYは40文字まで、記号を使用するとエラーになる（ただしundocument...恐らくdatataypeがASCII stringなのだろう）
        static readonly string APP_KEY = "84FBECE4-2423-4B97-8120-47FE9E2C28CC";
        
        HueClient client;
        Bridge bridge;

        public string IP
        {
            get;
            private set;
        }

        public bool IsInitialized
        {
            get;
            private set;
        }

        public ICommand RegisterCommand
        {
            get;
            private set;
        }

        public ObservableCollection<HueLight> Lights
        {
            get;
            private set;
        }

        public HueBridge(string ip)
        {
            this.RegisterCommand = new RelayCommand(param => this.register());
            this.Lights = new ObservableCollection<HueLight>();
            this.IP = ip;
            init();
        }

        private async void init()
        {
//            /api/84FBECE4-2423-4B97-8120-47FE9E2C28CC/config

            client = new HueClient(this.IP, APP_KEY);

            try
            {
                bridge = await client.GetBridgeAsync();
                var lights = await client.GetLightsAsync();
                foreach (var light in lights)
                {
                    Debug.WriteLine("Light find " + light.Id);
                    this.Lights.Add(new HueLight(client, light));
                }
                this.IsInitialized = true;
            }
            catch (Exception e)
            {
                ;   // Nothing to do...
            }

        }

        async void register()
        {
            if (await client.RegisterAsync(APP_NAME, APP_KEY))
            {
                this.IsInitialized = true;
            }            
        }

        public void LightOn(int red, int green, int blue)
        {
            var command = new LightCommand();
            command.On = true;
            command.SetColor(red, green, blue);
            client.SendCommandAsync(command);
        }

        public void LightOff()
        {
            var command = new LightCommand();
            command.On = false;
            client.SendCommandAsync(command);
        }
    }
}
