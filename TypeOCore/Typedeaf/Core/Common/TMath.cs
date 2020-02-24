using System;

namespace TypeOEngine.Typedeaf.Core
{
    namespace Common
    {
        public class TMath
        {
            public static TMathValue<TValue> Add<TValue>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                return new TMathValue<TValue>(a + b);
            }

            public static TMathValue<TValue> Subtract<TValue>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                return new TMathValue<TValue>(a - b);
            }

            public static TMathValue<TValue> Multiply<TValue>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                return new TMathValue<TValue>(a * b);
            }

            public static TMathValue<TValue> Divide<TValue>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                return new TMathValue<TValue>(a / b);
            }

            public static TMathValue<TValue> Max<TValue>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                return new TMathValue<TValue>(a > b ? a : b);
            }

            public static TMathValue<TValue> Min<TValue>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                return new TMathValue<TValue>(a < b ? a : b);
            }

            public static TMathValue<TValue> Lerp<TValue, T>(TValue aInput, TValue bInput, T tInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                dynamic t = tInput;

                return new TMathValue<TValue>(Convert.ChangeType((1.0 - t) * a + b * t, typeof(TValue)));
            }

            public static TMathValue<TValue> InverseLerp<TValue>(TValue aInput, TValue bInput, TValue tValue)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                dynamic v = tValue;

                return new TMathValue<TValue>((v - a) / ( b - a));
            }

            public static TMathValue<T> InverseLerp<T, TValue>(TValue aInput, TValue bInput, TValue tValue)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                dynamic v = tValue;

                return new TMathValue<T>(Convert.ChangeType((v - a) / (b - a), typeof(T)));
            }

            public static TMathValue<TOutput> Remap<TInput, TOutput>(TInput iMin, TInput iMax, TOutput oMin, TOutput oMax, TInput value)
            {
                var t = InverseLerp(iMin, iMax, value);
                return Lerp(oMin, oMax, t.Value);
            }
        }

        public class TMathValue<TValue>
        {
            public TValue Value { get; private set; }

            internal TMathValue(TValue value)
            {
                Value = value;
            }

            public TMathValue<TValue> Add(TValue bInput)
            {
                return TMath.Add(Value, bInput);
            }

            public TMathValue<TValue> Subtract(TValue bInput)
            {
                return TMath.Subtract(Value, bInput);
            }

            public TMathValue<TValue> Multiply(TValue bInput)
            {
                return TMath.Multiply(Value, bInput);
            }

            public TMathValue<TValue> Divide(TValue bInput)
            {
                return TMath.Divide(Value, bInput);
            }

            public TMathValue<TValue> Max(TValue bInput)
            {
                return TMath.Max(Value, bInput);
            }

            public TMathValue<TValue> Min(TValue bInput)
            {
                return TMath.Min(Value, bInput);
            }

            public TMathValue<TValue> Lerp<T>(TValue bInput, T tInput)
            {
                return TMath.Lerp(Value, bInput, tInput);
            }

            public TMathValue<TValue> InverseLerp(TValue aInput, TValue bInput)
            {
                return TMath.InverseLerp(aInput, bInput, Value);
            }

            public TMathValue<T> InverseLerp<T>(TValue aInput, TValue bInput)
            {
                dynamic a = aInput;
                dynamic b = bInput;
                dynamic v = Value;

                return new TMathValue<T>((v - a) / (b - a));
            }

            public TMathValue<TOutput> Remap<TOutput>(TValue iMin, TValue iMax, TOutput oMin, TOutput oMax)
            {
                return TMath.Remap(iMin, iMax, oMin, oMax, Value);
            }

            //TMathValue
            public TMathValue<TValue> UpperClamp(TValue clampTo)
            {
                dynamic a = Value;
                dynamic b = clampTo;
                Value = a < b ? a : b;
                return this;
            }

            public TMathValue<TValue> LowerClamp(TValue clampTo)
            {
                dynamic a = Value;
                dynamic b = clampTo;
                Value = a > b ? a : b;
                return this;
            }

            public TMathValue<TValue> Abs()
            {
                dynamic v = Value;
                Value = Math.Abs(v);
                return this;
            }
        }
    }
}
