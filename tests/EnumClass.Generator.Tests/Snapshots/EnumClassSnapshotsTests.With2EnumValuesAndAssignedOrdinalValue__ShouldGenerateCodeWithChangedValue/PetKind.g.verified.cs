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

    public abstract void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch);
    public abstract TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch);
    public static readonly CatEnumValue Cat = new CatEnumValue();
    public partial class CatEnumValue: PetKind
    {
        public override int Value => 0;
        public override Sample.PetKind Enum => Sample.PetKind.Cat;

        public override string ToString()
        {
            return "Cat";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
    public static readonly DogEnumValue Dog = new DogEnumValue();
    public partial class DogEnumValue: PetKind
    {
        public override int Value => 42;
        public override Sample.PetKind Enum => Sample.PetKind.Dog;

        public override string ToString()
        {
            return "Dog";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
}
