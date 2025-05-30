using Grpc.Net.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	/// <summary>
	/// gRPCのクライアントチャンネルを提供するクラス
	/// </summary>
	public sealed class MOClientChannelFactory : IMOClientChannelFactory
	{
		private readonly GrpcChannel _channel;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channel"></param>
		public MOClientChannelFactory(GrpcChannel channel)
		{
			_channel = channel;
		}

		/// <inheritdoc/>
		public GrpcChannel GetChannel()
		{
			return _channel;
		}
	}
}
