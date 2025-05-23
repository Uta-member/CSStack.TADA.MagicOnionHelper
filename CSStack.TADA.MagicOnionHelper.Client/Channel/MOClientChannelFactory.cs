using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace CSStack.TADA.MagicOnionHelper.Client
{
    /// <summary>
    /// gRPCのクライアントチャンネルを提供するクラス
    /// </summary>
    public sealed class MOClientChannelFactory : IMOClientChannelFactory
    {
        private readonly string _host;
        private readonly bool _isGRPCWebDefault;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="option"></param>
        public MOClientChannelFactory(MOClientFactoryOption option)
        {
            _host = option.Host;
            _isGRPCWebDefault = option.IsGRPCWebDefault;
        }

        /// <inheritdoc/>
        public GrpcChannel GetChannel()
        {
            if (_isGRPCWebDefault)
            {
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                return GrpcChannel.ForAddress(_host, new GrpcChannelOptions { HttpClient = httpClient });
            }
            return GrpcChannel.ForAddress(_host);
        }

        /// <summary>
        /// gRPCのクライアントチャンネルを取得する（gRPC-Webかどうか選択可能）
        /// </summary>
        /// <param name="isGRPCWeb">gRPC-Webかどうか</param>
        /// <returns></returns>
        public GrpcChannel GetChannel(bool isGRPCWeb)
        {
            if (isGRPCWeb)
            {
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                return GrpcChannel.ForAddress(_host, new GrpcChannelOptions { HttpClient = httpClient });
            }
            return GrpcChannel.ForAddress(_host);
        }

        /// <summary>
        /// gRPCのクライアントチャンネルのオプション
        /// </summary>
        public sealed record MOClientFactoryOption
        {
            /// <summary>
            /// ホスト(gRPCサーバのIPアドレス)
            /// </summary>
            public required string Host { get; init; }

            /// <summary>
            /// デフォルトの接続方法がgRPC Webかどうか
            /// </summary>
            public required bool IsGRPCWebDefault { get; init; }
        }
    }
}
