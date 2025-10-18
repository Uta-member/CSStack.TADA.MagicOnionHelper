namespace CSStack.TADA.MagicOnionHelper.Abstractions
{
    /// <summary>
    /// Optional拡張クラス
    /// </summary>
    public static class OptionalExtensions
    {
        /// <summary>
        /// OptionalからMPOptionalに変換
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="optional"></param>
        /// <returns></returns>
        public static MPOptional<TValue> ToMPOptional<TValue>(this Optional<TValue> optional)
        {
            return new MPOptional<TValue>
            {
                HasValue = optional.HasValue,
                Value = (TValue)(optional.HasValue ? ((object)optional.Value!) : ((object)default(TValue)!))!
            };
        }

        /// <summary>
        /// OptionalからMPOptionalに変換
        /// </summary>
        /// <typeparam name="TDTOValue"></typeparam>
        /// <typeparam name="TMPValue"></typeparam>
        /// <param name="optional"></param>
        /// <returns></returns>
        public static MPOptional<TMPValue> ToMPOptional<TDTOValue, TMPValue>(this Optional<TDTOValue> optional)
            where TMPValue : IMPDTO<TDTOValue, TMPValue>
        {
            if(!optional.HasValue)
            {
                return new MPOptional<TMPValue>
                {
                    HasValue = false,
                    Value = default(TMPValue)!
                };
            }

            TMPValue val = TMPValue.FromDTO(optional.Value!);
            return new MPOptional<TMPValue>
            {
                HasValue = true,
                Value = (TMPValue)(object)val
            };
        }

        /// <summary>
        /// OptionalからMPOptionalに変換
        /// </summary>
        /// <typeparam name="TDTOValue"></typeparam>
        /// <typeparam name="TMPValue"></typeparam>
        /// <param name="optional"></param>
        /// <param name="getValue"></param>
        /// <returns></returns>
        public static MPOptional<TMPValue> ToMPOptional<TDTOValue, TMPValue>(
            this Optional<TDTOValue> optional,
            Func<TDTOValue, TMPValue> getValue)
        {
            if(!optional.HasValue)
            {
                return new MPOptional<TMPValue>
                {
                    HasValue = false,
                    Value = default(TMPValue)!
                };
            }

            TMPValue value = getValue(optional.Value!);
            return new MPOptional<TMPValue>
            {
                HasValue = true,
                Value = value
            };
        }
    }
}