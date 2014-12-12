using System.Diagnostics;
using Sony.TS.Brainwave;
using HueIlluminator;
using System.Windows.Threading;
using System;
using System.Threading;
using HueIlluminator.Misc;

namespace HueIlluminator.Psycommu
{


    class PsycommuReceiver
    {
        private BrainwaveWatcher _brainwaveWatcher;


        public PsycommuReceiver()
        {
            _brainwaveWatcher = new BrainwaveWatcher();
            _brainwaveWatcher.Host = "ws://frozen-plains-4732.herokuapp.com/brainwave";
            _brainwaveWatcher.BrainwaveChanged += (s, e) => 
            {
                Debug.WriteLine("Brainwave data changed at {3}: alpha={0} beta={1} theta={2}", e.Data.alpha, e.Data.beta, e.Data.theta, e.Data.timestamp);
                // TODO: ライトのコントロール
                var target = this.Target;
                if (target != null)
                {
                    var rgb = RainbowColor.wlen2rgb((int)((780-380) * e.Data.alpha + 380));
                    target.LightOn((int)(255 * rgb.R), (int)(255 * rgb.G), (int)(255 * rgb.B));
                }
            };

            _brainwaveWatcher.Listen();
        }

        public HueIlluminator.Models.HueBridge Target
        {
            get;
            set;
        }
    }
}
