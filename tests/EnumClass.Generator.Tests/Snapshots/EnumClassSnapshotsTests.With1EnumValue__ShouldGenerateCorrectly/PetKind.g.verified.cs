//HintName: PetKind.g.cs
using System;

namespace Sample.EnumClass;
public abstract partial class PetKind: IEquatable<PetKind>, IEquatable<Sample.PetKind>
{
    public abstract int Value { get; }
    public abstract Sample.PetKind Enum { get; }
    public static implicit operator Sample.PetKind(PetKind value)
    {
        return value.Enum;
    }

    public bool Equals(PetKind other)
    {
        return !ReferenceEquals(other, null) && other.Enum == this.Enum;
    }

    public bool Equals(Sample.PetKind other)
    {
        return other == this.Enum;
    }

    public override bool Equals(object other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (other is PetKind)
        {
            return this.Equals((PetKind) other);
        }
        if (other is Sample.PetKind)
        {
            return this.Equals((Sample.PetKind) other);
        }
        return false;
    }

    public static bool operator ==(PetKind left, Sample.PetKind right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PetKind left, Sample.PetKind right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(Sample.PetKind left, PetKind right)
    {
        return right.Equals(left);
    }

    public static bool operator !=(Sample.PetKind left, PetKind right)
    {
        return !right.Equals(left);
    }

    public override int GetHashCode()
    {
        return this.Value;
    }

    public abstract void Switch(Action<CatEnumValue> catEnumValueSwitch);
    public abstract TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch);
    public abstract void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch);
    public abstract void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch);
    public abstract void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch);
    public static readonly CatEnumValue Cat = new CatEnumValue();
    public partial class CatEnumValue: PetKind
    {
        public override int Value => 0;
        public override Sample.PetKind Enum => Sample.PetKind.Cat;

        public override string ToString()
        {
            return "Cat";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch)
        {
            catEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
}
