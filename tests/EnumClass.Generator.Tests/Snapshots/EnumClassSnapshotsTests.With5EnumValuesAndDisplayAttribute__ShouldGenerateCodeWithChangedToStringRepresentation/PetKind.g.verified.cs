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
        return (int) this._realEnumValue;
    }

    public abstract void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch, Action<ParrotEnumValue> parrotSwitch, Action<HamsterEnumValue> hamsterSwitch, Action<CrocodileEnumValue> crocodileSwitch);
    public abstract TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch, Func<ParrotEnumValue, TResult> parrotSwitch, Func<HamsterEnumValue, TResult> hamsterSwitch, Func<CrocodileEnumValue, TResult> crocodileSwitch);
    public abstract void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch, Action<ParrotEnumValue, T0> parrotSwitch, Action<HamsterEnumValue, T0> hamsterSwitch, Action<CrocodileEnumValue, T0> crocodileSwitch);
    public abstract TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch, Func<ParrotEnumValue, T0, TResult> parrotSwitch, Func<HamsterEnumValue, T0, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileSwitch);
    public abstract void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch, Action<ParrotEnumValue, T0, T1> parrotSwitch, Action<HamsterEnumValue, T0, T1> hamsterSwitch, Action<CrocodileEnumValue, T0, T1> crocodileSwitch);
    public abstract TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileSwitch);
    public abstract void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileSwitch);
    public abstract void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileSwitch);
    public abstract void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileSwitch);
    public abstract TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileSwitch);

    public static readonly CatEnumValue Cat = new CatEnumValue();
    public partial class CatEnumValue: PetKind
    {
        public CatEnumValue(): base(global::Sample.PetKind.Cat) { }
        public override string ToString()
        {
            return "Sample cat";
        }

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch, Action<ParrotEnumValue> parrotSwitch, Action<HamsterEnumValue> hamsterSwitch, Action<CrocodileEnumValue> crocodileSwitch)
        {
            catSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch, Func<ParrotEnumValue, TResult> parrotSwitch, Func<HamsterEnumValue, TResult> hamsterSwitch, Func<CrocodileEnumValue, TResult> crocodileSwitch)
        {
            return catSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch, Action<ParrotEnumValue, T0> parrotSwitch, Action<HamsterEnumValue, T0> hamsterSwitch, Action<CrocodileEnumValue, T0> crocodileSwitch)
        {
            catSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch, Func<ParrotEnumValue, T0, TResult> parrotSwitch, Func<HamsterEnumValue, T0, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileSwitch)
        {
            return catSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch, Action<ParrotEnumValue, T0, T1> parrotSwitch, Action<HamsterEnumValue, T0, T1> hamsterSwitch, Action<CrocodileEnumValue, T0, T1> crocodileSwitch)
        {
            catSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileSwitch)
        {
            return catSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileSwitch)
        {
            catSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileSwitch)
        {
            return catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileSwitch)
        {
            catSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileSwitch)
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

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch, Action<ParrotEnumValue> parrotSwitch, Action<HamsterEnumValue> hamsterSwitch, Action<CrocodileEnumValue> crocodileSwitch)
        {
            dogSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch, Func<ParrotEnumValue, TResult> parrotSwitch, Func<HamsterEnumValue, TResult> hamsterSwitch, Func<CrocodileEnumValue, TResult> crocodileSwitch)
        {
            return dogSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch, Action<ParrotEnumValue, T0> parrotSwitch, Action<HamsterEnumValue, T0> hamsterSwitch, Action<CrocodileEnumValue, T0> crocodileSwitch)
        {
            dogSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch, Func<ParrotEnumValue, T0, TResult> parrotSwitch, Func<HamsterEnumValue, T0, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch, Action<ParrotEnumValue, T0, T1> parrotSwitch, Action<HamsterEnumValue, T0, T1> hamsterSwitch, Action<CrocodileEnumValue, T0, T1> crocodileSwitch)
        {
            dogSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileSwitch)
        {
            dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileSwitch)
        {
            return dogSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }

    public static readonly ParrotEnumValue Parrot = new ParrotEnumValue();
    public partial class ParrotEnumValue: PetKind
    {
        public ParrotEnumValue(): base(global::Sample.PetKind.Parrot) { }
        public override string ToString()
        {
            return "Parrot";
        }

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch, Action<ParrotEnumValue> parrotSwitch, Action<HamsterEnumValue> hamsterSwitch, Action<CrocodileEnumValue> crocodileSwitch)
        {
            parrotSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch, Func<ParrotEnumValue, TResult> parrotSwitch, Func<HamsterEnumValue, TResult> hamsterSwitch, Func<CrocodileEnumValue, TResult> crocodileSwitch)
        {
            return parrotSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch, Action<ParrotEnumValue, T0> parrotSwitch, Action<HamsterEnumValue, T0> hamsterSwitch, Action<CrocodileEnumValue, T0> crocodileSwitch)
        {
            parrotSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch, Func<ParrotEnumValue, T0, TResult> parrotSwitch, Func<HamsterEnumValue, T0, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch, Action<ParrotEnumValue, T0, T1> parrotSwitch, Action<HamsterEnumValue, T0, T1> hamsterSwitch, Action<CrocodileEnumValue, T0, T1> crocodileSwitch)
        {
            parrotSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileSwitch)
        {
            parrotSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileSwitch)
        {
            parrotSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileSwitch)
        {
            parrotSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileSwitch)
        {
            parrotSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileSwitch)
        {
            parrotSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileSwitch)
        {
            return parrotSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }

    public static readonly HamsterEnumValue Hamster = new HamsterEnumValue();
    public partial class HamsterEnumValue: PetKind
    {
        public HamsterEnumValue(): base(global::Sample.PetKind.Hamster) { }
        public override string ToString()
        {
            return "Big Boy";
        }

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch, Action<ParrotEnumValue> parrotSwitch, Action<HamsterEnumValue> hamsterSwitch, Action<CrocodileEnumValue> crocodileSwitch)
        {
            hamsterSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch, Func<ParrotEnumValue, TResult> parrotSwitch, Func<HamsterEnumValue, TResult> hamsterSwitch, Func<CrocodileEnumValue, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch, Action<ParrotEnumValue, T0> parrotSwitch, Action<HamsterEnumValue, T0> hamsterSwitch, Action<CrocodileEnumValue, T0> crocodileSwitch)
        {
            hamsterSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch, Func<ParrotEnumValue, T0, TResult> parrotSwitch, Func<HamsterEnumValue, T0, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch, Action<ParrotEnumValue, T0, T1> parrotSwitch, Action<HamsterEnumValue, T0, T1> hamsterSwitch, Action<CrocodileEnumValue, T0, T1> crocodileSwitch)
        {
            hamsterSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileSwitch)
        {
            hamsterSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileSwitch)
        {
            hamsterSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileSwitch)
        {
            hamsterSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileSwitch)
        {
            hamsterSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileSwitch)
        {
            hamsterSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileSwitch)
        {
            return hamsterSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }

    public static readonly CrocodileEnumValue Crocodile = new CrocodileEnumValue();
    public partial class CrocodileEnumValue: PetKind
    {
        public CrocodileEnumValue(): base(global::Sample.PetKind.Crocodile) { }
        public override string ToString()
        {
            return "Crocodile";
        }

        public override void Switch(Action<CatEnumValue> catSwitch, Action<DogEnumValue> dogSwitch, Action<ParrotEnumValue> parrotSwitch, Action<HamsterEnumValue> hamsterSwitch, Action<CrocodileEnumValue> crocodileSwitch)
        {
            crocodileSwitch(this);
        }

        public override TResult Switch<TResult>(Func<CatEnumValue, TResult> catSwitch, Func<DogEnumValue, TResult> dogSwitch, Func<ParrotEnumValue, TResult> parrotSwitch, Func<HamsterEnumValue, TResult> hamsterSwitch, Func<CrocodileEnumValue, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this);
        }

        public override void Switch<T0>(T0 arg0, Action<CatEnumValue, T0> catSwitch, Action<DogEnumValue, T0> dogSwitch, Action<ParrotEnumValue, T0> parrotSwitch, Action<HamsterEnumValue, T0> hamsterSwitch, Action<CrocodileEnumValue, T0> crocodileSwitch)
        {
            crocodileSwitch(this, arg0);
        }

        public override TResult Switch<TResult, T0>(T0 arg0, Func<CatEnumValue, T0, TResult> catSwitch, Func<DogEnumValue, T0, TResult> dogSwitch, Func<ParrotEnumValue, T0, TResult> parrotSwitch, Func<HamsterEnumValue, T0, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0);
        }

        public override void Switch<T0, T1>(T0 arg0, T1 arg1, Action<CatEnumValue, T0, T1> catSwitch, Action<DogEnumValue, T0, T1> dogSwitch, Action<ParrotEnumValue, T0, T1> parrotSwitch, Action<HamsterEnumValue, T0, T1> hamsterSwitch, Action<CrocodileEnumValue, T0, T1> crocodileSwitch)
        {
            crocodileSwitch(this, arg0, arg1);
        }

        public override TResult Switch<TResult, T0, T1>(T0 arg0, T1 arg1, Func<CatEnumValue, T0, T1, TResult> catSwitch, Func<DogEnumValue, T0, T1, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0, arg1);
        }

        public override void Switch<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Action<CatEnumValue, T0, T1, T2> catSwitch, Action<DogEnumValue, T0, T1, T2> dogSwitch, Action<ParrotEnumValue, T0, T1, T2> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2> crocodileSwitch)
        {
            crocodileSwitch(this, arg0, arg1, arg2);
        }

        public override TResult Switch<TResult, T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2, Func<CatEnumValue, T0, T1, T2, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0, arg1, arg2);
        }

        public override void Switch<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Action<CatEnumValue, T0, T1, T2, T3> catSwitch, Action<DogEnumValue, T0, T1, T2, T3> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3> crocodileSwitch)
        {
            crocodileSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, Func<CatEnumValue, T0, T1, T2, T3, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0, arg1, arg2, arg3);
        }

        public override void Switch<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<CatEnumValue, T0, T1, T2, T3, T4> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4> crocodileSwitch)
        {
            crocodileSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<CatEnumValue, T0, T1, T2, T3, T4, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0, arg1, arg2, arg3, arg4);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Action<CatEnumValue, T0, T1, T2, T3, T4, T5> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5> crocodileSwitch)
        {
            crocodileSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public override void Switch<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Action<CatEnumValue, T0, T1, T2, T3, T4, T5, T6> catSwitch, Action<DogEnumValue, T0, T1, T2, T3, T4, T5, T6> dogSwitch, Action<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6> parrotSwitch, Action<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6> hamsterSwitch, Action<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6> crocodileSwitch)
        {
            crocodileSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public override TResult Switch<TResult, T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<CatEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> catSwitch, Func<DogEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> dogSwitch, Func<ParrotEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> parrotSwitch, Func<HamsterEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> hamsterSwitch, Func<CrocodileEnumValue, T0, T1, T2, T3, T4, T5, T6, TResult> crocodileSwitch)
        {
            return crocodileSwitch(this, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

    }
}
}
