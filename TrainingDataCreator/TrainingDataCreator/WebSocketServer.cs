﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using vtortola.WebSockets;

namespace MindWaveProxy
{
    class WebSocketServer
    {
        private CancellationTokenSource tokenSource;

        private CancellationToken token;

        private WebSocketListener server;

        /// クライアントのWebSocketインスタンスを格納
        private List<WebSocket> _client = new List<WebSocket>();

        public delegate void OnReceive(string data);

        /// WebSocketサーバースタート
        public async void StartServer(int port, OnReceive onReceive)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, port);
            server = new WebSocketListener(endpoint);
            var rfc6455 = new vtortola.WebSockets.Rfc6455.WebSocketFactoryRfc6455(server);
            server.Standards.RegisterStandard(rfc6455);
            server.Start();


            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;

            await Task.Run(() => Accept(server, onReceive), token);
        }

        public void StopServer()
        {
            if(tokenSource != null)
            {
                tokenSource.Cancel();
            }
            if(server != null)
            {
                server.Stop();
            }
        }

        public async void Accept(WebSocketListener server, OnReceive onReceive)
        {
            while (!token.IsCancellationRequested)
            {
                var ws = await server.AcceptWebSocketAsync(token).ConfigureAwait(false);
                if (ws != null)
                {
                    await Task.Run(() => ProcessRequest(ws, onReceive), token);
                }
            }
        }
        
        /// WebSocket接続毎の処理
        public async void ProcessRequest(WebSocket ws, OnReceive onReceive)
        {
            /// 新規クライアントを追加
            _client.Add(ws);
            /// 一定時間ごとに接続維持確認
            while (ws.IsConnected && !token.IsCancellationRequested)
            {
                string msg = await ws.ReadStringAsync(token).ConfigureAwait(false);
                if (msg == null)
                    continue;

                onReceive(msg);

            }
            /// クライアントを除外する
            _client.Remove(ws);
            ws.Dispose();
        }
    }
}
