//HintName: SampleEnum.g.cs
using System;
using System.Runtime.CompilerServices;

namespace Test.EnumClass
{

public abstract partial class SampleEnum: IEquatable<SampleEnum>, IEquatable<global::Test.SampleEnum>, IComparable<SampleEnum>, IComparable<global::Test.SampleEnum>, IComparable
{
    protected readonly global::Test.SampleEnum _realEnumValue;

    protected SampleEnum(global::Test.SampleEnum enumValue)
    {
        this._realEnumValue = enumValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator global::Test.SampleEnum(SampleEnum value)
    {
        return value._realEnumValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(SampleEnum value)
    {
        return (byte) value._realEnumValue;
    }

    public bool Equals(SampleEnum other)
    {
        return !ReferenceEquals(other, null) && other._realEnumValue == this._realEnumValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(global::Test.SampleEnum other)
    {
        return other == this._realEnumValue;
    }

    public override bool Equals(object other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (other is SampleEnum)
        {
            return this.Equals((SampleEnum) other);
        }
        if (other is global::Test.SampleEnum)
        {
            return this.Equals((global::Test.SampleEnum) other);
        }
        return false;
    }

    public static bool operator ==(SampleEnum left, global::Test.SampleEnum right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(SampleEnum left, global::Test.SampleEnum right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(global::Test.SampleEnum left, SampleEnum right)
    {
        return right.Equals(left);
    }

    public static bool operator !=(global::Test.SampleEnum left, SampleEnum right)
    {
        return !right.Equals(left);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return this._realEnumValue.GetHashCode();
    }

    public static bool TryParse(string value, out SampleEnum sampleEnum)
    {
        switch (value)
        {
            case "First":
                sampleEnum = First;
                return true;
            case "SampleEnum.First":
                sampleEnum = First;
                return true;
        }
        sampleEnum = null;
        return false;
    }


    public int CompareTo(object other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        if (other is SampleEnum)
        {
            SampleEnum temp = (SampleEnum) other;
            byte result = ((byte)this._realEnumValue) - ((byte) temp._realEnumValue);
            return result < 0 ? -1 : result == 0 ? 0 : 1;
        }
        if (other is global::Test.SampleEnum)
        {
            byte result = ((byte)this._realEnumValue) - ((byte) other);
            return result < 0 ? -1 : result == 0 ? 0 : 1;
        }
        throw new ArgumentException($"Object to compare must be either {typeof(SampleEnum)} or {typeof(global::Test.SampleEnum)}. Given type: {other.GetType()}", "other");
    }

    public int CompareTo(SampleEnum other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
            byte result = ((byte)this._realEnumValue) - ((byte) other._realEnumValue);
            return result < 0 ? -1 : result == 0 ? 0 : 1;
    }

    public int CompareTo(global::Test.SampleEnum other)
    {
            byte result = ((byte)this._realEnumValue) - ((byte) other);
            return result < 0 ? -1 : result == 0 ? 0 : 1;
    }

    public abstract void Switch(Action<FirstEnumValue> firstSwitch);
    public abstract TResult Switch<TResult>(Func<FirstEnumValue, TResult> firstSwitch);
    public abstract void Switch<T0>(T0 arg0, Action<FirstEnumValue, T0> firstSwitch);
    public abstract TResult Switch<TResult, T0>(T0 arg0, Func<FirstEnumValue, T0, TResult> firstSwitch);
    public abstract void Switch<T0, T1>(T0 arg0, T1 arg1, Action<FirstEnumValue, T0, T1> firstSwitch);
    public abstract TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<FirstEnumValue, T0, T1, TResult> firstSwitch);
    public abstract void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<FirstEnumValue, T0, T1, T2> firstSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<FirstEnumValue, T0, T1, T2, TResult> firstSwitch);
    public abstract void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<FirstEnumValue, T0, T1, T2, T3> firstSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<FirstEnumValue, T0, T1, T2, T3, TResult> firstSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<FirstEnumValue, T0, T1, T2, T3, T4> firstSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<FirstEnumValue, T0, T1, T2, T3, T4, TResult> firstSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<FirstEnumValue, T0, T1, T2, T3, T4, T5> firstSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<FirstEnumValue, T0, T1, T2, T3, T4, T5, TResult> firstSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<FirstEnumValue, T0, T1, T2, T3, T4, T5, T6> firstSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<FirstEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> firstSwitch);

    public static readonly FirstEnumValue First = new FirstEnumValue();
    public partial class FirstEnumValue: SampleEnum
    {
        public FirstEnumValue(): base(global::Test.SampleEnum.First) { }
        public override string ToString()
        {
            return "First";
        }

        public override void Switch(Action<FirstEnumValue> firstSwitch)
        {
            firstSwitch(this);
        }

        public override TResult Switch<TResult>(Func<FirstEnumValue, TResult> firstSwitch)
        {
            return firstSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<FirstEnumValue, T0> firstSwitch)
        {
            firstSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<FirstEnumValue, T0, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<FirstEnumValue, T0, T1> firstSwitch)
        {
            firstSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<FirstEnumValue, T0, T1, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<FirstEnumValue, T0, T1, T2> firstSwitch)
        {
            firstSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<FirstEnumValue, T0, T1, T2, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<FirstEnumValue, T0, T1, T2, T3> firstSwitch)
        {
            firstSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<FirstEnumValue, T0, T1, T2, T3, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<FirstEnumValue, T0, T1, T2, T3, T4> firstSwitch)
        {
            firstSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<FirstEnumValue, T0, T1, T2, T3, T4, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<FirstEnumValue, T0, T1, T2, T3, T4, T5> firstSwitch)
        {
            firstSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<FirstEnumValue, T0, T1, T2, T3, T4, T5, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<FirstEnumValue, T0, T1, T2, T3, T4, T5, T6> firstSwitch)
        {
            firstSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<FirstEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> firstSwitch)
        {
            return firstSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
}
