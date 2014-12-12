using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using WebSocket4Net;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Sony.TS.Brainwave
{
    public class BrainwaveWatcher
    {
        private CancellationTokenSource _cancelTokenSource;

        public BrainwaveWatcher()
        {
            _cancelTokenSource = new CancellationTokenSource();
            this.Host = "ws://frozen-plains-4732.herokuapp.com/brainwave";
        }

        public string Host
        {
            get;
            set;
        }

        public delegate void BrainwaveChangedEventHandler(object sender, BrainwaveChangedEventArgs e);

        public event BrainwaveChangedEventHandler BrainwaveChanged;

        SynchronizationContext context = SynchronizationContext.Current;

        protected void OnBrainwaveChanged(BrainwaveChangedEventArgs e)
        {
            context.Post(state =>
            {
                var handler = this.BrainwaveChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }, null);
        }

        public void Listen()
        {
            CancellationToken ct = _cancelTokenSource.Token;
            var task = Task.Factory.StartNew(() =>
            {
                ct.ThrowIfCancellationRequested();

                var ws = new WebSocket(this.Host);

                ws.Opened += (s, e) =>
                {
                    Debug.WriteLine("Web socket opend.");
                    ws.Send("Hello!");
                };

                ws.Closed += (s, e) =>
                {
                    Debug.WriteLine("Web socket closed.");
                };

                ws.Error += (s, e) =>
                {
                    Debug.WriteLine("Web socket error.({0})", e.Exception.Message);
                };

                ws.MessageReceived += (s, e) =>
                {
                    Debug.WriteLine("Recieved:{0}", e.Message);
                    var serializer = new DataContractJsonSerializer(typeof(BrainwaveData));
                    using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Message)))
                    {
                        try
                        {
                            BrainwaveData data = (BrainwaveData)serializer.ReadObject(ms);
                            BrainwaveChangedEventArgs args = new BrainwaveChangedEventArgs { Timestamp = DateTime.Now, Data = data };
                            OnBrainwaveChanged(args);
                        }
                        catch (SerializationException)
                        {
                            ;   // Nothing to do...
                        }
                    }
                };

                ws.Open();

                for (; ; )
                {
                    if (ct.IsCancellationRequested)
                    {
                        ws.Close();
                        ct.ThrowIfCancellationRequested();
                    }
                }
            }, _cancelTokenSource.Token);

            //try
            //{
            //    task.Wait();
            //}
            //catch (AggregateException e)
            //{

            //}
        }
    }
}
