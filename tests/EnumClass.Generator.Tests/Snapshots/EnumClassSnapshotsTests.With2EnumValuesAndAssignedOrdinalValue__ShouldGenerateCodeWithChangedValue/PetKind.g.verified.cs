//HintName: PetKind.g.cs
using System;
using System.Runtime.CompilerServices;

namespace Sample.EnumClass
{

public abstract partial class PetKind: IEquatable<PetKind>, IEquatable<global::Sample.PetKind>
{
    protected readonly global::Sample.PetKind _realEnumValue;

    protected PetKind(global::Sample.PetKind enumValue)
    {
        this._realEnumValue = enumValue;
    }

    public static implicit operator global::Sample.PetKind(PetKind value)
    {
        return value._realEnumValue;
    }

    public static explicit operator int(PetKind value)
    {
        return (int) value._realEnumValue;
    }

    public bool Equals(PetKind other)
    {
        return !ReferenceEquals(other, null) && other._realEnumValue == this._realEnumValue;
    }

    public bool Equals(global::Sample.PetKind other)
    {
        return other == this._realEnumValue;
    }

    public override bool Equals(object other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        if (other is PetKind)
        {
            return this.Equals((PetKind) other);
        }
        if (other is global::Sample.PetKind)
        {
            return this.Equals((global::Sample.PetKind) other);
        }
        return false;
    }

    public static bool operator ==(PetKind left, global::Sample.PetKind right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PetKind left, global::Sample.PetKind right)
    {
        return !left.Equals(right);
    }

    public static bool operator ==(global::Sample.PetKind left, PetKind right)
    {
        return right.Equals(left);
    }

    public static bool operator !=(global::Sample.PetKind left, PetKind right)
    {
        return !right.Equals(left);
    }

    public override int GetHashCode()
    {
        return this._realEnumValue.GetHashCode();
    }

    public abstract void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch);
    public abstract TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch);
    public abstract void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch);
    public abstract TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch);
    public abstract void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch);
    public abstract TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch);
    public abstract void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch);
    public abstract void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch);

    public static readonly CatEnumValue Cat = new CatEnumValue();
    public partial class CatEnumValue: PetKind
    {
        public CatEnumValue(): base(global::Sample.PetKind.Cat) { }
        public override string ToString()
        {
            return "Cat";
        }

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch)
        {
            catSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch)
        {
            return catSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch)
        {
            catSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch)
        {
            return catSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch)
        {
            catSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch)
        {
            return catSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch)
        {
            catSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }

    public static readonly DogEnumValue Dog = new DogEnumValue();
    public partial class DogEnumValue: PetKind
    {
        public DogEnumValue(): base(global::Sample.PetKind.Dog) { }
        public override string ToString()
        {
            return "Dog";
        }

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch)
        {
            dogSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch)
        {
            return dogSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch)
        {
            dogSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch)
        {
            dogSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
}
}
