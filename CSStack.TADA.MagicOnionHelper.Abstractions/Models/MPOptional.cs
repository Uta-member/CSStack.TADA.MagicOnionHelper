using MessagePack;

namespace CSStack.TADA.MagicOnionHelper.Abstractions
{
	/// <summary>
	/// MessagePack用のOptional
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	[MessagePackObject]
	public sealed record MPOptional<TValue>
	{
		/// <summary>
		/// 値が設定されているか
		/// </summary>
		[Key(1)]
		public required bool HasValue { get; set; }

		/// <summary>
		/// 値
		/// </summary>
		[Key(0)]
		public required TValue Value { get; set; }

		/// <summary>
		/// Tada.OptionalからMPOptionalに変換
		/// </summary>
		/// <param name="optional"></param>
		/// <returns></returns>
		public static MPOptional<TValue> FromOptional(Optional<TValue> optional)
		{
			return new MPOptional<TValue>()
			{
				HasValue = optional.HasValue,
				Value = optional.HasValue ? optional.Value : default!
			};
		}

		/// <summary>
		/// Tada.OptionalからMPOptionalに変換
		/// </summary>
		/// <typeparam name="TDTOValue">Tada.Optionalの型引数</typeparam>
		/// <typeparam name="TMPValue">MPOptionalの値の型(IMPDTOに限る)</typeparam>
		/// <param name="optional"></param>
		/// <returns></returns>
		public static MPOptional<TValue> FromOptional<TDTOValue, TMPValue>(Optional<TDTOValue> optional)
			where TMPValue : IMPDTO<TDTOValue, TMPValue>, TValue
		{
			if (!optional.HasValue)
			{
				return new MPOptional<TValue>() { HasValue = false, Value = default!, };
			}
			TMPValue moObject = TMPValue.FromDTO(optional.Value);
			return new MPOptional<TValue>() { HasValue = true, Value = moObject, };
		}

		/// <summary>
		/// Tada.OptionalからMPOptionalに変換
		/// </summary>
		/// <typeparam name="TDTOValue">Tada.Optionalの型引数</typeparam>
		/// <param name="optional">Tada.Optionalのインスタンス</param>
		/// <param name="getValue">MOOptionalのValueに入れたいインスタンスを取得する(HasValueで検証後に呼ばれます)</param>
		/// <returns></returns>
		public static MPOptional<TValue> FromOptional<TDTOValue>(
			Optional<TDTOValue> optional,
			Func<TDTOValue, TValue> getValue)
		{
			if (!optional.HasValue)
			{
				return new MPOptional<TValue>() { HasValue = false, Value = default!, };
			}

			TValue? moObject = getValue.Invoke(optional.Value);
			return new MPOptional<TValue>() { HasValue = true, Value = moObject, };
		}

		/// <summary>
		/// TADA.Optionalに変換
		/// </summary>
		/// <returns></returns>
		public Optional<TValue> ToOptional()
		{
			if (!HasValue)
			{
				return new Optional<TValue>();
			}
			return new Optional<TValue>(Value);
		}

		/// <summary>
		/// TADA.Optionalに変換
		/// </summary>
		/// <typeparam name="TDTOValue">TADA.Optional変換後のOptional値の型</typeparam>
		/// <typeparam name="TMPValue">MPOptionalの値の型(IMPDTOに限る)</typeparam>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public Optional<TDTOValue> ToOptional<TDTOValue, TMPValue>()
			where TMPValue : IMPDTO<TDTOValue, TMPValue>, TValue
		{
			if (!HasValue)
			{
				return new Optional<TDTOValue>();
			}
			if (Value is TMPValue tmp)
			{
				return new Optional<TDTOValue>(tmp.ToDTO());
			}
			throw new ArgumentException($"ValueがMessagePackオブジェクトの型がIMPDTOを実装していません。{Value?.GetType().FullName}");
		}

		/// <summary>
		/// TADA.Optionalに変換
		/// </summary>
		/// <typeparam name="TDTOValue">TADA.Optional変換後のOptional値の型</typeparam>
		/// <param name="getValue">Optionalに渡す値のインスタンスを取得する(HasValueで検証後に呼ばれます)</param>
		/// <returns></returns>
		public Optional<TDTOValue> ToOptional<TDTOValue>(Func<TValue, TDTOValue> getValue)
		{
			if (!HasValue)
			{
				return new Optional<TDTOValue>();
			}

			return new Optional<TDTOValue>(getValue.Invoke(Value));
		}
	}
}
