using Grpc.Net.Client;
using MagicOnion;
using MagicOnion.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	/// <summary>
	/// MagicOnionのサービスクライアントファクトリ
	/// </summary>
	public sealed class MOServiceClientFactory
	{
		/// <summary>
		/// クライアント作成
		/// </summary>
		/// <typeparam name="TMOService"></typeparam>
		/// <param name="channelFactory"></param>
		/// <returns></returns>
		public static TMOService Create<TMOService>(IMOClientChannelFactory channelFactory)
			where TMOService : IService<TMOService>
		{
			var channel = channelFactory.GetChannel();
			return Create<TMOService>(channel);
		}

		/// <summary>
		/// クライアント作成
		/// </summary>
		/// <typeparam name="TMOService"></typeparam>
		/// <param name="channel"></param>
		/// <returns></returns>
		public static TMOService Create<TMOService>(GrpcChannel channel) where TMOService : IService<TMOService>
		{
			return MagicOnionClient.Create<TMOService>(channel);
		}
	}
}
