using Grpc.Net.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	/// <summary>
	/// gRPCのクライアントチャンネルを提供するインターフェース
	/// </summary>
	public interface IMOClientChannelFactory
	{
		/// <summary>
		/// gRPCのクライアントチャンネルを取得する
		/// </summary>
		/// <returns></returns>
		GrpcChannel GetChannel();
	}
}
