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

    public abstract void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch, Action<ParrotEnumValue> parrotEnumValueSwitch, Action<HamsterEnumValue> hamsterEnumValueSwitch, Action<CrocodileEnumValue> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch, Action<ParrotEnumValue, T0> parrotEnumValueSwitch, Action<HamsterEnumValue, T0> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileEnumValueSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileEnumValueSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileEnumValueSwitch);
    public static readonly CatEnumValue Cat = new CatEnumValue();
    public partial class CatEnumValue: PetKind
    {
        public override int Value => 0;
        public override Sample.PetKind Enum => Sample.PetKind.Cat;

        public override string ToString()
        {
            return "Sample cat";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch, Action<ParrotEnumValue> parrotEnumValueSwitch, Action<HamsterEnumValue> hamsterEnumValueSwitch, Action<CrocodileEnumValue> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch, Action<ParrotEnumValue, T0> parrotEnumValueSwitch, Action<HamsterEnumValue, T0> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileEnumValueSwitch)
        {
            catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileEnumValueSwitch)
        {
            return catEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
    public static readonly DogEnumValue Dog = new DogEnumValue();
    public partial class DogEnumValue: PetKind
    {
        public override int Value => 1;
        public override Sample.PetKind Enum => Sample.PetKind.Dog;

        public override string ToString()
        {
            return "Dog";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch, Action<ParrotEnumValue> parrotEnumValueSwitch, Action<HamsterEnumValue> hamsterEnumValueSwitch, Action<CrocodileEnumValue> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch, Action<ParrotEnumValue, T0> parrotEnumValueSwitch, Action<HamsterEnumValue, T0> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileEnumValueSwitch)
        {
            dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileEnumValueSwitch)
        {
            return dogEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
    public static readonly ParrotEnumValue Parrot = new ParrotEnumValue();
    public partial class ParrotEnumValue: PetKind
    {
        public override int Value => 2;
        public override Sample.PetKind Enum => Sample.PetKind.Parrot;

        public override string ToString()
        {
            return "Parrot";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch, Action<ParrotEnumValue> parrotEnumValueSwitch, Action<HamsterEnumValue> hamsterEnumValueSwitch, Action<CrocodileEnumValue> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch, Action<ParrotEnumValue, T0> parrotEnumValueSwitch, Action<HamsterEnumValue, T0> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileEnumValueSwitch)
        {
            parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileEnumValueSwitch)
        {
            return parrotEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
    public static readonly HamsterEnumValue Hamster = new HamsterEnumValue();
    public partial class HamsterEnumValue: PetKind
    {
        public override int Value => 3;
        public override Sample.PetKind Enum => Sample.PetKind.Hamster;

        public override string ToString()
        {
            return "Hamster";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch, Action<ParrotEnumValue> parrotEnumValueSwitch, Action<HamsterEnumValue> hamsterEnumValueSwitch, Action<CrocodileEnumValue> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch, Action<ParrotEnumValue, T0> parrotEnumValueSwitch, Action<HamsterEnumValue, T0> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileEnumValueSwitch)
        {
            hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileEnumValueSwitch)
        {
            return hamsterEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
    public static readonly CrocodileEnumValue Crocodile = new CrocodileEnumValue();
    public partial class CrocodileEnumValue: PetKind
    {
        public override int Value => 4;
        public override Sample.PetKind Enum => Sample.PetKind.Crocodile;

        public override string ToString()
        {
            return "Crocodile";
        }

        public override void Switch(Action<CatEnumValue> catEnumValueSwitch, Action<DogEnumValue> dogEnumValueSwitch, Action<ParrotEnumValue> parrotEnumValueSwitch, Action<HamsterEnumValue> hamsterEnumValueSwitch, Action<CrocodileEnumValue> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catEnumValueSwitch, Func<DogEnumValue, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catEnumValueSwitch, Action<DogEnumValue, T0> dogEnumValueSwitch, Action<ParrotEnumValue, T0> parrotEnumValueSwitch, Action<HamsterEnumValue, T0> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catEnumValueSwitch, Action<DogEnumValue, T0, T1> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catEnumValueSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogEnumValueSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotEnumValueSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterEnumValueSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileEnumValueSwitch)
        {
            crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catEnumValueSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogEnumValueSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotEnumValueSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterEnumValueSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileEnumValueSwitch)
        {
            return crocodileEnumValueSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
}
